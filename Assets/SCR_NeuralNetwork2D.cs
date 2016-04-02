using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class NeuralNetworkLayer
{
	public SCR_Neuron[] Neurons;
}

public struct GizmoData
{
	public Vector2 Position;
	public Vector2 T;
}

public class SCR_NeuralNetwork2D : MonoBehaviour 
{
	public SCR_Neuron X;
	public SCR_Neuron Y;

	public SCR_Neuron OutputRed;
	public SCR_Neuron OutputBlue;

	DATA_Point[] Points;

	public NeuralNetworkLayer[] Layers;

	public float Step = 0.1F;

	public void Awake()
	{
		Refresh();
	}

	public void Refresh()
	{
		Points = GameObject.FindObjectsOfType<DATA_Point>();

		var neurons = GameObject.FindObjectsOfType<SCR_Neuron>();

		var numLayers = 0;

		foreach (var neuron in neurons)
		{
			var length = neuron.Layer + 1;

			if(length > numLayers)
			{
				numLayers = length;
			}
		}

		Layers = new NeuralNetworkLayer[numLayers];

		for (int i = 0; i < Layers.Length; i++)
		{
			Layers[i] = new NeuralNetworkLayer();
			Layers[i].Neurons = neurons.Where(_ => _.Layer == i).ToArray();
		}
	}

	public void Update () 
	{
		foreach (var layer in Layers)
		{
			foreach (var neuron in layer.Neurons) 
			{
				{
					var lastCost = TotalSquaredDistance();
					var lastValue = neuron.Bias;

					neuron.Bias += (Random.value - 0.5F) * Step;

					var newCost = TotalSquaredDistance();

					if(newCost > lastCost)
					{
						neuron.Bias = lastValue;
					}
				}

				for (int i = 0; i < neuron.PreviousWeights.Length; i++)
				{
					var lastCost = TotalSquaredDistance();
					var lastValue = neuron.PreviousWeights[i];

					neuron.PreviousWeights[i] += (Random.value - 0.5F) * Step;

					var newCost = TotalSquaredDistance();

					if(newCost > lastCost)
					{
						neuron.PreviousWeights[i] = lastValue;
					}
				}
			}	
		}
//		for (int p = 0; p < Points.Length; p++)
//		{
//			var point = Points[p];
//
////			point.
//
////			point.transform.position
////        	point.transform.position
//		}
	}

	public float TotalSquaredDistance()
	{
		float total = 0;

		foreach (var point in Points)
		{
			var v = Evaulate(point.transform.position);

//			if(point.Type == PointType.Red)
//			{
//				t = -t;
//			}

			var t = point.Type == PointType.Red ? v.x : v.y;

			total += t * t;
		}

		return total;
	}

	public Vector2 Evaulate(Vector2 p)
	{
		if(X)
		{
			X.Current = p.x;
		}

		if(Y)
		{
			Y.Current = p.y;
		}

		for (int l = 1; l < Layers.Length; l++) 
		{
			var layer = Layers[l];

			for (int n = 0; n < layer.Neurons.Length; n++) 
			{
				var neuron = layer.Neurons[n];

				neuron.Evaulate();
			}
		}

		return new Vector2(OutputRed.Current, OutputBlue.Current);
	}

	public void OnValidate()
	{
		Refresh();
	}

	const int GizmoIterations = 32;
	protected GizmoData[,] GizmoDatas = new GizmoData[GizmoIterations, GizmoIterations];

	public void OnDrawGizmos()
	{
		const float area = 10;

		var size = (area / (GizmoIterations - 1));

		for (int x = 0; x < GizmoIterations; x++)
		{
			var xPos = -(x / (1F - (float) GizmoIterations)) * area;

			for (int y = 0; y < GizmoIterations; y++)
			{
				var yPos = -(y / (1F - (float) GizmoIterations)) * area;

				var p = new Vector2(xPos, yPos);

				var t = Evaulate(p);


				GizmoDatas[x, y].Position = p;
				GizmoDatas[x, y].T = t;
			}
		}

		var min = float.MaxValue;
		var max = float.MinValue;

//		print(min + " " + max);

		foreach (var item in GizmoDatas) 
		{
			if(item.T.x < min)
			{
				min = item.T.x;
			}
			if(item.T.x > max)
			{
				max = item.T.x;
			}
		}

		foreach (var item in GizmoDatas) 
		{
			Gizmos.color = Color.Lerp(Color.yellow, Color.blue, Mathf.InverseLerp(min, max, item.T.x));

			Gizmos.DrawWireCube(item.Position, new Vector2(size, size));
		}


		// Line
		Gizmos.color = Color.white;

		var iter = 128;
		var last = Vector2.zero;

		for (int i = 0; i < iter; i++) 
		{
			var t = i / (float) iter;

			var x = t * 14;

			var y = Evaulate(new Vector2(x, 0)).x;

			var p = new Vector2(x, y - 10);

			if(i > 0)
			{
				Gizmos.DrawLine(last, p);
			}

			last = p;
		}
	}
}