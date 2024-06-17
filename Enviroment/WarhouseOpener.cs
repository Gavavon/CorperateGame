using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarhouseOpener : MonoBehaviour
{

	public MetalDoorHandler metalDoor;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			metalDoor.OpenMetalDoor();
		}
	}
}
