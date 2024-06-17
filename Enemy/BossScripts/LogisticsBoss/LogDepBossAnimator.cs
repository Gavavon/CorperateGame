using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LogDepBossAnimator : MonoBehaviour
{
	private NavMeshAgent agent;
	private Animator animator;
	private LogDepBossActions actions;

	// animation IDs
	private int animIDGVel;
	private int animIDActivate;
	private int animIDFloat;

	// Start is called before the first frame update
	void Start()
	{
		actions = GetComponent<LogDepBossActions>();
		animator = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		AssignAnimationIDs();
	}

	void FixedUpdate()
	{
		animator.SetFloat(animIDGVel, agent.velocity.magnitude);
	}

	public void AnimateSecondPhase() 
	{
		animator.SetBool(animIDFloat, true);
	}

	public void AnimateBossActivation()
	{
		animator.SetBool(animIDActivate, true);
	}

	private void AssignAnimationIDs()
	{
		animIDGVel = Animator.StringToHash("GenVelocity");
		animIDFloat = Animator.StringToHash("Floating");
		animIDActivate = Animator.StringToHash("Activate");
	}
}
