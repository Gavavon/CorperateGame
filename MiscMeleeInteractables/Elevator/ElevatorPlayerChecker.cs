using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class ElevatorPlayerChecker : MonoBehaviour
{
	private ElevatorHandler parentElevator;

	private void Start()
	{
		parentElevator = GetComponentInParent<ElevatorHandler>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			parentElevator.isPlayerInside = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			parentElevator.isPlayerInside = false;
			/*
			if (!parentElevator.canChangeScenes) 
			{
				Task.Run(() => parentElevator.CloseWhenAvailable());
			}
			*///this for having the elevator close behind the player
		}
	}
}
