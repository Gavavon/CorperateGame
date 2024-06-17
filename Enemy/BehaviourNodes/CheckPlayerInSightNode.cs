using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckPlayerInSightNode : ActionNode
{
	//Doesn't work right
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
		if (actions.CheckForPlayer())
		{
			actions.PlayerInRange();
			return State.Success;
		}
		else
		{
			actions.PlayerOutRange();
			return State.Failure;
		}
	}
}
