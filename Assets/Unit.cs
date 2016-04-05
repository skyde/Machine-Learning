using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public abstract class Unit : TextBase
{
	public float Input;
	public float Value;
	public float Gradient;

	public SCR_Connection[] NextConnections;
	public SCR_Connection[] PreviousConnections;

	public abstract void Forward();
	public abstract void Backward();

	public void Start()
	{
		Refresh();
	}

	public void Refresh()
	{
		var connections = GameObject.FindObjectsOfType<SCR_Connection>();

		NextConnections = connections.Where(_ => _.Previous == this).ToArray();
		PreviousConnections = connections.Where(_ => _.Next == this).ToArray();
	}

	public override void OnValidate()
	{
		base.OnValidate();

		Refresh();
	}
}
