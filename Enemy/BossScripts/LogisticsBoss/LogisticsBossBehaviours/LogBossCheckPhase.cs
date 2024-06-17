using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LogBossCheckPhase : ActionNode
{
    [Range (1,2)]
    public int checkForPhase = 0;

	LogDepBossActions actions;
	protected override void OnStart() 
    {
		actions = blackboard.attachedObject.GetComponent<LogDepBossActions>();
	}

    protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        switch (true) 
        {
            case true when checkForPhase == 1 && !actions.CheckSecondPhase():
				return State.Success;
			case true when checkForPhase == 2 && actions.CheckSecondPhase():
				return State.Success;
            default:
				return State.Failure;
		}
    }
}
