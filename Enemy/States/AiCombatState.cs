using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCombatState : AiState
{
	public AiStateId GetId()
	{
		return AiStateId.Combat;
	}
	public void Enter(AiAgent agent)
	{
		agent.actions.EnterCombat();
	}

	public void Update(AiAgent agent)
	{

	}

	public void Exit(AiAgent agent)
	{

	}
}
