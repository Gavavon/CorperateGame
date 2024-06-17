using DG.Tweening;
using HighlightPlus;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TheKiwiCoder;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static CameraTargetController;

public class LogDepBossActions : MonoBehaviour
{
    private BossHealthSystem healthSystem;
	public float secondPhaseHealthCheck;
	private bool inSecondPhase = false;

	private bool dead = false;

	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public GameObject playerTarget;

	private Ragdoll ragdoll;
	private AiSensor sensor;
	private WeaponIK wepIK;
	[HideInInspector]
	public NavMeshAgent agent;
	private BossBehaviourTreeHandler treeRunner;
	private LogDepBossAnimator animator;

	public GameObject mysticArmsTop;
	public GameObject mysticArmsBottom;

	public int forcePush = 100;

	//===========SMG=============
	public BossGunHandler machinePistol1;
	private LogBossWeaponLookAt smgLook1;
	public BossGunHandler machinePistol2;
	private LogBossWeaponLookAt smgLook2;
	public int smgCooldown = 10;
	[HideInInspector]
	public bool canUseSMG = true;
	private bool canShootSMG = true;
	[SerializeField]
	private int shotsPerBurst = 30;
	[SerializeField]
	private float smgSecondsBetweenBurst = 3;
	[SerializeField]
	private float smgSecondsBetweenShots = 1;

	//===========Rocket=============
	public BossRocketLauncher rocketLauncher1;
	public BossRocketLauncher rocketLauncher2;
	public int missileCooldown = 15;
	[HideInInspector]
	public bool canUseMissile = true;

	//===========Rifle=============
	public BossGunHandler rifle;
	private LogBossWeaponLookAt rifleLook;
	private bool canShootRifle = true;
	[SerializeField]
	private float rifleSecondsBetweenShots = 1;

	//===========Rifle=============
	public BossGunHandler sniper;
	private LogBossWeaponLookAt sniperLook;
	private bool canShootSniper = true;
	[SerializeField]
	private float sniperSecondsBetweenShots = 1;

	//===========Rifle=============
	public BossGunHandler shotgun;
	private LogBossWeaponLookAt shotgunLook;
	private bool canShootShotgun = true;
	[SerializeField]
	private float shotgunSecondsBetweenShots = 1;

	public List<Transform> floatPoints = new List<Transform>();

	public List<Transform> movePoints = new List<Transform>();
	public int moveCooldown = 20;
	[HideInInspector]
	public bool canMove = true;

	public LogDepBossManager bossManager;

	private Quaternion myRotation = Quaternion.identity;

	private bool isActive = false;

	void Start()
    {
		playerTarget = GameObject.FindGameObjectsWithTag("PlayerTargetPoint")[0];
		player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		healthSystem = GetComponent<BossHealthSystem>();
		ragdoll = GetComponent<Ragdoll>();
		sensor = GetComponent<AiSensor>();
		wepIK = GetComponent<WeaponIK>();
		agent = GetComponent<NavMeshAgent>();
		treeRunner = GetComponent<BossBehaviourTreeHandler>();
		animator = GetComponent<LogDepBossAnimator>();


		smgLook1 = machinePistol1.GetComponent<LogBossWeaponLookAt>();
		smgLook2 = machinePistol2.GetComponent<LogBossWeaponLookAt>();
		rifleLook = rifle.GetComponent<LogBossWeaponLookAt>();
		sniperLook = sniper.GetComponent<LogBossWeaponLookAt>();
		shotgunLook = shotgun.GetComponent<LogBossWeaponLookAt>();
	}

	private void Update()
	{
		if (inSecondPhase && !dead) 
		{
			LookAtPlayer();
		}
	}

	public void ActivateBoss()
	{
		if (isActive)
		{
			return;
		}
		isActive = true;
		treeRunner.enabled = true;
		agent.enabled = true;
		agent.destination = this.transform.position;
		wepIK.enabled = true;
		wepIK.SetTargetTransform(playerTarget.transform);
		sensor.enabled = true;
		animator.AnimateBossActivation();
	}

	public void LookAtPlayer()
	{
		var p = player.position;
		p.y = transform.position.y;
		transform.LookAt(p);
	}

	#region checks
	public bool GetActive()
	{
		return isActive;
	}

	public bool CheckDead()
	{
		return dead;
	}

	public bool CheckSecondPhase()
	{
		return inSecondPhase;
	}

	#endregion

	#region Attacks

	//not working
	private void LookUpdater(int lookIndex) 
	{
		switch (lookIndex) 
		{
			case 0:
				rifleLook.StopLooking();
				smgLook1.StopLooking();
				smgLook2.StopLooking();
				sniperLook.StopLooking();
				shotgunLook.StopLooking();
				break;
			case 1:
				sniperLook.StartLooking();
				//Sniper Look
				smgLook1.StopLooking();
				smgLook2.StopLooking();
				rifleLook.StopLooking();
				shotgunLook.StopLooking();
				break;
			case 2:
				shotgunLook.StartLooking();
				//Shotgun Look
				smgLook1.StopLooking();
				smgLook2.StopLooking();
				rifleLook.StopLooking();
				sniperLook.StopLooking();
				break;
			case 3:
				rifleLook.StartLooking();
				//Rifle Look
				smgLook1.StopLooking();
				smgLook2.StopLooking();
				sniperLook.StopLooking();
				shotgunLook.StopLooking();
				break;
			case 4:
				smgLook1.StartLooking();
				smgLook2.StartLooking();
				//SMG Look
				rifleLook.StopLooking();
				sniperLook.StopLooking();
				shotgunLook.StopLooking();
				break;
		}
	}

	public void SniperAttack() 
	{
		LookUpdater(1);
		StartCoroutine(SniperShootRunner());
	}

	private IEnumerator SniperShootRunner() 
	{
		if (!canShootSniper)
		{
			yield break;
		}
		canShootSniper = false;
		sniper.ShootGun();
		yield return new WaitForSeconds(sniperSecondsBetweenShots);
		canShootSniper = true;
	}

	public void ShotgunAttack() 
	{
		LookUpdater(2);
		StartCoroutine(ShotgunShootRunner());
	}
	private IEnumerator ShotgunShootRunner()
	{
		if (!canShootShotgun)
		{
			yield break;
		}
		canShootShotgun = false;
		shotgun.ShootGun();
		yield return new WaitForSeconds(shotgunSecondsBetweenShots);
		canShootShotgun = true;
	}

	public void RifleAttack() 
	{
		LookUpdater(3);
		StartCoroutine(RifleShootRunner());
	}

	private IEnumerator RifleShootRunner()
	{
		if (!canShootRifle)
		{
			yield break;
		}
		canShootRifle = false;
		rifle.ShootGun();
		yield return new WaitForSeconds(rifleSecondsBetweenShots);
		canShootRifle = true;
	}

	public void SMGAttack() 
	{
		if (!canUseSMG) 
		{
			return;
		}
		if (inSecondPhase) 
		{
			canUseSMG = false;
			LookUpdater(4);
			StartCoroutine(SMGSprayRunner(true));
			Task.Run(() => SMGCooldownRunner());
		}
		StartCoroutine(SMGSprayRunner());
	}

	private IEnumerator SMGSprayRunner(bool ignoreBurstTimer = false)
	{
		if (!canShootSMG)
		{
			yield break;
		}
		canShootSMG = false;
		for (int i = 0; i < shotsPerBurst; i++) 
		{
			machinePistol1.ShootGun();
			machinePistol2.ShootGun();
			yield return new WaitForSeconds(smgSecondsBetweenShots);
		}
		if (ignoreBurstTimer) 
		{
			canShootSMG = true;
			yield break;
		}
		yield return new WaitForSeconds(smgSecondsBetweenBurst);
		canShootSMG = true;
	}

	async Task SMGCooldownRunner() 
	{
		await Task.Delay(smgCooldown * 1000);
		canUseSMG = true;
	}

	public void MissileAttack() 
	{
		if (!canUseMissile)
		{
			return;
		}
		canUseMissile = false;

		LookUpdater(0);

		rocketLauncher1.Fire();
		rocketLauncher2.Fire();

		Task.Run(() => MissileCooldownRunner());
	}
	async Task MissileCooldownRunner()
	{
		await Task.Delay(missileCooldown * 1000);
		canUseMissile = true;
	}

	#endregion

	#region Movement Nodes
	public void MoveBoss() 
	{
		if (!canMove) 
		{
			return;
		}
		canMove = false;

		agent.destination = movePoints[Random.Range(0, movePoints.Count - 1)].position;
		Task.Run(() => MoveBossRunner());
	}
	async Task MoveBossRunner()
	{
		await Task.Delay(moveCooldown * 1000);
		canMove = true;
	}
	#endregion

	#region Death and Damage
	public void TakeDamage(float damge)
	{
		healthSystem.TakeDamage(damge);
		if (healthSystem.currentHealth <= 0)
		{
			Died();
			return;
		}
		if (healthSystem.currentHealth < secondPhaseHealthCheck && !inSecondPhase)
		{
			ActivatePhase2();
		}
	}
	public void Died()
	{
		if (dead)
		{
			return;
		}
		dead = true;

		treeRunner.enabled = false;
		agent.enabled = false;
		wepIK.enabled = false;
		sensor.enabled = false;
		StartCoroutine(DeathSequence());
		bossManager.BossSlayed();
	}
	private IEnumerator DeathSequence()
	{
		mysticArmsTop.SetActive(false);
		mysticArmsBottom.SetActive(false);
		ragdoll.ActivateRagdoll();
		DropGuns();
		yield return new WaitForSeconds(0);
		StartCoroutine(RemoveBody());
	}

	private IEnumerator RemoveBody()
	{
		yield return new WaitForSeconds(10f);
		Destroy(gameObject);
	}
	#endregion

	#region Phase Change
	private void EquipFloatingGuns() 
	{
		machinePistol1.transform.parent = floatPoints[4];
		machinePistol1.transform.DOLocalMove(new Vector3(0, 0, 0), 2);
		machinePistol1.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f);

		machinePistol2.transform.parent = floatPoints[5];
		machinePistol2.transform.DOLocalMove(new Vector3(0, 0, 0), 2);
		machinePistol2.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f);

		rocketLauncher1.transform.parent = floatPoints[0];
		rocketLauncher1.playerTarget = playerTarget;
		rocketLauncher1.transform.DOLocalMove(new Vector3(0, 0, 0), 2);
		rocketLauncher1.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f);

		rocketLauncher2.transform.parent = floatPoints[1];
		rocketLauncher2.playerTarget = playerTarget;
		rocketLauncher2.transform.DOLocalMove(new Vector3(0, 0, 0), 2);
		rocketLauncher2.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f);

		rifle.transform.parent = floatPoints[2];
		rifle.transform.DOLocalMove(new Vector3(0, 0, 0), 1);
		rifle.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f);

		sniper.transform.parent = floatPoints[3];
		sniper.transform.DOLocalMove(new Vector3(0, 0, 0), 1);
		sniper.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f);

		shotgun.transform.parent = floatPoints[6];
		shotgun.transform.DOLocalMove(new Vector3(0, 0, 0), 1);
		shotgun.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f).OnComplete(() => {
			smgLook1.enabled = true;
			smgLook2.enabled = true;
			rifleLook.enabled = true;
			sniperLook.enabled = true;
			shotgunLook.enabled = true;
			inSecondPhase = true;
		});
	}

	private void DropGuns() 
	{
		machinePistol1.GetComponent<Rigidbody>().isKinematic = false;
		machinePistol1.GetComponent<Rigidbody>().useGravity = true;

		machinePistol2.GetComponent<Rigidbody>().isKinematic = false;
		machinePistol2.GetComponent<Rigidbody>().useGravity = true;

		rocketLauncher1.GetComponent<Rigidbody>().isKinematic = false;
		rocketLauncher1.GetComponent<Rigidbody>().useGravity = true;

		rocketLauncher2.GetComponent<Rigidbody>().isKinematic = false;
		rocketLauncher2.GetComponent<Rigidbody>().useGravity = true;

		rifle.GetComponent<Rigidbody>().isKinematic = false;
		rifle.GetComponent<Rigidbody>().useGravity = true;

		sniper.GetComponent<Rigidbody>().isKinematic = false;
		sniper.GetComponent<Rigidbody>().useGravity = true;

		shotgun.GetComponent<Rigidbody>().isKinematic = false;
		shotgun.GetComponent<Rigidbody>().useGravity = true;
	}

	private void UnEquipGuns() 
	{
		machinePistol1.transform.parent = null;
		machinePistol2.transform.parent = null;

		machinePistol1.GetComponentInChildren<HighlightEffect>().enabled = true;
		machinePistol2.GetComponentInChildren<HighlightEffect>().enabled = true;
		rocketLauncher1.GetComponentInChildren<HighlightEffect>().enabled = true;
		rocketLauncher2.GetComponentInChildren<HighlightEffect>().enabled = true;
		rifle.GetComponentInChildren<HighlightEffect>().enabled = true;
		sniper.GetComponentInChildren<HighlightEffect>().enabled = true;
		shotgun.GetComponentInChildren<HighlightEffect>().enabled = true;

	}

	public void ActivatePhase2()
	{
		if (inSecondPhase) 
		{
			return;
		}
		//inSecondPhase = true;
		UnEquipGuns();
		animator.AnimateSecondPhase();
		EquipFloatingGuns();
		mysticArmsTop.SetActive(true);
		mysticArmsBottom.SetActive(true);
		Task.Run(() => MoveBossRunner());
	}
	#endregion
}
