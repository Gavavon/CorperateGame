using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class VehicleAIMovement : MonoBehaviour
{

    public Transform Destination;
	
	private Animator _animator;

	private NavMeshAgent agent;

	private int _animIDWheels;

	// Start is called before the first frame update
	void Start()
    {
		agent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();

		_animIDWheels = Animator.StringToHash("Moving");

		_animator.SetBool(_animIDWheels, true);

		agent.destination = Destination.position;
	}

	private void OnTriggerEnter(Collider collision)
	{
		switch (collision.gameObject.tag) 
		{
			case "Vehicle":
				StopVehicle();
				break;
			case "Pedestrian":
				StopVehicle();
				break;
		}
	}

	private void OnTriggerExit(Collider collision)
	{
		switch (collision.gameObject.tag)
		{
			case "Vehicle":
				StartVehicle();
				break;
			case "Pedestrian":
				StartVehicle();
				break;
		}
	}

	public void StopVehicle() 
	{
		agent.isStopped = true;
		_animator.SetBool(_animIDWheels, false);
	}

	public void StartVehicle() 
	{
		agent.isStopped = false;
		_animator.SetBool(_animIDWheels, true);
	}
	public void SetVehicleDestination(Transform Dest)
	{
		agent.destination = Dest.position;
	}
}
