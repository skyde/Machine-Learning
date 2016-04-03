using UnityEngine;
using System.Collections;
using System.Linq;

public class SCR_NodeData : SCR_Node
{
	public SCR_Connection[] ForwardConnections;
	public SCR_Connection[] BackwardConnections;

	public void Awake()
	{

	}

	public override float Forward ()
	{
		return Value;
	}

	public override float Backward ()
	{
		return 0;
	}
}
