using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class BigSniperActions : MonoBehaviour
{

	private BigSniperAnimator animator;
	[SerializeField]
	private EnemyGunHandler gun;

	private AiActions genericActions;

	private bool swapped = false;


	void Start()
	{
		animator = GetComponent<BigSniperAnimator>();
		genericActions = GetComponent<AiActions>();
	}

	public void ShootGun() 
	{
		genericActions.ShootGunGetter();
	}

	public void CombatSwitcher(bool switchTo)
	{
		switch (switchTo)
		{
			case true:
				genericActions.LookAtPlayer();
				genericActions.PlayerInRange();
				UpdateAnimatorForCombat();
				ChangeEnemyCombatComponent(true);
				break;
			case false:
				ChangeEnemyCombatComponent(false);
				break;
		}
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
		gun.gameObject.SetActive(x);

		GetComponent<AiSensor>().enabled = x;
		GetComponent<WeaponIK>().enabled = x;
		GetComponent<AiBehaviourTreeHandler>().enabled = x;
	}
}
