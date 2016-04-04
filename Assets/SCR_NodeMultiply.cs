using UnityEngine;
using System.Collections;
using System.Linq;

public class SCR_NodeMultiply : SCR_Node
{
	public float Bias;

	public override float TransformOutput (float value)
	{
		return Sigmoid(value, Bias);
	}

	public override float Forward ()
	{
		var total = 0F;

		for (int i = 0; i < PreviousConnections.Length; i++)
		{
			total += PreviousConnections[i].Previous.TransformedValue * PreviousConnections[i].Weight;
		}

		return total;
	}

	public override float Backward ()
	{
		return 0;
	}
}
