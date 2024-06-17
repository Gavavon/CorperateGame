using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestinationSender : MonoBehaviour
{

	public Transform Dest;

	private void OnTriggerEnter(Collider collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Vehicle":
				collision.GetComponent<VehicleAIMovement>().SetVehicleDestination(Dest);
				break;
			case "Pedestrian":
				collision.GetComponent<PedestrianAIMovement>().SetPedestrianDestination(Dest);
				break;
		}
	}
}
