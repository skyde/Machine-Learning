using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[System.Serializable]
public class NeuralNetworkLayer
{
	public SCR_Neuron[] Neurons;
}

public class SCR_NeuralNetwork2D : MonoBehaviour 
{
	public SCR_Neuron X;
	public SCR_Neuron Y;

	public SCR_Neuron OutputRed;
	public SCR_Neuron OutputBlue;

	DATA_Point[] Points;

//	public int NumLayers = 3;

	public NeuralNetworkLayer[] Layers;

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
		for (int p = 0; p < Points.Length; p++)
		{
			var point = Points[p];

//			point.transform.position
//        	point.transform.position
		}
	}

	public void Evaulate(Vector2 p)
	{
		X.Current = p.x;
		Y.Current = p.y;

		for (int l = 1; l < Layers.Length; l++) 
		{
			var layer = Layers[l];

			for (int n = 0; n < layer.Neurons.Length; n++) 
			{
				var neuron = layer.Neurons[n];

				neuron.Evaulate();
			}
		}
	}

	public void OnValidate()
	{
		Refresh();
	}

	public static float MinA = float.MaxValue;
	public static float MaxA = float.MinValue;

	public void OnDrawGizmos()
	{
		const int iter = 32;
		const float area = 10;

		var size = (area / (iter - 1));

//		var minA = 0F;
//		var maxA = 0F;

//		print(MinA + " " + MaxA);

		for (int x = 0; x < iter; x++)
		{
			var xPos = -(x / (1F - (float) iter)) * area;

			for (int y = 0; y < iter; y++)
			{
				var yPos = -(y / (1F - (float) iter)) * area;

				var p = new Vector2(xPos, yPos);

				X.Current = xPos;
				Y.Current = yPos;

				Evaulate(p);

				var t = OutputRed.Current;

//				print(t);

				if(t < MinA)
				{
					MinA = t;
				}

				if(t > MaxA)
				{
					MaxA = t;
				}

//				print(t);

				Gizmos.color = Color.Lerp(Color.yellow, Color.blue, Mathf.InverseLerp(MinA, MaxA, t));

				Gizmos.DrawWireCube(p, new Vector2(size, size));
			}
		}

//		MinA = minA;
//		MaxA = maxA;
	}
}
