using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDeathState : AiState
{
	public AiStateId GetId()
	{
		return AiStateId.Death;
	}
	
	public void Enter(AiAgent agent)
	{
		agent.ragdoll.ActivateRagdoll();
		agent.actions.DespawnBody();
	}
	
	public void Update(AiAgent agent)
	{

	}
	
	public void Exit(AiAgent agent)
	{
		
	}
}
