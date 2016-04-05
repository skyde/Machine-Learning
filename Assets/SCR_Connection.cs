using UnityEngine;
using System.Collections;
using System.Linq;

//[ExecuteInEditMode]
public class SCR_Connection : Unit
{
	public float Weight = 0.5F;

	public override string GetText ()
	{
        return Weight.ToString("##.######");
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

	public override void Forward ()
	{
		
	}

	public override void Backward ()
	{
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
