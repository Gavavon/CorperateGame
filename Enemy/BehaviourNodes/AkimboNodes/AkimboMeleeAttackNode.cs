using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class AkimboMeleeAttackNode : ActionNode
{
	AkimboActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AkimboActions>();
		actions.SwingSword();
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
