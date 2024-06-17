using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using InfimaGames.LowPolyShooterPack;

[System.Serializable]
public class LogBossForceAttack : ActionNode
{
	LogDepBossActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<LogDepBossActions>();
        actions.player.GetComponent<Movement>().LaunchPlayer(actions.transform.forward, actions.forcePush);
	}

    protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        return State.Success;
    }
}
