using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using System;

public class Sigmoid : Unit
{
	public override string Identifier 
	{
		get 
		{
			return "sig";
		}
	}

	public override void Forward ()
	{
		Value = CaculateSigmoid(SumInputValues());
	}

	public override void Backward ()
	{
		for (int i = 0; i < Inputs.Count; i++)
		{
			Inputs[i].Gradient = (Value * (1F - Value)) * Gradient;
		}
	}

	public static double CaculateSigmoid(double value)
	{
		return 1.0 / (1.0 + Math.Exp(-value));
	}
}
