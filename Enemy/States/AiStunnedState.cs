using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;

public class AiStunnedState : AiState
{
	public AiStateId GetId()
	{
		return AiStateId.Stunned;
	}
	public void Enter(AiAgent agent)
	{
		agent.aiAnimator.AnimateStun();
		agent.GetComponent<AiBehaviourTreeHandler>().enabled = false;
		try
		{
			agent.navMeshAgent.SetDestination(agent.transform.position);
		}
		catch { }
		//consider spawning in a particle effect
		agent.actions.StunEnemyCoolDownGetter();
	}

	public void Update(AiAgent agent)
	{

	}

	public void Exit(AiAgent agent)
	{

	}
}
