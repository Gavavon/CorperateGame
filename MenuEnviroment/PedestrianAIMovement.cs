using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PedestrianAIMovement : MonoBehaviour
{
	public Transform Destination;

	private Animator _animator;

	private NavMeshAgent agent;

	private int _animIDWalk;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		_animator = GetComponent<Animator>();

		_animIDWalk = Animator.StringToHash("Speed");

		agent.destination = Destination.position;
	}

	public void Update()
	{
		_animator.SetFloat(_animIDWalk, agent.velocity.magnitude);
	}

	public void StopPedestrian()
	{
		agent.isStopped = true;
	}

	public void StartPedestrian()
	{
		agent.isStopped = false;
	}

	public void SetPedestrianDestination(Transform Dest)
	{
		agent.destination = Dest.position;
	}
}
