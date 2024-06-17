using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerSuperNode : ActionNode
{
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
        actions.RunSuper();
	}

	protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        return State.Success;
    }
}
