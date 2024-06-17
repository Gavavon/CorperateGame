using DG.Tweening;
using MoreMountains.Feedbacks;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;
using static AiAgentConfig;

public class AkimboActions : MonoBehaviour
{
	private AkimboAnimator animator;
	[SerializeField]
	private EnemyGunHandler gun1;
	[SerializeField]
	private EnemyGunHandler gun2;
	[SerializeField]
	private List<EnemyGunHandler> guns;
	[SerializeField]
	private GameObject meleeWep;

	[SerializeField]
	private Transform hand;

	private NavMeshAgent agent;
	private AiActions genericActions;

	private bool swapped = false;

	void Start()
	{
		guns.Add(gun1);
		guns.Add(gun2);
		animator = GetComponent<AkimboAnimator>();
		genericActions = GetComponent<AiActions>();
	}

	public void ShootGuns() 
	{
		genericActions.ShootGunsGetter(guns);
	}
	public void SwingSword()
	{
		animator.AnimateAttack();
	}

	public bool CheckForSwap() 
	{
		return swapped;
	}

	public IEnumerator SwapWeapons()
	{
		swapped = true;
		animator.AnimateThrow();
		yield return new WaitForSeconds(0.2f);
		gun1.transform.parent = null;
		gun2.transform.parent = null;
		gun1.GetComponent<Rigidbody>().isKinematic = false;
		gun2.GetComponent<Rigidbody>().isKinematic = false;
		yield return new WaitForSeconds(0.4f);
		meleeWep.transform.parent = hand;
		meleeWep.transform.localPosition = new Vector3(0.0929f, 0.0444f, 0.0286f);
		meleeWep.transform.localRotation = Quaternion.Euler(-160.967f, -97.56799f, 80.344f);
	}

	public void CombatSwitcher(bool switchTo) 
	{
		switch (switchTo) 
		{
			case true:
				UpdateAnimatorForCombat();
				ChangeEnemyCombatComponent(true);
				break;
			case false:
				ChangeEnemyCombatComponent(false);
				break;
		}
	}

	public AkimboAnimator GetAnimator() 
	{
		return animator;
	}

	public AiActions GetGenericActions()
	{
		return genericActions;
	}

	private void UpdateAnimatorForCombat()
	{
		
		animator.DoneWithTask();
	}

	private void ChangeEnemyCombatComponent(bool x)
	{
		gun1.gameObject.SetActive(x);
		gun2.gameObject.SetActive(x);
		meleeWep.gameObject.SetActive(x);

		genericActions.aiAgent.navMeshAgent.enabled = x;
		GetComponent<AiSensor>().enabled = x;
		GetComponent<WeaponIK>().enabled = x;
		GetComponent<AiBehaviourTreeHandler>().enabled = x;
	}

}
