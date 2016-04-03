using UnityEngine;
using System.Collections;
using System.Linq;

[RequireComponent(typeof(TextMesh))]
public abstract class SCR_Node : TextBase
{
	public float Value;
	public float Gradient;

	public abstract float Forward();
	public abstract float Backward();

	public override string GetText ()
	{
		return Value.ToString();
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.5F, 0.8F, 1, 1);

		Gizmos.DrawWireCube(transform.position, new Vector2(1, 1));

		Gizmos.color = new Color(0, 0, 0, 0);

		Gizmos.DrawWireCube(transform.position, new Vector2(1, 1));

	}

//	public float Bias = 0.1F;
//	public float[] PreviousWeights;
//	public SCR_Node[] Previous = new SCR_Node[0];

//	public float Current;

//	public void Awake()
//	{
//		Refresh();
//	}
//
//	public void Evaulate()
//	{
//		var total = 0F;
//
//		for (int i = 0; i < Previous.Length; i++) 
//		{
//			var previous = Previous[i];
//
//			total += previous.Sigmoid(previous.Current, PreviousWeights[i], previous.Bias);
//		}
//
//		Current = total;
//	}
//
//	public float Sigmoid(float t, float weight, float b)
//	{
//		return weight * (1F / (1F + Mathf.Pow(2, -t + b)));
//	}
//
//	public void Refresh()
//	{
//		var all = SCR_Node.FindObjectsOfType<SCR_Node>();
//
//		Previous = all.Where(_ => _.Layer == Layer - 1).ToArray();
////		Next = all.Where(_ => _.Layer == Layer + 1).ToArray();
//
//		PreviousWeights = new float[Previous.Length];
//
//		for (int i = 0; i < PreviousWeights.Length; i++) 
//		{
//			PreviousWeights[i] = 0.1F;	
//		}
//
//		var x = Layer * 2 + 1;
//		var y = -Feature - 2;
//
//		transform.position = new Vector2(x, y);
//	}
//
//	bool hasValidated;
//
//	public void OnValidate()
//	{
//		if(Application.isPlaying || hasValidated)
//		{
//			return;
//		}
//
//		hasValidated = true;
//
//		Refresh();
//	}
//
//	public void OnDrawGizmos()
//	{
//		Gizmos.color = Color.yellow;
//
////		Gizmos.DrawWireSphere(transform.position, 0.25F);
//
//		foreach (var item in Previous)
//		{
//			Gizmos.DrawLine(transform.position, item.transform.position);
//		}
//	}
}
