using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class BigSniperShootNode : ActionNode
{
	BigSniperActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<BigSniperActions>();
		actions.ShootGun();
	}

	protected override void OnStop()
	{
	}

	protected override State OnUpdate()
	{
		actions.GetGenericActions().LookAtPlayer();
		return State.Success;
	}
}
