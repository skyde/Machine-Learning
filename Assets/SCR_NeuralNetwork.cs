using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class NetworkLayer
{
	public GameObject LayerBase;
	public SCR_Node[] Nodes;
}

[ExecuteInEditMode]
public class SCR_NeuralNetwork : MonoBehaviour 
{
    public SCR_Node Input;
    public SCR_Node Output;
	public NetworkLayer[] Layers;

	public float Step = 0.01F;
	public GameObject Connection;

	public bool AutoFit = true;

	DATA_Point[] Points;
	SCR_Node[] Nodes;
	SCR_Connection[] Connections;

	public void Awake()
	{
		Points = GameObject.FindObjectsOfType<DATA_Point>();
		Nodes = GameObject.FindObjectsOfType<SCR_Node>();

		if(Application.isPlaying)
		{
			for (int i = 0; i < Layers.Length - 1; i++)
			{
				var currentLayer = Layers[i];
				var nextLayer = Layers[i + 1];

				for (int c = 0; c < currentLayer.Nodes.Length; c++)
				{
					for (int n = 0; n < nextLayer.Nodes.Length; n++)
					{
						var previous = currentLayer.Nodes[c];
						var next = nextLayer.Nodes[n];

						var obj = GameObject.Instantiate(Connection);

						var connection = obj.GetComponent<SCR_Connection>();

						connection.Previous = previous;
						connection.Next = next;

						obj.transform.parent = transform;
					}
				}
			}
		}

		Connections = GameObject.FindObjectsOfType<SCR_Connection>();

		foreach (var layer in Layers) 
		{
			layer.Nodes = layer.LayerBase.GetComponentsInChildren<SCR_Node>();
		}

		foreach (var item in Nodes) 
		{
			if(item is SCR_NodeMultiply)
			{
				var node = (SCR_NodeMultiply) item;

				node.Bias = Random.value - 0.5F;
			}
		}

		foreach (var item in Connections) 
		{
			item.Weight = Random.value - 0.5F;
		}
	}

	public void Update()
	{
		if(!Application.isPlaying || !AutoFit)
		{
			return;
		}

		foreach (var item in Nodes) 
		{
			if(item is SCR_NodeMultiply)
			{
				var node = (SCR_NodeMultiply) item;

				var lastError = CaculateError();
				var lastValue = node.Bias;

				node.Bias += (Random.value - 0.5F) * Step;

				if(CaculateError() > lastError)
				{
					node.Bias = lastValue;
				}
			}
		}

		foreach (var item in Connections) 
		{
			var lastError = CaculateError();
			var lastValue = item.Weight;

			item.Weight += (Random.value - 0.5F) * Step;

			if(CaculateError() > lastError)
			{
				item.Weight = lastValue;
			}
		}

//		foreach (var item in Points) {
//			
//		}
	}

	public float CaculateError()
	{
		var value = 0F;

		foreach (var point in Points) 
		{
			var p = (Vector2) point.transform.position;

			var y = Evaluate(p.x);

			var error = p.y - y;

			error *= error;

			value += error;
		}

		return value;
	}

    public float Evaluate(float x)
	{
        Input.Value = x;

		foreach (var layer in Layers)
		{
			foreach (var node in layer.Nodes)
			{
				node.Value = node.Forward();	
				node.Activated = node.TransformOutput(node.Value);
			}
		}

        return Output.Value;
	}

	public void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(Vector2.zero, new Vector2(100, 0));

		Gizmos.color = Color.white;

		var iter = 1280;
        var last = Vector2.zero;
		
		for (int i = 0; i < iter; i++) 
		{
			var t = i / (float) iter;
			
			var x = t * 140;
			
			var y = Evaluate(x);

            var p = new Vector2(x, y);
			
			if(i > 0)
			{
                Gizmos.DrawLine(last, p);
			}
			
			last = p;
		}
		
		
		//		Gizmos.DrawLine(new Vector2(0, B), new Vector2(1000, B + M * 1000));
	}
//	public SCR_Neuron Input;
//	//	public SCR_Neuron Y;
//
//	public SCR_Neuron Output;
//	//	public SCR_Neuron OutputBlue;
//
//	DATA_Point[] Points;
//
//	public NeuralNetworkLayer[] Layers;
//
//	public float Step = 0.1F;
//
//	public void Awake()
//	{
//		Refresh();
//	}
//
//	public void Refresh()
//	{
//		Points = GameObject.FindObjectsOfType<DATA_Point>();
//
//		var neurons = GameObject.FindObjectsOfType<SCR_Neuron>();
//
//		var numLayers = 0;
//
//		foreach (var neuron in neurons)
//		{
//			var length = neuron.Layer + 1;
//
//			if(length > numLayers)
//			{
//				numLayers = length;
//			}
//		}
//
//		Layers = new NeuralNetworkLayer[numLayers];
//
//		for (int i = 0; i < Layers.Length; i++)
//		{
//			Layers[i] = new NeuralNetworkLayer();
//			Layers[i].Neurons = neurons.Where(_ => _.Layer == i).ToArray();
//		}
//	}
//
//	public void Update () 
//	{
//		foreach (var layer in Layers)
//		{
//			foreach (var neuron in layer.Neurons) 
//			{
//				{
//					var lastCost = TotalSquaredDistance();
//					var lastValue = neuron.Bias;
//
//					neuron.Bias += (Random.value - 0.5F) * Step;
//
//					var newCost = TotalSquaredDistance();
//
//					if(newCost > lastCost)
//					{
//						neuron.Bias = lastValue;
//					}
//				}
//
//				for (int i = 0; i < neuron.PreviousWeights.Length; i++)
//				{
//					var lastCost = TotalSquaredDistance();
//					var lastValue = neuron.PreviousWeights[i];
//
//					neuron.PreviousWeights[i] += (Random.value - 0.5F) * Step;
//
//					var newCost = TotalSquaredDistance();
//
//					if(newCost > lastCost)
//					{
//						neuron.PreviousWeights[i] = lastValue;
//					}
//				}
//			}	
//		}
//		//		for (int p = 0; p < Points.Length; p++)
//		//		{
//		//			var point = Points[p];
//		//
//		////			point.
//		//
//		////			point.transform.position
//		////        	point.transform.position
//		//		}
//	}
//
//	public float TotalSquaredDistance()
//	{
//		float total = 0;
//
//		foreach (var point in Points)
//		{
//			var t = Evaulate(point.transform.position);
//
//			//			if(point.Type == PointType.Red)
//			//			{
//			//				t = -t;
//			//			}
//
//			//			var t = point.Type == PointType.Red ? v : v;
//
//			total += t * t;
//		}
//
//		return total;
//	}
//
//	public float Evaulate(Vector2 p)
//	{
//		Input.Current = p.x;
//
//		for (int l = 1; l < Layers.Length; l++) 
//		{
//			var layer = Layers[l];
//
//			for (int n = 0; n < layer.Neurons.Length; n++) 
//			{
//				var neuron = layer.Neurons[n];
//
//				neuron.Evaulate();
//			}
//		}
//
//		return Output.Current;//OutputBlue.Current);
//	}
//
//	public void OnValidate()
//	{
//		Refresh();
//	}
//
//	const int GizmoIterations = 32;
//	protected GizmoData[,] GizmoDatas = new GizmoData[GizmoIterations, GizmoIterations];
//
//	public void OnDrawGizmos()
//	{
//		//		const float area = 10;
//		//
//		//		var size = (area / (GizmoIterations - 1));
//		//
//		//		for (int x = 0; x < GizmoIterations; x++)
//		//		{
//		//			var xPos = -(x / (1F - (float) GizmoIterations)) * area;
//		//
//		//			for (int y = 0; y < GizmoIterations; y++)
//		//			{
//		//				var yPos = -(y / (1F - (float) GizmoIterations)) * area;
//		//
//		//				var p = new Vector2(xPos, yPos);
//		//
//		//				var t = Evaulate(p);
//		//
//		//
//		//				GizmoDatas[x, y].Position = p;
//		//				GizmoDatas[x, y].T = t;
//		//			}
//		//		}
//		//
//		//		var min = float.MaxValue;
//		//		var max = float.MinValue;
//		//
//		////		print(min + " " + max);
//		//
//		//		foreach (var item in GizmoDatas) 
//		//		{
//		//			if(item.T.x < min)
//		//			{
//		//				min = item.T.x;
//		//			}
//		//			if(item.T.x > max)
//		//			{
//		//				max = item.T.x;
//		//			}
//		//		}
//		//
//		//		foreach (var item in GizmoDatas) 
//		//		{
//		//			Gizmos.color = Color.Lerp(Color.yellow, Color.blue, Mathf.InverseLerp(min, max, item.T.x));
//		//
//		//			Gizmos.DrawWireCube(item.Position, new Vector2(size, size));
//		//		}
//		//
//		//
//		//		// Line
//		Gizmos.color = Color.white;
//
//		var iter = 512;
//		var last = Vector2.zero;
//
//		for (int i = 0; i < iter; i++) 
//		{
//			var t = i / (float) iter;
//
//			var x = t * 14;
//
//			var y = Evaulate(new Vector2(x, 0));
//
//			var p = new Vector2(x, y);
//
//			if(i > 0)
//			{
//				Gizmos.DrawLine(last, p);
//			}
//
//			last = p;
//		}
//	}
}