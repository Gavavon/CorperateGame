using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerCheckSuperDoneNode : ActionNode
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
        if (actions.IsSuperOnCoolDown())
        {
            return State.Failure;
        }
        else 
        {
			return State.Success;
		}
        
    }
}
