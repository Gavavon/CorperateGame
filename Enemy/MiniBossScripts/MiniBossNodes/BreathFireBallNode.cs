using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class BreathFireBallNode : ActionNode
{
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
		actions.ShootFireBallGetter();
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
		return State.Success;
	}
}
