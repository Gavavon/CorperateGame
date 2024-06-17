using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyGunHandler;

public class AiAnimator : MonoBehaviour
{
	private NavMeshAgent agent;
	private Animator animator;
	private AiActions actions;

	[SerializeField]
	[Range(0, 35)]
	private int setTask;

	// animation IDs
	private int animIDGVel;
	private int animIDXVel;
	private int animIDYVel;
	private int animIDTaskNum;
	private int animIDDoingTask;
	private int animIDAlerted;
	private int animIDGunChoice;
	private int animIDReload;
	private int animIDMeleeUser;
	private int animIDisBlocking;
	private int animIDisStunned;
	private int animIDisAttacking;
	private int animIDisCharging;

	// Start is called before the first frame update
	void Start()
	{
		actions = GetComponent<AiActions>();
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

	public float GetAnimationLength() 
	{
		return animator.GetCurrentAnimatorStateInfo(0).length;
	}

	public void AnimateBlock() 
	{
		animator.SetBool(animIDisStunned, false);
		animator.SetBool(animIDisAttacking, false);
		animator.SetBool(animIDisCharging, false);

		animator.SetBool(animIDisBlocking, true);
	}
	public void AnimateStun()
	{
		animator.SetBool(animIDisBlocking, false);
		animator.SetBool(animIDisAttacking, false);
		animator.SetBool(animIDisCharging, false);

		animator.SetBool(animIDisStunned, true);
	}
	public void AnimateAttack()
	{
		animator.SetBool(animIDisStunned, false);
		animator.SetBool(animIDisBlocking, false);
		animator.SetBool(animIDisCharging, false);

		animator.SetBool(animIDisAttacking, true);
	}

	public void AnimateCharge()
	{
		animator.SetBool(animIDisStunned, false);
		animator.SetBool(animIDisBlocking, false);
		animator.SetBool(animIDisAttacking, false);

		animator.SetBool(animIDisCharging, true);
	}

	public void AnimateIdle()
	{
		animator.SetBool(animIDisStunned, false);
		animator.SetBool(animIDisBlocking, false);
		animator.SetBool(animIDisAttacking, false);
		animator.SetBool(animIDisCharging, false);
	}

	public IEnumerator RunReloadAnimation()
	{
		animator.SetBool(animIDReload, true);
		yield return new WaitForSeconds(1);
		animator.SetBool(animIDReload, false);
	}

	public void SwitchCombatIn(EnemyGunHandler.WeaponType weaponType = WeaponType.Handgun) 
	{
		if (actions.aiType == AiAgentConfig.AiBehaviors.Chargers)
		{
			animator.SetBool(animIDMeleeUser, true);
			return;
		}
		/*
		 * Assault Rifle: 1
		 * Handgun: 2
		 * Shotgun: 3
		 * SMG: 4
		 * Sniper: 5
		 */
		switch (weaponType)
		{
			case EnemyGunHandler.WeaponType.AssaultRifle:
				animator.SetFloat(animIDGunChoice, 1);
				break;
			case EnemyGunHandler.WeaponType.Handgun:
				animator.SetFloat(animIDGunChoice, 2);
				break;
			case EnemyGunHandler.WeaponType.Shotgun:
				animator.SetFloat(animIDGunChoice, 3);
				break;
			case EnemyGunHandler.WeaponType.SMG:
				animator.SetFloat(animIDGunChoice, 4);
				break;
			case EnemyGunHandler.WeaponType.Sniper:
				animator.SetFloat(animIDGunChoice, 5);
				break;
		}

		animator.SetBool(animIDAlerted, true);

	}

	private void AssignAnimationIDs()
	{
		animIDGVel = Animator.StringToHash("GenVelocity");
		animIDXVel = Animator.StringToHash("XVelocity");
		animIDYVel = Animator.StringToHash("YVelocity");
		animIDTaskNum = Animator.StringToHash("TaskNum");
		animIDDoingTask = Animator.StringToHash("DoingTask");
		animIDAlerted = Animator.StringToHash("Alerted");
		animIDGunChoice = Animator.StringToHash("GunChoice");
		animIDReload = Animator.StringToHash("Reload");

		animIDMeleeUser = Animator.StringToHash("MeleeUser");
		animIDisBlocking = Animator.StringToHash("isBlocking");
		animIDisStunned = Animator.StringToHash("isStunned");
		animIDisAttacking = Animator.StringToHash("isAttacking");
		animIDisCharging = Animator.StringToHash("isCharging");
	}
}
