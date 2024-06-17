using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class AiAlertedState : AiState
{
	public AiStateId GetId()
	{
		return AiStateId.Alerted;
	}
	public void Enter(AiAgent agent)
	{
		agent.actions.EnemyAlerted();
	}

	public void Update(AiAgent agent)
	{
		
	}
	
	public void Exit(AiAgent agent)
	{
		
	}
}
