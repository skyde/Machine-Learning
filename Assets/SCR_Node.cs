using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

[RequireComponent(typeof(TextMesh))]
public class SCR_Node : Unit
{
	public enum ActivateFunctionStyle
	{
		Sigmoid,
		PureLinear,
	}

	public ActivateFunctionStyle ActivateStyle = ActivateFunctionStyle.Sigmoid;

	public float Bias;

//	public float Input()
//	{
//
//	}

	public override void Forward()
	{
		switch (ActivateStyle) 
		{
		case ActivateFunctionStyle.Sigmoid:
			Value = 1F / (1F + Mathf.Pow(2.71828F, -Input + Bias));
			break;
		case ActivateFunctionStyle.PureLinear:
			Value = Input + Bias;
			break;
		}
	}

	public override void Backward ()
	{
		switch (ActivateStyle) 
		{
		case ActivateFunctionStyle.Sigmoid:
			Gradient = Value * (1F - Value);
			break;
		case ActivateFunctionStyle.PureLinear:
			Gradient = 1;
			break;
		}
	}

	public override string GetText ()
	{
		if(Input == Value)
		{
            return Input.ToString("##.######");
		}

        return Input.ToString("##.######") + "\n" + Value.ToString("##.######");
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = new Color(0.5F, 0.8F, 1, 1);

		Gizmos.DrawWireCube(transform.position, new Vector2(1, 1));

		Gizmos.color = new Color(0, 0, 0, 0);

		Gizmos.DrawCube(transform.position, new Vector2(1, 1));
	}

}
