using UnityEngine;
using System.Collections;

public enum PointType
{
	Red,
	Blue
}

public class DATA_Point : MonoBehaviour 
{
	public PointType Type;

	public void OnValidate()
	{
		var renderer = GetComponent<SpriteRenderer>();

		if(renderer)
		{
			switch (Type)
			{
			case PointType.Red:
				renderer.color = Color.red;
				break;
			case PointType.Blue:
				renderer.color = Color.blue;
				break;
			}
		}
	}

	public void OnDrawGizmos()
	{
		
//		Gizmos.drawc
	}
}
