using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

public class Add : Unit
{
	public override void Forward ()
	{
		Value = SumInputValues();
	}

	public override void Backward ()
	{
		for (int i = 0; i < Inputs.Count; i++)
		{
			Inputs[i].Gradient = Gradient;
		}
	}
}
