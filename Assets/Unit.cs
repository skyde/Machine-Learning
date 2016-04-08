using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

public abstract class Unit : MonoBehaviour
{
	public float Value = 1;
	public float Gradient = 1;

	public int Layer;

	public List<Unit> Inputs = new List<Unit>();
	public List<Unit> Outputs = new List<Unit>();

	public abstract string Identifier { get; }

//	public void CaculateForward()
//	{
//		Value = Forward();
//	}
//
//	public void CaculateBackward()
//	{
//		Gradient = Backward();
//	}

	public abstract void Forward();
//	{
////		if(Inputs.Count > 0)
////		{
////
////		}
////		aV SumInputValues();
//	}

	public abstract void Backward();
//	{
////		return SumOutputGradients();
//	}

	public float SumInputValues()
	{
		var value = 0F;

		for (int i = 0; i < Inputs.Count; i++)
		{
			value += Inputs[i].Value;
		}

		return value;
	}

//	public float SumOutputGradients()
//	{
//		var value = 0F;
//
//		for (int i = 0; i < Outputs.Count; i++)
//		{
//			value += Outputs[i].Gradient;
//		}
//
//		return value;
//	}

	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(1, 0.7F, 0.4F);
		foreach (var item in Inputs) 
		{
			var mid = Vector2.Lerp(transform.position, item.transform.position, 0.5F);
			Gizmos.DrawLine(transform.position, mid);
		}

		Gizmos.color = new Color(0.4F, 0.7F, 1F);
		foreach (var item in Outputs) 
		{
			var mid = Vector2.Lerp(transform.position, item.transform.position, 0.5F);
			Gizmos.DrawLine(transform.position, mid);
		}

		var size = new Vector3(2, 2);

		Gizmos.color = new Color(0.5F, 0.8F, 1F, 0.7F);
		Gizmos.DrawWireCube(transform.position, size);

		Gizmos.color = new Color(0, 0, 0, 0);
		Gizmos.DrawCube(transform.position, size);

		Handles.Label(transform.position, 
			Identifier + "\nvalue " + Value + "\ngradient " + Gradient);
//		Helpers.
	}
}
