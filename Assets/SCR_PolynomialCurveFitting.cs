using UnityEngine;
using System.Collections;

public class SCR_PolynomialCurveFitting : MonoBehaviour 
{
	DATA_Point[] Points;

	public float[] Constants = new float[0];

	public float Step = 0.1F;

	public bool AutoFit = true;

	public void Awake()
	{
		Points = GameObject.FindObjectsOfType<DATA_Point>();
	}

	public void Update() 
	{
		if(!AutoFit)
		{
			return;
		}

		var constants = new float[Constants.Length];

		for (int i = 0; i < Constants.Length; i++) 
		{
			var dist = TotalDistanceSquared(Constants);

			for (int c = 0; c < Constants.Length; c++) 
			{
				constants[c] = Constants[c];
			}

			var newConstant = Constants[i] + (Random.value - 0.5F) * Step;

			constants[i] = newConstant;

			var newDist = TotalDistanceSquared(constants);

			if(Mathf.Abs(newDist) < Mathf.Abs(dist))
			{
				Constants[i] = newConstant;
			}
		}
	}

	public float TotalDistanceSquared(float[] constants)
	{
		float total = 0;

		foreach (var point in Points)
		{
			var d = Distance(point.transform.position, constants);

			total += d * d;
		}

		return total;
	}

	public static float Distance(Vector2 position, float[] constants)
	{
		var p = Evaluate(position.x, constants);

		return position.y - p.y;
	}

	public static Vector2 Evaluate(float x, float[] constants)
	{
		var lineY = constants[0];

		for (int i = 1; i < constants.Length; i++)
		{
			lineY += Mathf.Pow(x, i) * constants[i];
		}

		return new Vector2(x, lineY);
	}

	public void OnDrawGizmos()
	{
		if(Constants.Length == 0)
		{
			return;
		}

		var iter = 128;
		var last = Vector2.zero;

		for (int i = 0; i < iter; i++) 
		{
			var t = i / (float) iter;

			var x = t * 14;

			var p = Evaluate(x, Constants);

			if(i > 0)
			{
				Gizmos.DrawLine(last, p);
			}

			last = p;
		}


//		Gizmos.DrawLine(new Vector2(0, B), new Vector2(1000, B + M * 1000));
	}
}
