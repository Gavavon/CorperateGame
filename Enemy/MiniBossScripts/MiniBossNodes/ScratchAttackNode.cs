using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ScratchAttackNode : ActionNode
{
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
		actions.ScratchPlayerGetter();
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		return State.Success;
	}
}
