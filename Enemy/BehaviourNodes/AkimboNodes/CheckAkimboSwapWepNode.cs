using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckAkimboSwapWepNode : ActionNode
{
    AkimboActions actions;
    protected override void OnStart() 
    {
		actions = blackboard.attachedObject.GetComponent<AkimboActions>();
	}

    protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        if (actions.CheckForSwap())
        {
            return State.Success;
        }
        else 
        {
			return State.Failure;
		}
        
    }
}
