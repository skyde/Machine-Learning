using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SCR_Node : MonoBehaviour 
{
	public Unit Input;
	public Unit Output;

	public List<Unit> SubLayers;

	public void Forward()
	{
		for (int i = 0; i < SubLayers.Count; i++) 
		{
//			print("forward " + SubLayers[i].name + " " + SubLayers[i].Identifier);
			SubLayers[i].Forward();
		}
	}

	public void Backward()
	{
		for (int i = SubLayers.Count - 1; i >= 0; i--) 
		{
//			print("Backward " + SubLayers[i].name + " " + SubLayers[i].Identifier);

			SubLayers[i].Backward();
		}
	}
}
