using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeCarMaterial : MonoBehaviour
{
	public Material[] VehicleMats;
	private void Start()
	{
		this.GetComponent<Renderer>().material = VehicleMats[Random.Range(0, VehicleMats.Length)];
	}
}
