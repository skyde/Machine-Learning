using UnityEngine;
using System.Collections;
using System.Linq;

public class Connection : MonoBehaviour
{
	public SCR_Neuron Source;
	public SCR_Neuron Target;
}

public class SCR_Neuron : MonoBehaviour
{
	public int Feature;
	public int Layer;

	public float Bias;
	public float[] Weights;

	public SCR_Neuron[] Sources = new SCR_Neuron[0];
	public SCR_Neuron[] Targets = new SCR_Neuron[0];

	public void Awake()
	{
		Refresh();
	}

	public void Update()
	{

	}

	public float Sigmoid(float x, float weight, float b)
	{
		var v = weight * x + b;

		return 1F / (1F + Mathf.Pow(2, -v));
	}

	public void Refresh()
	{
		var all = SCR_Neuron.FindObjectsOfType<SCR_Neuron>();

		Sources = all.Where(_ => _.Layer == Layer - 1).ToArray();
		Targets = all.Where(_ => _.Layer == Layer + 1).ToArray();

		Weights = new float[Weights.Length];

		var x = Layer * 2 + 1;
		var y = -Feature - 2;

		transform.position = new Vector2(x, y);
	}

	public void OnValidate()
	{
		if(Application.isPlaying)
		{
			return;
		}

		Refresh();
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

//		Gizmos.DrawWireSphere(transform.position, 0.25F);

		foreach (var item in Targets)
		{
			Gizmos.DrawLine(transform.position, item.transform.position);
		}
	}
}
