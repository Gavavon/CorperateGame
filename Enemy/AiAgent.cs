using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class AiAgent : MonoBehaviour
{
	public bool Unique = false;
	public AiAgentConfig config;
	[HideInInspector]
	public Transform player;
	[HideInInspector]
	public GameObject playerTarget;

	[HideInInspector] public AiStateMachine stateMachine;
	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public Ragdoll ragdoll;
	[HideInInspector] public AiAnimator aiAnimator;
	[HideInInspector] public AiActions actions;
    [HideInInspector] public AiSensor sensor;
	[HideInInspector] public WeaponIK weaponIK;


	// Start is called before the first frame update
	void Start()
    {
		
		playerTarget = GameObject.FindGameObjectsWithTag("PlayerTargetPoint")[0];
		player = GameObject.FindGameObjectsWithTag("Player")[0].transform;

		ragdoll = GetComponent<Ragdoll>();
		navMeshAgent = GetComponent<NavMeshAgent>();
		aiAnimator = GetComponent<AiAnimator>();
		actions = GetComponent<AiActions>();

		sensor = GetComponent<AiSensor>();
		weaponIK = GetComponent<WeaponIK>();
		stateMachine = new AiStateMachine(this);

        stateMachine.RegisterState(new AiAlertedState());
        stateMachine.RegisterState(new AiDeathState());
		stateMachine.RegisterState(new AiCombatState());
		stateMachine.RegisterState(new AiIdleState());
		stateMachine.RegisterState(new AiStunnedState());

		switch (config.enemyBehaviors)
		{
			case AiAgentConfig.AiBehaviors.None:
				Debug.Log("Enemy Does not have Behavior Assigned");
				break;
			case AiAgentConfig.AiBehaviors.ProvokableWorkers:
				stateMachine.ChangeState(AiStateId.Idle);
				break;
			case AiAgentConfig.AiBehaviors.Attackers:
				stateMachine.ChangeState(AiStateId.Idle);
				break;
			case AiAgentConfig.AiBehaviors.Defenders:
				stateMachine.ChangeState(AiStateId.Idle);
				break;
			case AiAgentConfig.AiBehaviors.Suppressors:
				stateMachine.ChangeState(AiStateId.Idle);
				break;
			case AiAgentConfig.AiBehaviors.Chargers:
				stateMachine.ChangeState(AiStateId.Idle);
				break;
			case AiAgentConfig.AiBehaviors.Unique:
				stateMachine.ChangeState(AiStateId.Idle);
				break;
		}
	}

    // Update is called once per frame
    void Update()
    {
        stateMachine.Update();
    }
}
