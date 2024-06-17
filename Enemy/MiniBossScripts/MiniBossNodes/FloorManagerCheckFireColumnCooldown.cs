using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerCheckFireColumnCooldown : ActionNode
{
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		if (!actions.IsShootColumnOnCoolDown())
		{
			return State.Success;
		}
		else
		{
			return State.Failure;
		}
	}
}
