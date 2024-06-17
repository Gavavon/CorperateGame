using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class BreathFireColumnNode : ActionNode
{
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
		actions.ShootFireColumnGetter();
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		actions.LookAtPlayer();
		if (actions.IsShootColumnOnCoolDown())
		{
			return State.Success;
		}
		else
		{
			return State.Running;
		}
	}
}
