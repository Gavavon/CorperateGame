using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class MoveInRange : ActionNode
{
	AiActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
	}

	protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
		actions.LookAtPlayer();
		if (actions.CheckForPlayer())
		{
			actions.PlayerInRange();
		}
		else
		{
			actions.PlayerOutRange();
		}
		actions.MoveDecisionGetter();
		return State.Success;
    }
}
