using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static AiAgentConfig;
using static CameraTargetController;
using InfimaGames.LowPolyShooterPack;
using UnityEngine.InputSystem;
using TheKiwiCoder;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using MoreMountains.Feedbacks;
using UnityEditor.Rendering;
using UnityEngine.AI;
using System;
using PixelCrushers;
using UnityEngine.UIElements;
using MoreMountains.Tools;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.Windows;
using UnityEngine.SceneManagement;

public class AiActions : MonoBehaviour
{
	#region Declarations
	[SerializeField]
	public AiBehaviors aiType = AiBehaviors.None;

	[Header("Alert References")]
	[SerializeField]
	private GameObject mark;
	[Header("Weapon References")]
	[SerializeField]
	private EnemyGunHandler gun;
	[SerializeField]
	private GameObject meleeWeapon;
	[SerializeField]
	private GameObject shield;

	[Header("Feedback References")]
	[SerializeField]
	private MMFeedbacks damageFeedback;

	[HideInInspector]
	[SerializeField]
	public RoomTrigger room;

	private bool dead = false;

	[HideInInspector]
	[SerializeField]
	public bool recentlyTakenDamage = false;

	private bool resetDamageTakenCooldown = false;

	[Title(label: "Attacker specific actions")]

	[Header("Gun Cooldowns")]
	public float shootCooldown = 0.5f;

	[Header("Close Distance Chances")]
	public float percentToMove = 40;
	public float movementCheckCooldown = 0.5f;

	[Header("Long Distance Chances")]
	//cool down between taking a group of pot shots
	public float coolDownBetweenPotShots = 3f;
	//percent chance the enemy will take pot shots
	public float percentTakePotShots = 80;
	//updated spread because of distance
	public float potShotSpread = 1;
	//cool down between each shot taken
	public float potShotCoolDownBetweenShots = 0.5f;
	//max amount of pot shots taken at one time
	public int potShotMax = 4;
	//min amount of pot shots taken at one time
	public int potShotMin = 2;

	[Title(label: "Charger specific actions")]

	//[HideInInspector]
	//[SerializeField]
	//public bool swordCanDealDamage = false;

	[HideInInspector]
	[SerializeField]
	public bool recentlyCharging = false;

	[Header("Charge Node")]
	public float speedUpTime = 1f;

	[Header("Cooldowns")]
	[SerializeField]
	private float stunCooldown = 2f;

	private bool stunStarted = false;

	/* Enemy damage cooldown obsolete
	[HideInInspector]
	[SerializeField]
	private float damageCooldown = 0.01f;
	protected float lastDamageTakenAt = -1f;
	*/

	private float currentHealth = 1;

	[HideInInspector]
	[SerializeField]
	public AiAnimator animationController;

	[HideInInspector]
	[SerializeField]
	public AiAgent aiAgent;

	[HideInInspector]
	[SerializeField]
	public float navMeshSpeed = 4;

	[HideInInspector]
	[SerializeField]
	private bool canShoot = true;
	[HideInInspector]
	[SerializeField]
	private bool canShootCooldown = true;
	[HideInInspector]
	[SerializeField]
	private bool canCheckMove = true;
	[HideInInspector]
	[SerializeField]
	public bool stopDurationDone = true;
	[HideInInspector]
	[SerializeField]
	private bool alerted = false;
	#endregion
	#region Unity

	private void Start()
	{
		//newActionSet = Convert.ChangeType(newActionSet, actionComponents[0]);
		currentHealth = GetComponent<AiAgent>().config.maxHealth;
		animationController = GetComponent<AiAnimator>();
		aiAgent = GetComponent<AiAgent>();
		ResetMark();
	}

	#endregion

	#region public methods

	public void TakeDamage(float amount)
	{
		if (dead)
		{
			return;
		}

		if (room == null)
		{
			Debug.LogError("Enemy: " + gameObject.name + " did not properly have their room assigned");
		} //Room Debugger

		if (!alerted) 
		{
			AlertEnemy();
			room.AlertRoom();
		}

		if (recentlyTakenDamage)
		{
			resetDamageTakenCooldown = true;
		}
		else 
		{
			recentlyTakenDamage = true;
			StartCoroutine(TakenDamageRecentlyCoolDown(5));
		}

		/* Enemy damage cooldown obsolete
		//we make sure enough time has passed since the last time this enemy took damage
		if (Time.time - lastDamageTakenAt < damageCooldown)
		{
			return;
		}
		lastDamageTakenAt = Time.time;
		*/
		damageFeedback?.PlayFeedbacks(this.transform.position, amount);

		//Make sure projectile has a damage value set
		currentHealth -= amount;
		//healthBar.SetHealthBarPercentage(currentHealth/maxHealth);
		if (currentHealth <= 0)
		{
			Die();
			return;
		}
	}

	#region Clean Up
	public void DespawnBody()
	{
		StartCoroutine(RemoveBody());
	}
	#endregion

	#region Alert Enemy
	public void AlertEnemy()
	{
		if (dead) 
		{
			return;
		}
		if (alerted)
		{
			return;
		}
		if (GetComponentInChildren<ProvocationSensor>() != null) 
		{
			Destroy(GetComponentInChildren<ProvocationSensor>().gameObject);
		}
		aiAgent.stateMachine.ChangeState(AiStateId.Alerted);
	}

	public void EnemyAlerted()
	{
		//this.GetComponent<BoxCollider>().enabled = false;
		if (dead)
		{
			return;
		}
		alerted = true;
		StartCoroutine(ShowAlertSign());
		StartCoroutine(AlertReactionTime());
		
	}
	#endregion

	#region Combat Change
	public void EnterCombat()
	{
		if (dead)
		{
			return;
		}
		if (aiType == AiBehaviors.Unique)
		{
			//something in another script
			EnterUniqueCombat(true);
			return;
		}

		ChangeEnemyCombatComponent(true);
		UpdateAnimatorForCombat();
		aiAgent.navMeshAgent.SetDestination(this.transform.position);
	}

	public void ChangeEnemyState(AiStateId state)
	{
		aiAgent.stateMachine.ChangeState(state);
	}
	#endregion

	#region Attack

	public void ShootGunsGetter(List<EnemyGunHandler> guns)
	{
		//we can't run Coroutines in the behaviour tree so
		StartCoroutine(ShootGuns(guns));
	}

	public void ShootCertainGunGetter(EnemyGunHandler gun, bool useVariantSpread = false, float newSpread = 0.25f, bool coolDownOverride = false, float newCoolDown = 3f, bool multipleShots = false, int shotsAmount = 1, float secondsBetweenShots = 0.5f)
	{
		//we can't run Coroutines in the behaviour tree so
		StartCoroutine(ShootGun(useVariantSpread, newSpread, coolDownOverride, newCoolDown, multipleShots, shotsAmount, secondsBetweenShots, gun));
	}

	public void ShootGunGetter(bool useVariantSpread = false, float newSpread = 0.25f, bool coolDownOverride = false, float newCoolDown = 3f, bool multipleShots = false, int shotsAmount = 1, float secondsBetweenShots = 0.5f)
	{
		//we can't run Coroutines in the behaviour tree so
		StartCoroutine(ShootGun(useVariantSpread, newSpread, coolDownOverride, newCoolDown, multipleShots, shotsAmount, secondsBetweenShots));
	}

	public void ShootGunGetterWithCoolDown(float cooldown, bool useVariantSpread = false, float newSpread = 0.25f, bool coolDownOverride = false, float newCoolDown = 3f, bool multipleShots = false, int shotsAmount = 1, float secondsBetweenShots = 0.5f)
	{
		//we can't run Coroutines in the behaviour tree so
		StartCoroutine(ShootGunWithCoolDown(cooldown, useVariantSpread, newSpread, coolDownOverride, newCoolDown, multipleShots, shotsAmount, secondsBetweenShots));
	}

	public void SwordActivationGetter() 
	{
		StartCoroutine(ActivateSword());
		animationController.AnimateAttack();
	}

	public IEnumerator ReloadWeapon() 
	{
		if (aiType == AiBehaviors.Unique)
		{
			switch (true) 
			{
				case true when GetComponent<AkimboActions>() != null:
					StartCoroutine(GetComponent<AkimboActions>().SwapWeapons());
					yield break;
				case true when GetComponent<BigSniperActions>() != null:
					gun.SetIsReload(false);
					yield break;
			}
		}

		StartCoroutine(animationController.RunReloadAnimation());
		gun.PlayReloadSFX();
		yield return new WaitForSeconds(1.5f);
		gun.SetIsReload(false);
	}
	#endregion

	#region Checks
	public bool CheckForPlayer()
	{
		return aiAgent.sensor.IsInSight(aiAgent.playerTarget);
	}

	public void PlayerInRange()
	{
		aiAgent.weaponIK.SetTargetTransform(aiAgent.playerTarget.transform);
	}

	public void PlayerOutRange()
	{
		aiAgent.weaponIK.SetTargetTransformDefault();
	}
	#endregion

	#region Misc Node Actions
	public void StopForDurationGetter(Vector3 currentDestination, float waitTime) 
	{
		StartCoroutine(StopEnemy(currentDestination, waitTime));
	}

	public void MoveDecisionGetter()
	{
		//we can't run Coroutines in the behaviour tree so
		StartCoroutine(RandomMoveDecider());
	}

	public void SpeedUpGetter() 
	{
		StartCoroutine(StartChargeSpeedUp(speedUpTime));
	}

	public void LookAtPlayer() 
	{
		var p = aiAgent.player.position;
		p.y = transform.position.y;
		transform.LookAt(p);
	}

	public void StunEnemyCoolDownGetter() 
	{
		//find out what state we are in and return if we are currently stunned
		if (stunStarted) 
		{
			return;
		}
		stunStarted = true;
		StartCoroutine(StunCooldown());
	}
	#endregion

	#endregion

	#region private methods

	#region Take Damage
	//could be replaced with an async
	private IEnumerator TakenDamageRecentlyCoolDown(int waitTime) 
	{
		for (int i = 0; i < waitTime*2; i++) 
		{
			if (resetDamageTakenCooldown) 
			{
				resetDamageTakenCooldown = false;
				StartCoroutine(TakenDamageRecentlyCoolDown(5));
				yield break;
			}
			yield return new WaitForSeconds(0.5f);
		}
		
		recentlyTakenDamage = false;
	}
	private void Die()
	{
		EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
		enemyManager.RemoveSelfFromList(gameObject);
		aiAgent.player.GetComponent<PlayerIncomeHandler>().GiveMoney(UnityEngine.Random.Range(10,15));
		dead = true;
		if (aiType == AiBehaviors.Unique)
		{
			//something in another script
			EnterUniqueCombat(false);
			aiAgent.stateMachine.ChangeState(AiStateId.Death);
			return;
		}
		ChangeEnemyCombatComponent(false);
		aiAgent.stateMachine.ChangeState(AiStateId.Death);
	}
	#endregion

	#region Enter Combat
	private void UpdateAnimatorForCombat()
	{
		if (aiType == AiBehaviors.Chargers)
		{
			animationController.SwitchCombatIn();
		}
		else
		{
			animationController.SwitchCombatIn(gun.wepType);
		}
		animationController.DoneWithTask();
	}

	private void ChangeEnemyCombatComponent(bool x)
	{
		if (aiType == AiBehaviors.Chargers)
		{
			meleeWeapon.gameObject.SetActive(x);
			shield.gameObject.SetActive(x);
		}
		else 
		{
			gun.gameObject.SetActive(x);
		}
		
		aiAgent.navMeshAgent.enabled = x;
		GetComponent<AiSensor>().enabled = x;
		GetComponent<WeaponIK>().enabled = x;
		GetComponent<AiBehaviourTreeHandler>().enabled = x;
	}

	private void EnterUniqueCombat(bool OnOff)
	{
		switch (true)
		{
			case true when GetComponent<AkimboActions>() != null:
				GetComponent<AkimboActions>().CombatSwitcher(OnOff);
				return;
			case true when GetComponent<BigSniperActions>() != null:
				GetComponent<BigSniperActions>().CombatSwitcher(OnOff);
				return;
		}
	}

	#endregion

	#region Clean Up
	private IEnumerator RemoveBody() 
	{
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}
	#endregion

	#region Alert
	private IEnumerator AlertReactionTime(bool overrideTime = false, float reactionTimeMax = 0f, float reactionTimeMin = 0.5f)
	{
		if (overrideTime)
		{
			yield return new WaitForSeconds(reactionTimeMin);
		}
		else
		{
			yield return new WaitForSeconds(UnityEngine.Random.Range(reactionTimeMin, reactionTimeMax));
		}

		/*
		switch (aiAgent.config.enemyBehaviors)
		{
			case AiBehaviors.None:
				Debug.Log("Enemy Does not have Behavior Assigned");
				break;
			case AiBehaviors.ProvokableWorkers:
				//doesn't get alerted by player entering room
				break;
			case AiBehaviors.Attackers:
				//gets alerted right away
				break;
			case AiBehaviors.Defenders:
				//later
				break;
			case AiBehaviors.Suppressors:
				//later
				break;
			case AiBehaviors.Charger:
				//later
				break;
		}
		*/

		if (dead)
		{
			yield return null;
		}

		//based on what type of enemy it is they may enter other states after being alerted
		ChangeEnemyState(AiStateId.Combat);
	}

	private IEnumerator ShowAlertSign()
	{
		mark.SetActive(true);
		mark.transform.DOLocalMove(new Vector3(0, 2.1f, 0), 0.5f);
		//play sound effect
		yield return new WaitForSeconds(1f);
		ResetMark();
	}

	private void ResetMark()
	{
		mark.SetActive(false);
		mark.transform.localPosition = new Vector3(0, 1.5f, 0);
	}

	#endregion

	#region Attack
	private IEnumerator ShootGunWithCoolDown(float cooldown, bool useVariantSpread = false, float newSpread = 0.25f, bool coolDownOverride = false, float newCoolDown = 3f, bool multipleShots = false, int shotsAmount = 1, float secondsBetweenShots = 0.5f)
	{
		//we can't run Coroutines in the behaviour tree so
		if (!canShootCooldown)
		{
			yield break;
		}
		canShootCooldown = false;
		if (aiAgent.config.moveForwardPotShotStop)
		{
			StopForDurationGetter(aiAgent.navMeshAgent.destination, secondsBetweenShots * shotsAmount);
		}
		StartCoroutine(ShootGun(useVariantSpread, newSpread, coolDownOverride, newCoolDown, multipleShots, shotsAmount, secondsBetweenShots));
		yield return new WaitForSeconds(cooldown + (secondsBetweenShots * shotsAmount));
		canShootCooldown = true;
	}
	private IEnumerator ShootGun(bool useVariantSpread, float newSpread, bool coolDownOverride, float newCoolDown, bool multipleShots, int shotsAmount, float secondsBetweenShots, EnemyGunHandler gun = null)
	{
		if (!canShoot)
		{
			yield break;
		}
		canShoot = false;
		if (gun == null) 
		{
			gun = this.gun;
		}
		if (multipleShots)
		{
			for (int i = 0; i < shotsAmount; i++)
			{
				if (useVariantSpread)
				{
					gun.ShootGun(true, newSpread);
				}
				else
				{
					gun.ShootGun();
				}
				yield return new WaitForSeconds(secondsBetweenShots);
			}
		}
		else
		{
			if (useVariantSpread)
			{
				gun.ShootGun(true, newSpread);
			}
			else
			{
				gun.ShootGun();
			}
		}
		if (coolDownOverride)
		{
			yield return new WaitForSeconds(newCoolDown);
			canShoot = true;
			yield break;
		}
		yield return new WaitForSeconds(shootCooldown);
		canShoot = true;
	}

	private IEnumerator ShootGuns(List<EnemyGunHandler> guns)
	{
		if (!canShoot)
		{
			yield break;
		}
		canShoot = false;
		for (int i = 0; i < guns.Count; i++)
		{
			guns[i].ShootGun();
		}
		yield return new WaitForSeconds(shootCooldown);
		canShoot = true;
	}
	private IEnumerator ActivateSword() 
	{
		yield return new WaitForSeconds(0.3f);
		recentlyCharging = false;
	}
	#endregion

	#region Misc Node Actions
	private IEnumerator RandomMoveDecider()
	{
		if (!canCheckMove)
		{
			yield break;
		}
		canCheckMove = false;
		//perecent chance we change position
		if (UnityEngine.Random.Range(0, 1f) <= percentToMove / 100)
		{
			Vector3 crossTemp = Vector3.Cross(transform.position - aiAgent.player.position, new Vector3(0, 1, 0)).normalized;

			for (int i = 0; i < 5; i++)
			{
				int randomTemp = UnityEngine.Random.Range(-6, 6);

				if (room.roomCollider.bounds.Contains((randomTemp * crossTemp) + transform.position))
				{
					aiAgent.navMeshAgent.SetDestination((randomTemp * crossTemp) + transform.position);
					break;
				}
			}
		}
		else
		{
			aiAgent.navMeshAgent.SetDestination(transform.position);
		}
		//randomly decide if the player is going to move after a certain amount of time
		yield return new WaitForSeconds(movementCheckCooldown);
		canCheckMove = true;
	}

	private IEnumerator StopEnemy(Vector3 currentDestination, float waitTime)
	{
		if (!stopDurationDone)
		{
			yield break;
		}
		stopDurationDone = false;
		aiAgent.navMeshAgent.destination = this.transform.position;
		yield return new WaitForSeconds(waitTime);
		aiAgent.navMeshAgent.destination = currentDestination;
		stopDurationDone = true;
	}

	private IEnumerator StartChargeSpeedUp(float speedUpTime) 
	{
		if (aiAgent.navMeshAgent.speed > 4) 
		{
			yield break;
		}
		yield return new WaitForSeconds(speedUpTime / 2);
		aiAgent.navMeshAgent.speed = 4.5f;
		yield return new WaitForSeconds(speedUpTime / 2);
		aiAgent.navMeshAgent.speed = 5f;
	}

	private IEnumerator StunCooldown() 
	{
		yield return new WaitForSeconds(stunCooldown);
		stunStarted = false;
		ChangeEnemyState(AiStateId.Combat);
	}

	#endregion

	#endregion
}
