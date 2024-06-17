using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LogBossRifleAttack : ActionNode
{
	LogDepBossActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<LogDepBossActions>();
		actions.RifleAttack();
	}

    protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        return State.Success;
    }
}
