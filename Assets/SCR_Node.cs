//using UnityEngine;
//using System.Collections;
//using System.Linq;
//using System.Collections.Generic;
//
//[RequireComponent(typeof(TextMesh))]
//public class SCR_Node : Unit
//{
//	public enum ActivateFunctionStyle
//	{
//		Sigmoid,
//		PureLinear,
//		Bias
//	}
//
//	public ActivateFunctionStyle ActivateStyle = ActivateFunctionStyle.Sigmoid;
//
////	public override bool UsesConstant
////	{
////		get 
////		{
////			return ActivateStyle == ActivateFunctionStyle.Bias;
////		}
////	}
//
////	public float Input()
////	{
////
////	}
//
//	public override void Forward()
//	{
//		switch (ActivateStyle) 
//		{
//		case ActivateFunctionStyle.Sigmoid:
//			Value = 1F / (1F + Mathf.Pow(2.71828F, -Input + Constant));
//			break;
//		case ActivateFunctionStyle.PureLinear:
//			Value = Input;
//			break;
//		case ActivateFunctionStyle.Bias:
//			Value = Input + Constant;
//			break;
//		}
//	}
//
//	public override void Backward ()
//	{
//		switch (ActivateStyle) 
//		{
//		case ActivateFunctionStyle.Sigmoid:
////			var s = 1F / (1F + Mathf.Pow(2.71828F, -Input + Constant));
//
//			Gradient = Value * (1F - Value) * SumGradients();
//			break;
//		case ActivateFunctionStyle.PureLinear:
//
//			Gradient = 1 * SumGradients();
//
//			//			foreach (var item in PreviousUnits)
//			//			{
//			//				item.
//			//			}
//			//			Gradient = Input;
//			break;
//		case ActivateFunctionStyle.Bias:
//
//			Gradient = SumGradients();
//
//			//			foreach (var item in PreviousUnits)
//			//			{
//			//				item.
//			//			}
//			//			Gradient = Input;
//			break;
//		}
//	}
//
//	public override string GetText ()
//	{
//		if(Input == Value)
//		{
//			return Input.ToString("##.######") + "\ng=" + Gradient.ToString("##.######");
//		}
//
//		return Input.ToString("##.######") + "\n" + Value.ToString("##.######") + "\ng=" + Gradient.ToString("##.######");
//	}
//
//	public void OnDrawGizmos()
//	{
//		if(!UsesConstant)
//		{
//			Constant = 0;
//		}
//
//		Gizmos.color = new Color(0.5F, 0.8F, 1, 1);
//
//		Gizmos.DrawWireCube(transform.position, new Vector2(1, 1));
//
//		Gizmos.color = new Color(0, 0, 0, 0);
//
//		Gizmos.DrawCube(transform.position, new Vector2(1, 1));
//	}
//
//}
