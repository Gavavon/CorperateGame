using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class MeleeAttackPlayerNode : ActionNode
{
	AiActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
		actions.aiAgent.navMeshAgent.speed = actions.navMeshSpeed;
		actions.SwordActivationGetter();
	}

	protected override void OnStop() 
    {
		
	}

    protected override State OnUpdate() 
    {
		actions.LookAtPlayer();
		return State.Success;
    }
}
