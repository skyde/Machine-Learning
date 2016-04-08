using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;

public class Multiply : Unit
{
	public override string Identifier 
	{
		get 
		{
			return "mul";
		}
	}

	public override void Forward ()
	{
		var value = 1.0;

		for (int i = 0; i < Inputs.Count; i++)
		{
			value *= Inputs[i].Value;
		}

		Value = value;
	}

	public override void Backward ()
	{
		for (int i = 0; i < Inputs.Count; i++)
		{
			var value = 1.0;

			for (int x = 0; x < Inputs.Count; x++) 
			{
				if(i == x)
				{
					continue;
				}

				value *= Inputs[x].Value;
			}

			Inputs[i].Gradient += value * Gradient;
		}
	}
}

