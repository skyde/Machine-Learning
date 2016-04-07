using UnityEngine;
using System.Collections;
using System.Linq;

//[ExecuteInEditMode]
public class SCR_Connection : Unit
{
//	public float Constant = 0.5F;

	public override string GetText ()
	{
		return Constant.ToString("##.######") + "\ng=" + Gradient.ToString("##.######");
	}

	public override float TextScale
	{
		get
		{
			return 0.5F;
		}
	}

	public void Start()
	{
		foreach (var item in PreviousUnits) 
		{
			if(!item.NextUnits.Contains(this))
			{
				item.NextUnits.Add(this);
			}
		}

		foreach (var item in NextUnits) 
		{
			if(!item.PreviousUnits.Contains(this))
			{
				item.PreviousUnits.Add(this);
			}
		}
	}

	public static float Sigmoid(float v)
	{
//		if (v < -10.0F) return 0.0F;
//		else if (v > 10.0F) return 1.0F;
//
//		v = Mathf.Clamp(v, -20, 20);

		return 1F / (1F + Mathf.Pow(2.71828F, -v));
	}

	public override void Forward ()
	{
//		Value = Input * Sigmoid(Constant);//1F / (1F + Mathf.Pow(2.71828F, -Input + Constant));

		Value = Input * Constant;
	}

	public override void Backward ()
	{
//		Gradient = Sigmoid(Constant) * (1F - Sigmoid(Constant)) * SumGradients();

		Gradient = Constant * SumGradients();
	}

	public override void Update()
	{
		base.Update();

		if(PreviousUnits.Count() == 0 || NextUnits.Count == 0)
		{
			return;
		}

//		ValidateNode(Previous);
//		ValidateNode(Next);

		transform.position = Vector2.Lerp(
			PreviousUnits[0].transform.position, 
			NextUnits[0].transform.position, 
			0.333F);
	}

//	public void ValidateNode(SCR_Node node)
//	{
//
//	}

	public void OnDrawGizmos()
	{
		if(PreviousUnits.Count == 0 || NextUnits.Count == 0)
		{
			return;
		}

		Gizmos.color = new Color(0, 0, 0, 1);

		Gizmos.DrawLine(PreviousUnits[0].transform.position, NextUnits[0].transform.position);

		Gizmos.color = new Color(0, 0, 0, 0);

		var iter = 32;
		for (int i = 0; i < iter; i++) 
		{
			var t = i / (iter + 1F);
			Gizmos.DrawSphere(Vector2.Lerp(PreviousUnits[0].transform.position, NextUnits[0].transform.position, t), 0.05F);
		}
	}
}
