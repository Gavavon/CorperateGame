using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerCheckPhaseNode : ActionNode
{
    public int phaseExpected = 0;
    private int currentPhase = 0;

	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
        if (actions.inSecondPhase)
        {
            currentPhase = 2;
		}
        else 
        {
            currentPhase = 1;
		}
	}

	protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        if (currentPhase == phaseExpected) 
        {
			return State.Success;
		}
        else 
        {
			return State.Failure;
		}
        
    }
}
