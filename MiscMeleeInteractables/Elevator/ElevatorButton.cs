using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorButton : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	public ElevatorHandler parentElevator;

	private void Start()
	{
		parentElevator = GetComponentInParent<ElevatorHandler>();
	}

	private void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Melee")
		{
			parentElevator.InteractWithElevator();
		}
	}
}
