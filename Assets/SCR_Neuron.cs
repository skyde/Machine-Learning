using UnityEngine;
using System.Collections;
using System.Linq;

public class Connection : MonoBehaviour
{
	public SCR_Neuron Source;
	public SCR_Neuron Target;
}

public class SCR_Neuron : MonoBehaviour
{
	public int Feature;
	public int Layer;

	public float Weight;
	public float Bias;

	public SCR_Neuron[] Sources = new SCR_Neuron[0];
	public SCR_Neuron[] Targets = new SCR_Neuron[0];

	public void Awake()
	{
		var all = SCR_Neuron.FindObjectsOfType<SCR_Neuron>();

		Sources = all.Where(_ => _.Layer == Layer - 1).ToArray();
		Targets = all.Where(_ => _.Layer == Layer + 1).ToArray();
	}

	public void OnDrawGizmos()
	{
		var x = Layer;
		var y = Feature;
	}
}
