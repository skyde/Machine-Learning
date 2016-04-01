using UnityEngine;
using System.Collections;

public class SCR_LinearCurveFitting : MonoBehaviour 
{
	public float B;
	public float M;

	public DATA_Point[] Points;

	public float BStep = 0.1F;
	public float MStep = 0.05F;

	public void Update() 
	{
		Points = GameObject.FindObjectsOfType<DATA_Point>();

		{
			var dist = TotalDistanceSquared(M, B);

			var newB = B + (Random.value - 0.5F) * BStep;
			var newDist = TotalDistanceSquared(M, newB);

			if(Mathf.Abs(newDist) < Mathf.Abs(dist))
			{
				B = newB;
			}
		}

		{
			var dist = TotalDistanceSquared(M, B);

			var newM = M + (Random.value - 0.5F) * MStep;
			var newDist = TotalDistanceSquared(newM, B);

			if(Mathf.Abs(newDist) < Mathf.Abs(dist))
			{
				M = newM;
			}
		}
	}

	public float TotalDistanceSquared(float m, float b)
	{
		float total = 0;

		foreach (var point in Points)
		{
			var d = Distance(point.transform.position, m, b);

			total += d * d;
		}

		return total;
	}

	public static float Distance(Vector2 position, float m, float b)
	{
		var lineY = b + m * position.x;

		return position.y - lineY;
	}

	public void OnDrawGizmos()
	{
		Gizmos.DrawLine(new Vector2(0, B), new Vector2(1000, B + M * 1000));
	}
}
