using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class CheckDistanceNode : ActionNode
{
	public float lowerDistance = 0;
	public float upperDistance = 10;

	AiActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
	}

	protected override void OnStop()
	{
	}

	protected override State OnUpdate()
	{
		float distance = Vector3.Distance(actions.transform.position, actions.aiAgent.player.transform.position);

		if (lowerDistance < distance && distance < upperDistance)
		{
			return State.Success;
		}
		return State.Failure;
	}

	/*
	* -1 < Distacne < 4				(Close)
	*  4 < Distacne < 7				(Mid)
	*  7 < Distacne < 1000			(Far)
	*/
}
