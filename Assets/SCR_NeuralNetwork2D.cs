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

			X.Current = point.transform.position.x;
			Y.Current = point.transform.position.y;

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
	}
}
