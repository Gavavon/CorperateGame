using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckBeingAttackedNode : ActionNode
{

	//have a bool in actions that is if the enemy has recently been attacked turn the bool on
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
        if (actions.recentlyTakenDamage)
        {
            return State.Success;
        }
        else 
        {
			return State.Failure;
		}
        
    }
}
