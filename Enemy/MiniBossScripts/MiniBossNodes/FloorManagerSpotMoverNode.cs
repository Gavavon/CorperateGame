using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerSpotMoverNode : ActionNode
{
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
		actions.agent.SetDestination(actions.specialLocations[Random.Range(0, actions.specialLocations.Count)].position);
	}

	protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
		actions.transform.LookAt(actions.agent.destination);
		if (actions.agent.remainingDistance <= 1 && actions.stopDurationDone)
		{
			return State.Success;
		}
		return State.Running;
	}
}
