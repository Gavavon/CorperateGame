using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LogBossSMGAttack : ActionNode
{
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
		if (!actions.CheckSecondPhase()) 
		{
			actions.LookAtPlayer();
		}

		if (actions.canUseSMG)
		{
			actions.SMGAttack();
			return State.Success;
		}
		else
		{
			return State.Failure;
		}
    }
}
