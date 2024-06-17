using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSideCollider : MonoBehaviour
{
	private DoorHandler door;

	private void Start()
	{
		door = this.gameObject.GetComponentInParent<DoorHandler>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player")
		{
			door.canPunchDoor = true;
		}
	}
}
