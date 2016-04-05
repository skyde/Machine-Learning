using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public abstract class Unit : TextBase
{
	public float Input
	{
		get
		{
			if(PreviousUnits.Count > 0)
			{
				var input = 0F;

				for (int i = 0; i < PreviousUnits.Count; i++)
				{
					input += PreviousUnits[i].Value;
				}

				return input;
			}

			return Value;
		}
	}

	public float Value;
	public float Gradient;

	public List<Unit> NextUnits = new List<Unit>();
	public List<Unit> PreviousUnits = new List<Unit>();

	public abstract void Forward();
	public abstract void Backward();

//	public void Start()
//	{
////		Refresh();
//	}

//	public void Refresh()
//	{
//		var connections = GameObject.FindObjectsOfType<SCR_Connection>();
//
//		NextUnits = connections.Where(_ => _.PreviousUnits.Contains(this)).ToArray();
//		PreviousUnits = connections.Where(_ => _.NextUnits.Contains(this)).ToArray();
//	}

//	public override void OnValidate()
//	{
//		base.OnValidate();
//
//		Refresh();
//	}
}
