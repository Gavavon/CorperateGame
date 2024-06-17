using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayInteractor : MonoBehaviour
{
	public int RayRange = 5;

	public PlayerRayInteractor instance;

	void Awake()
	{
		instance = this;
	}

	void OnDrawGizmosSelected()
	{
		// Draws a 5 unit long red line in front of the object
		Gizmos.color = Color.green;
		Vector3 direction = transform.TransformDirection(Vector3.forward) * RayRange;
		Gizmos.DrawRay(transform.position, direction);
	}

	public void CheckInteractionRayCast() 
	{
		Debug.Log("Interacting");

		RaycastHit hit;
		if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, RayRange))
		{
			Transform objectHit = hit.transform;
			//if E is pressed and looking at certain objects different things happen


		}
	}
}
