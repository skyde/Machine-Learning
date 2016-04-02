using UnityEngine;
using System.Collections;
using System.Linq;

//public class Connection : MonoBehaviour
//{
//	public SCR_Neuron Source;
//	public SCR_Neuron Target;
//}

public class SCR_Neuron : MonoBehaviour
{
	public int Feature;
	public int Layer;

	public float Bias = 0.1F;
	public float[] PreviousWeights;
	public SCR_Neuron[] Previous = new SCR_Neuron[0];

	public float Current;

	public void Awake()
	{
		Refresh();
	}

	public void Evaulate()
	{
		var total = 0F;

		for (int i = 0; i < Previous.Length; i++) 
		{
			var previous = Previous[i];

			total += previous.Sigmoid(previous.Current, PreviousWeights[i], previous.Bias);
		}

		Current = total;
	}

	public float Sigmoid(float t, float weight, float b)
	{
		var v = weight * t + b;

		return 1F / (1F + Mathf.Pow(2, -v));
	}

	public void Refresh()
	{
		var all = SCR_Neuron.FindObjectsOfType<SCR_Neuron>();

		Previous = all.Where(_ => _.Layer == Layer - 1).ToArray();
//		Next = all.Where(_ => _.Layer == Layer + 1).ToArray();

		PreviousWeights = new float[Previous.Length];

		for (int i = 0; i < PreviousWeights.Length; i++) 
		{
			PreviousWeights[i] = 0.1F;	
		}

		var x = Layer * 2 + 1;
		var y = -Feature - 2;

		transform.position = new Vector2(x, y);
	}

	bool hasValidated;

	public void OnValidate()
	{
		if(Application.isPlaying || hasValidated)
		{
			return;
		}

		hasValidated = true;

		Refresh();
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

//		Gizmos.DrawWireSphere(transform.position, 0.25F);

		foreach (var item in Previous)
		{
			Gizmos.DrawLine(transform.position, item.transform.position);
		}
	}
}
