using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LogBossMissileAttack : ActionNode
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
		if (actions.canUseMissile)
		{
			actions.MissileAttack();
			return State.Success;
		}
		else 
		{
			return State.Failure;
		}
    }
}
