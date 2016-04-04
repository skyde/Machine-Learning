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
	public float Weight = 0.5F;
//	public float Gradient;

	public override string GetText ()
	{
		return Weight.ToString();
	}

	public override float TextScale
	{
		get
		{
			return 0.5F;
		}
	}

	public override void Update()
	{
		base.Update();

		if(!Previous || !Next)
		{
			return;
		}

//		ValidateNode(Previous);
//		ValidateNode(Next);

		transform.position = Vector2.Lerp(Previous.transform.position, Next.transform.position, 0.333F);
	}

//	public void ValidateNode(SCR_Node node)
//	{
//
//	}

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
