using UnityEngine;
using System.Collections;
using System.Linq;

//public class Connection : MonoBehaviour
//{
//	public SCR_Neuron Source;
//	public SCR_Neuron Target;
//}

//public class SCR_Multiply

[ExecuteInEditMode]
public class SCR_Connection : TextBase
{
	public SCR_Node Previous;
	public SCR_Node Next;

//	public float Weight;
	public float Value;
	public float Gradient;

	public override float TextScale
	{
		get
		{
			return 0.5F;
		}
	}

//	public abstract float Forward();
//
//	public abstract float Backward();

	public void Update()
	{
		if(!Previous || !Next)
		{
			return;
		}

		transform.position = (Previous.transform.position + Next.transform.position) * 0.5F;
	}

	public void OnDrawGizmos()
	{
		if(!Previous || !Next)
		{
			return;
		}

		Gizmos.color = new Color(0, 0, 0, 1);

		Gizmos.DrawLine(Previous.transform.position, Next.transform.position);

		Gizmos.color = new Color(0, 0, 0, 0);

		var iter = 32;
		for (int i = 0; i < iter; i++) 
		{
			var t = i / (iter + 1F);
			Gizmos.DrawSphere(Vector2.Lerp(Previous.transform.position, Next.transform.position, t), 0.05F);
		}
	}
}
