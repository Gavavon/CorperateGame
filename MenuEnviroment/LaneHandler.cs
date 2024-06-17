using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneHandler : MonoBehaviour
{
	public bool CanPass = false;

	private void OnTriggerEnter(Collider collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Vehicle":
				collision.GetComponent<VehicleAIMovement>().StopVehicle();
				break;
			case "Pedestrian":
				collision.GetComponent<PedestrianAIMovement>().StopPedestrian();
				break;
		}
	}

	private void OnTriggerStay(Collider collision)
	{
		if (CanPass) 
		{
			switch (collision.gameObject.tag)
			{
				case "Vehicle":
					collision.GetComponent<VehicleAIMovement>().StartVehicle();
					break;
				case "Pedestrian":
					collision.GetComponent<PedestrianAIMovement>().StartPedestrian();
					break;
			}
		}
		
	}
}
