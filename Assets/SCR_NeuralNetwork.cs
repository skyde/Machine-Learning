using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//[System.Serializable]
//public class NetworkLayer
//{
//	public GameObject LayerBase;
//	public List<Unit> Nodes = new List<Unit>();
//}

[System.Serializable]
public class Layer
{
	public List<Unit> Units = new List<Unit>();
}

//[ExecuteInEditMode]
public class SCR_NeuralNetwork : MonoBehaviour 
{
//	public bool AutoFit = true;
//	public bool Preview = true;

	public Unit Input;
	public Unit Output;

	public Unit[] AllUnits = new Unit[0];
	public List<Layer> Layers = new List<Layer>();

	public float Step = 0.0005F;

	public bool Converge = true;

	public void Awake()
	{
		AllUnits = GameObject.FindObjectsOfType<Unit>();

		Layers = new List<Layer>();

		for (int i = 0; i < 100; i++) 
		{
			Layer layer = null;

			foreach (var item in AllUnits) 
			{
				if(item.Layer == i)
				{
					if(layer == null)
					{
						layer = new Layer();
						Layers.Add(layer);
					}

					layer.Units.Add(item);
				}
			}

			if(layer == null)
			{
				break;
			}
		}
	}

	public void Update()
	{
		foreach (var layer in Layers) 
		{
			foreach (var unit in layer.Units) 
			{
				unit.Forward();
			}
		}

		foreach (var item in AllUnits) 
		{
			item.Gradient = 1F;
		}

		for (int i = Layers.Count - 1; i >= 0; i--)
		{
			foreach (var unit in Layers[i].Units) 
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
					item.Value += Step;
				}
			}
		}
	}

//	public GameObject LayersBase;
//	public NetworkLayer[] Layers;
//
//	public float Step = 0.01F;
//	public GameObject Connection;
//
//	DATA_Point[] Points;
//	Unit[] Units;

//	public void Update()
//	{
//		if(!Application.isPlaying)
//		{
//			return;
//		}
//
//		if(AutoFit)
//		{
//			foreach (var point in Points) 
//			{
//				var pos = point.transform.position;
//
//				Input.Value = pos.x;
//
//				for(int i = 0; i < Layers.Length; i++)
//				{
//					for(int p = 0; p < Layers[i].Nodes.Count; p++)
//					{
//						Layers[i].Nodes[p].Forward(); 
//					}
//				}
//
//				foreach (var item in Units) 
//				{
//					item.Gradient = item.Value;
//				}
//
//				const float analyticalStep = 0.0001F;
//
//				Input.Value = pos.x + analyticalStep;
//
//				for(int i = 0; i < Layers.Length; i++)
//				{
//					for(int p = 0; p < Layers[i].Nodes.Count; p++)
//					{
//						Layers[i].Nodes[p].Forward(); 
//					}
//				}
//
//				foreach (var item in Units) 
//				{
//					item.Gradient = (item.Gradient - item.Value) / analyticalStep;
//				}
//
////				foreach (var item in Layers[Layers.Length - 1].Nodes) 
////				{
////					var v = pos.y - item.Value;
////
////					v = (v * v) * Mathf.Sign(v);
////
////					item.Gradient = v;
////				}
////
////				for(int i = Layers.Length - 2; i >= 0; i--)
////				{
////					for(int p = 0; p < Layers[i].Nodes.Count; p++)
////					{
////						Layers[i].Nodes[p].Backward(); 
////					}
////				}
////
////				foreach (var item in Units) 
////				{
////					if(item.UsesConstant)
////					{
////						item.Constant += Step * item.Gradient;// * item.Value;
////					}
////				}
//			}
//		}
//		else
//		{
////			for(int i = 0; i < Layers.Length; i++)
////			{
////				for(int p = 0; p < Layers[i].Nodes.Count; p++)
////				{
////					Layers[i].Nodes[p].Forward(); 
////				}
////			}
////
////			foreach (var item in Layers[Layers.Length - 1].Nodes) 
////			{
////				item.Gradient = Target - item.Value;
////			}
////
////			for(int i = Layers.Length - 2; i >= 0; i--)
////			{
////				for(int p = 0; p < Layers[i].Nodes.Count; p++)
////				{
////					Layers[i].Nodes[p].Backward(); 
////				}
////			}
//
////			foreach (var item in Units) 
////			{
////				if(item.UsesConstant)
////				{
////					item.Constant += Step * item.Gradient;// * item.Value;
////				}
////			}
//		}
//
////		foreach (var point in Points) 
////		{
////			//var p = (Vector2) point.transform.position;
////
////			//var y = Evaluate(p.x);
////
//////			var error = p.y - y;
//////
//////			error *= error;
//////
//////			value += error;
////			Input.Value = point.transform.position.x;
////
////			for(int i = 0; i < Layers.Length; i++)
////			{
////				for(int p = 0; p < Layers[i].Nodes.Count; p++)
////				{
////					Layers[i].Nodes[p].Forward(); 
////				}
////			}
////
////			for(int i = Layers.Length - 1; i >= 0; i--)
////			{
////				for(int p = 0; p < Layers[i].Nodes.Count; p++)
////				{
////					Layers[i].Nodes[p].Backward(); 
////				}
////			}
////				
////		}
//
////		foreach (var item in Units) 
////		{
////			var lastError = CaculateError();
////			var lastValue = item.Constant;
////
////			item.Constant += (Random.value - 0.5F) * Step;
////
////			if(CaculateError() > lastError)
////			{
////				item.Constant = lastValue;
////			}
////		}
//	}
//
//	public float Target = 3;
//
//	public void Awake()
//	{
//		Points = GameObject.FindObjectsOfType<DATA_Point>();
//
//		Layers = new NetworkLayer[LayersBase.transform.childCount * 2 - 1];
//
//		for (int i = 0; i < Layers.Length; i++) 
//		{
//			Layers[i] = new NetworkLayer();
//		}
//
//		for (int i = 0; i < LayersBase.transform.childCount; i++) 
//		{
//			var c = LayersBase.transform.GetChild(i);
//
//			Layers[i * 2].LayerBase = c.gameObject;
//			Layers[i * 2].Nodes = Layers[i * 2].LayerBase.GetComponentsInChildren<SCR_Node>().ToList<Unit>();
//		}
//
//		if(Application.isPlaying)
//		{
//			for (int i = 0; i < Layers.Length - 1; i += 2)
//			{
//				var currentLayer = Layers[i];
//				var middleLayer = Layers[i + 1];
//				var nextLayer = Layers[i + 2];
//
//				for (int c = 0; c < currentLayer.Nodes.Count; c++)
//				{
//					for (int n = 0; n < nextLayer.Nodes.Count; n++)
//					{
//						var previous = currentLayer.Nodes[c];
//						var next = nextLayer.Nodes[n];
//
//						var obj = GameObject.Instantiate(Connection);
//
//						var connection = obj.GetComponent<SCR_Connection>();
//
//						connection.PreviousUnits.Add(previous);
//						connection.NextUnits.Add(next);
//
//						middleLayer.Nodes.Add(connection);
//
//						obj.transform.parent = transform;
//					}
//				}
//			}
//		}
//
//		Units = GameObject.FindObjectsOfType<Unit>();
//
//		foreach (var item in Units) 
//		{
//			item.Constant = Random.value - 0.5F;
//		}
//	}
//
//	public float CaculateError()
//	{
//		var value = 0F;
//
//		foreach (var point in Points) 
//		{
//			var p = (Vector2) point.transform.position;
//
//			var y = Evaluate(p.x);
//
//			var error = p.y - y;
//
//			error *= error;
//
//			value += error;
//		}
//
//		return value;
//	}
//
//    public float Evaluate(float x)
//	{
//		Input.Value = x;
//
//		foreach (var layer in Layers)
//		{
//			foreach (var node in layer.Nodes)
//			{
//				node.Forward();
//			}
//		}
//
//		for(int i = Layers.Length - 2; i >= 0; i--)
//		{
//			for(int p = 0; p < Layers[i].Nodes.Count; p++)
//			{
//				Layers[i].Nodes[p].Backward(); 
//			}
//		}
//
//		return Output.Value;
//	}
//
//	public void OnDrawGizmos()
//	{
//		if(!Preview)
//		{
//			return;
//		}
////		return;
//
//		Gizmos.color = Color.yellow;
//		Gizmos.DrawLine(Vector2.zero, new Vector2(100, 0));
//
//		Gizmos.color = Color.white;
//
//		var iter = 1280;
//        var last = Vector2.zero;
//		
//		for (int i = 0; i < iter; i++) 
//		{
//			var t = i / (float) iter;
//			
//			var x = t * 140;
//			
//			var y = Evaluate(x);
//
//            var p = new Vector2(x, y);
//			
//			if(i > 0)
//			{
//                Gizmos.DrawLine(last, p);
//			}
//			
//			last = p;
//		}
//	}
}