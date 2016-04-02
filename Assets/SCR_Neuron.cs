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

	public float Bias;
	public float[] Weights;

	public SCR_Neuron[] Previous = new SCR_Neuron[0];
	public SCR_Neuron[] Next = new SCR_Neuron[0];

	public float Current;

	public void Awake()
	{
		Refresh();
	}

	public void Evaulate(float t)
	{
		for (int i = 0; i < Next.Length; i++) 
		{
			
//			Next[i].Evaulate()
		}
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
		Next = all.Where(_ => _.Layer == Layer + 1).ToArray();

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

		foreach (var item in Next)
		{
			Gizmos.DrawLine(transform.position, item.transform.position);
		}
	}
}
