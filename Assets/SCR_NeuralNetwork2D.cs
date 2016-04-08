using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SCR_NeuralNetwork2D : MonoBehaviour 
{
	public float Step = 0.0005F;
	public bool Converge = true;

	public Unit[] Inputs;
	public Unit Output;

	public Unit[] AllUnits = new Unit[0];
	public List<Layer> Layers = new List<Layer>();
	DATA_Point[] Points;

	public GameObject LayersContainer;
	public GameObject Connection;

	public void Awake()
	{
		Points = GameObject.FindObjectsOfType<DATA_Point>();
		Layers = new List<Layer>();

		SCR_Node[] lastNodes = null;

		for (int i = 0; i < LayersContainer.transform.childCount; i++)
		{
			var obj = LayersContainer.transform.GetChild(i).gameObject;
			var nodes = obj.GetComponentsInChildren<SCR_Node>();

			if(lastNodes != null)
			{
				var t = 0F;
				foreach (var left in lastNodes) 
				{
					t += 1F / (float) lastNodes.Length;
					foreach (var right in nodes) 
					{
						var c = GameObject.Instantiate(Connection);
						var connection = c.GetComponent<SCR_Node>();

						connection.transform.position = Vector2.Lerp(left.transform.position, right.transform.position, 0.35F + t * 0.3F);
//						connection.transform.parent = left.transform.parent;


						connection.Input.Inputs.Add(left.Output);
						right.Input.Inputs.Add(connection.Output);

						left.SubLayers.AddRange(connection.SubLayers);
					}
				}
			}

			var l = new Layer();
			l.Nodes.AddRange(nodes);
			Layers.Add(l);

			lastNodes = nodes;
		}

		AllUnits = GameObject.FindObjectsOfType<Unit>();

		foreach (var item in AllUnits) 
		{
			if(item is Data)
			{
				item.Value = (Random.value - 0.5F);
			}
		}

//		for (int i = 0; i < 100; i++) 
//		{
//			Layer layer = null;
//
//			foreach (var item in AllUnits) 
//			{
//				if(item.Layer == i)
//				{
//					if(layer == null)
//					{
//						layer = new Layer();
//						Layers.Add(layer);
//					}
//
//					layer.Units.Add(item);
//				}
//			}

//			if(layer == null)
//			{
//				break;
//			}
//		}
	}

	public void Update()
	{
		foreach (var item in Points) 
		{
			var target = item.Type == PointType.Red ? 1 : -1;
			RunStep(target, item.transform.position.x, item.transform.position.y);
		}
	}

	public void RunStep(double desiredOutput, params double[] inputs)
	{
		for (int i = 0; i < inputs.Length; i++) 
		{
			Inputs[i].Value = inputs[i];	
		}
//		Input.Value = input;

		foreach (var layer in Layers) 
		{
			foreach (var unit in layer.Nodes) 
			{
				unit.Forward();
			}
		}

		foreach (var item in AllUnits) 
		{
			item.Gradient = 0F;
		}

		Output.Gradient = desiredOutput - Output.Value;

		for (int i = Layers.Count - 1; i >= 0; i--)
		{
			foreach (var unit in Layers[i].Nodes) 
			{
				unit.Backward();
			}
		}

		if(Converge)
		{
			foreach (var item in AllUnits) 
			{
				if(item is Data)
				{
					item.Value += Step * item.Gradient;
				}
			}
		}
	}

	public bool Preview = true;

	public double Evaluate(params double[] inputs)
	{
		for (int i = 0; i < inputs.Length; i++) 
		{
			Inputs[i].Value = inputs[i];	
		}

		foreach (var layer in Layers) 
		{
			foreach (var unit in layer.Nodes) 
			{
				if(unit == null)
				{
					continue;
				}

				unit.Forward();
			}
		}

		return Output.Value;
	}

	public void OnDrawGizmos()
	{
		if(!Preview)
		{
			return;
		}
		var iter = 32;
		var xSize = 10F;
		var ySize = 10F;

		for (int x = 0; x < iter; x++) 
		{
			var xT = x / (float) iter;

			for (int y = 0; y < iter; y++) 
			{
				var yT = y / (float) iter;

				var p = new Vector2(xT * xSize, yT * ySize);

				var v = Evaluate(p.x, p.y);

				Gizmos.color = new Color((float) v, 1F, 1F - (float) v * 0.5F, 0.5F * Mathf.Clamp01(1F - (float) v));

				Gizmos.DrawCube(new Vector3(p.x, p.y, 0), new Vector2(xSize / iter, ySize / iter));
			}
		}

//		Gizmos.color = Color.yellow;
//		Gizmos.DrawLine(Vector2.zero, new Vector2(100, 0));
//
//		Gizmos.color = Color.white;
//
//		var iter = 1000;
//        var last = Vector2.zero;
//		
//		for (int i = 0; i < iter; i++) 
//		{
//			var t = i / (float) iter;
//			
//			var x = t * 20;
//
//			var y = Evaluate(x);
//
//			var p = new Vector2(x, (float) y);
//			
//			if(i > 0)
//			{
//                Gizmos.DrawLine(last, p);
//			}
//			
//			last = p;
//		}
	}
}