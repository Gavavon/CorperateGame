using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyGunHandler;

public class AkimboAnimator : MonoBehaviour
{
	private NavMeshAgent agent;
	private Animator animator;
	private AkimboActions actions;

	[SerializeField]
	[Range(0, 35)]
	private int setTask;

	// animation IDs
	private int animIDXVel;
	private int animIDYVel;
	private int animIDTaskNum;
	private int animIDDoingTask;
	private int animIDDropGuns;
	private int animIDisAttacking;
	private int animIDCharging;

	// Start is called before the first frame update
	void Start()
	{
		actions = GetComponent<AkimboActions>();
		animator = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		AssignAnimationIDs();
		animator.SetFloat(animIDTaskNum, setTask);
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		//get enemy stats if the ai is alerted run correct animation

		if (agent.velocity.normalized.x > -0.05 && agent.velocity.normalized.x < 0.05)
		{
			animator.SetFloat(animIDXVel, 0);
		}
		else
		{
			animator.SetFloat(animIDXVel, agent.velocity.normalized.x);
		}

		if (agent.velocity.normalized.z > -0.05 && agent.velocity.normalized.z < 0.05)
		{
			animator.SetFloat(animIDYVel, 0);
		}
		else
		{
			animator.SetFloat(animIDYVel, agent.velocity.normalized.z);
		}
	}
	[ContextMenu("DebugTaskUpdater")]
	public void DebugTaskUpdater() 
	{
		animator.SetFloat(animIDTaskNum, setTask);
	}
	public void DoneWithTask()
	{
		animator.SetBool(animIDDoingTask, false);
	}
	public void AnimateThrow()
	{
		animator.SetBool(animIDDropGuns, true);
	}
	public void AnimateAttack()
	{
		animator.SetBool(animIDCharging, false);
		animator.SetBool(animIDisAttacking, true);
	}

	public void AnimateCharge()
	{
		animator.SetBool(animIDisAttacking, false);
		animator.SetBool(animIDCharging, true);
	}

	public void AnimateIdle()
	{
		animator.SetBool(animIDisAttacking, false);
		animator.SetBool(animIDCharging, false);
	}

	private void AssignAnimationIDs()
	{
		animIDXVel = Animator.StringToHash("XVelocity");
		animIDYVel = Animator.StringToHash("YVelocity");
		animIDTaskNum = Animator.StringToHash("TaskNum");
		animIDDoingTask = Animator.StringToHash("DoingTask");
		animIDDropGuns = Animator.StringToHash("DropGuns");

		animIDisAttacking = Animator.StringToHash("isAttacking");
		animIDCharging = Animator.StringToHash("Charging");
	}
}