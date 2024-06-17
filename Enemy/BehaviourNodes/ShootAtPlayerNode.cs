using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class ShootAtPlayerNode : ActionNode
{
	AiActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
		
		actions.ShootGunGetter(false);
	}

	protected override void OnStop() {
    }

    protected override State OnUpdate() {
        return State.Success;
    }
}
