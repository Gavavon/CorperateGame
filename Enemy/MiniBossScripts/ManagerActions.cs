using InfimaGames.LowPolyShooterPack;
using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Processors;

public class ManagerActions : MonoBehaviour
{

	private float currentHealth;
	public float totalHealth;
	public float secondPhaseHealthCheck;

	public bool inSecondPhase = false;

	public Material originalMat;
	public Material secondMaterial;

	public SkinnedMeshRenderer skin;

	private MiniBossBehaviourTree treeRunner;

	public ParticleSystem fireBreath;
	public ParticleSystem fireFlak;

	[HideInInspector]
	public NavMeshAgent agent;

	private ManagerAnimator animator;

	public Transform headRefPoint;

	[SerializeField]
	private Transform rayCastOrigin;

	[SerializeField]
	private MiniBossRoomManager roomManager;
	[SerializeField]
	private Transform enemyAttackCloneParent;

	public GameObject fireColumn;
	public GameObject fireAOE;
	public GameObject fireBall;

	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public GameObject playerTarget;

	private bool isAttacking = false;

	private bool knockBackCoolDownActive = false;
	private bool superCoolDownActive = false;

	private AiSensor sensor;
	private WeaponIK wepIK;

	[HideInInspector]
	public bool stopDurationDone = true;
	public LayerMask rayCastExclusionlayers;

	private Ragdoll ragdoll;
	private MMProgressBar hudHealthInfo;

	public int fireBallForce = 500;
	public int knockBackForce = 200;

	public Collider RightHand;
	public Collider LeftHand;

	public int clawDamage = 5;

	public List<Transform> specialLocations;

	private bool superStarted = false;

	private bool shootColumnStarted = false;
	private bool shootColumnCoolDownActive = false;
	private int columnCoolDown = 25000;

	[Tooltip("In miliseconds")]
	private int superCoolDown = 25000;
	[HideInInspector]
	public bool dead = false;

	// Start is called before the first frame update
	void Start()
	{
		currentHealth = totalHealth;
		HudInfoHandler temp = GameObject.FindGameObjectWithTag("HUD").GetComponent<HudInfoHandler>();
		hudHealthInfo = temp.mmBossHealthBar.GetComponent<MMProgressBar>();
		playerTarget = GameObject.FindGameObjectsWithTag("PlayerTargetPoint")[0];
		player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		ragdoll = GetComponent<Ragdoll>();
		sensor = GetComponent<AiSensor>();
		wepIK = GetComponent<WeaponIK>();
		agent = GetComponent<NavMeshAgent>();
		treeRunner = GetComponent<MiniBossBehaviourTree>();
		animator = GetComponent<ManagerAnimator>();
		skin = GetComponentInChildren<SkinnedMeshRenderer>();
	}
	public void StopForDurationGetter(Vector3 currentDestination, float waitTime)
	{
		StartCoroutine(StopEnemy(currentDestination, waitTime));
	}
	private IEnumerator StopEnemy(Vector3 currentDestination, float waitTime)
	{
		if (!stopDurationDone)
		{
			yield break;
		}
		stopDurationDone = false;
		agent.destination = this.transform.position;
		yield return new WaitForSeconds(waitTime);
		agent.destination = currentDestination;
		stopDurationDone = true;
	}

	public bool CheckForPlayer()
	{
		return sensor.IsInSight(playerTarget);
	}

	public void PlayerInRange()
	{
		wepIK.SetTargetTransform(playerTarget.transform);
	}

	public void PlayerOutRange()
	{
		wepIK.SetTargetTransformDefault();
	}

	public bool GetBnockBackCoolDown() 
	{
		return knockBackCoolDownActive;
	}
	public void LookAtPlayer()
	{
		var p = player.position;
		p.y = transform.position.y;
		transform.LookAt(p);
	}

	[ContextMenu("Activate")]
	public void ActivateMiniBoss() 
    {
        animator.DoneWithTask();
		agent.enabled = true;
		agent.destination = this.transform.position;
        GetComponent<WeaponIK>().enabled = true;
        GetComponent<AiSensor>().enabled = true;
        treeRunner.enabled = true;
	}

    public void TakeDamage(float dmg) 
    {
		hudHealthInfo.MinusXPercent((float)dmg / (float)totalHealth);
		currentHealth -= dmg;
		if (currentHealth <= 0) 
        {
            Died();
            return;
        }
        if(currentHealth < secondPhaseHealthCheck && !inSecondPhase) 
        {
            ActivatePhase2();
        }
    }

    [ContextMenu("FireBall")]
    public void ShootFireBallGetter() 
    {
		if (isAttacking)
		{
			return;
		}
		isAttacking = true;
		StartCoroutine(ShootFireBall());
    }

	[ContextMenu("FireColumn")]
	public void ShootFireColumnGetter()
	{
		if (shootColumnStarted == true || shootColumnCoolDownActive == true)
		{
			return;
		}
		shootColumnStarted = true;
		
		StartCoroutine(ShootFireColumn());
	}
	[ContextMenu("FireAOE")]
	public void ShootFireAOEGetter()
	{
		if (isAttacking) 
		{
			return;
		}
		isAttacking = true;
		StartCoroutine(ShootFireAOE());
	}
	[ContextMenu("Scratch")]
	public void ScratchPlayerGetter()
	{
		if (isAttacking)
		{
			return;
		}
		isAttacking = true;
		StartCoroutine(ScratchAtPlayer());
	}

	[ContextMenu("KnockBack")]
	public void KnockBackGetter() 
	{
		Vector3 direction = headRefPoint.TransformDirection(Vector3.forward);
		player.GetComponent<Movement>().LaunchPlayer(direction, knockBackForce);

		knockBackCoolDownActive = true;
		
	}

	public void RunSuper() 
	{
		if (superStarted == true || superCoolDownActive == true) 
		{
			return;
		}
		superStarted = true;

		//Doesn't work right now needs to be adjusted so that super actually gets called


		//Debug.Log("Super==========================================");
		//shoot fire on the ground
		//knockback player
		//if room not set on fire, set it on fire
		//shoot 2 sets of fire columns
		superCoolDownActive = true;
		superStarted = false;
		Task.Run(() => RunSuperCoolDown());
	}

	public bool IsSuperOnCoolDown() 
	{
		return superCoolDownActive;
	}

	public bool IsShootColumnOnCoolDown()
	{
		return shootColumnCoolDownActive;
	}

	async Task RunSuperCoolDown() 
	{
		await Task.Delay(superCoolDown);
		superCoolDownActive = false;
	}

	private IEnumerator ScratchAtPlayer() 
	{
		animator.ScratchAttack(true);
		yield return new WaitForSeconds(0.2f);
		animator.ScratchAttack(false);
		yield return new WaitForSeconds(1.10f);
		isAttacking = false;
	}

	private IEnumerator ShootFireBall()
	{
		animator.AnimateMouthOpen();
		yield return new WaitForSeconds(0.13f);
		Vector3 direction = headRefPoint.TransformDirection(Vector3.forward);
		// Does the ray intersect any objects excluding the player layer
		GameObject projectile = Instantiate(fireBall, new Vector3(rayCastOrigin.position.x, rayCastOrigin.position.y, rayCastOrigin.position.z), Quaternion.identity);
		projectile.GetComponent<Rigidbody>().AddForce(direction * fireBallForce);
		fireFlak.Play();
		yield return new WaitForSeconds(1f);
		animator.AnimateMouthClosed();
		isAttacking = false;
	}

	private IEnumerator ShootFireAOE()
	{
		animator.AnimateMouthOpen();
		yield return new WaitForSeconds(0.13f);
		fireBreath.Play();
		yield return new WaitForSeconds(0.1f);
		Vector3 direction = headRefPoint.TransformDirection(Vector3.forward);
		direction.y = 0;
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(rayCastOrigin.position, rayCastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~rayCastExclusionlayers))
		{
			//Debug.DrawRay(rayCastOrigin.position, rayCastOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			GameObject attackClone = Instantiate(fireAOE, new Vector3(hit.point.x + (direction.x), 0, hit.point.z + (direction.z)), Quaternion.identity);
			attackClone.transform.parent = enemyAttackCloneParent;
		}
		yield return new WaitForSeconds(1f);
		fireBreath.Stop();
		animator.AnimateMouthClosed();
		isAttacking = false;
	}


	private IEnumerator ShootFireColumn() 
    {
		shootColumnStarted = false;
		StopForDurationGetter(agent.destination, 3);
		animator.AnimateMouthOpen();
		yield return new WaitForSeconds(0.13f);
		fireBreath.Play();
		yield return new WaitForSeconds(0.1f);
        Vector3 direction = headRefPoint.TransformDirection(Vector3.forward);
		direction.y = 0;
		RaycastHit hit;
		// Does the ray intersect any objects excluding the player layer
		if (Physics.Raycast(rayCastOrigin.position, rayCastOrigin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, ~rayCastExclusionlayers))
		{
			//Debug.DrawRay(rayCastOrigin.position, rayCastOrigin.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
			for (int i = 0; i < 7; i++)
			{
				GameObject attackClone = Instantiate(fireColumn, new Vector3(hit.point.x + (i * direction.x), 0, hit.point.z + (i * direction.z)), Quaternion.identity);
				attackClone.transform.parent = enemyAttackCloneParent;
				yield return new WaitForSeconds(0.1f);
			}
		}
        yield return new WaitForSeconds(1f);
        fireBreath.Stop();
        animator.AnimateMouthClosed();
		shootColumnCoolDownActive = true;
		Task.Run(() => FirColumnCoolDown());
	}

	async Task FirColumnCoolDown()
	{
		await Task.Delay(columnCoolDown);
		shootColumnCoolDownActive = false;
	}

	[ContextMenu("Start Second Phase")]
	public void ActivatePhase2() 
    {
		StartCoroutine(roomManager.StartSecondPhase());
		inSecondPhase = true;
		skin.material = secondMaterial;
	}

    public void Died() 
    {
		if (dead) 
		{
			return;
		}
		dead = true;

		roomManager.MiniBossSlayed();
        skin.material = originalMat;

		ragdoll.ActivateRagdoll();
		StartCoroutine(RemoveBody());
        treeRunner.enabled = false;
		agent.enabled = false;
		wepIK.enabled = false;
		sensor.enabled = false;
	}

	private IEnumerator RemoveBody()
	{
		yield return new WaitForSeconds(10f);
		Destroy(gameObject);
	}


}
