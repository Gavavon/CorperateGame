using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class LogBossCheckDistance : ActionNode
{
	public enum DistanceType 
	{
		far,
		middle,
		close,
		suparclose
	}
	public DistanceType distance;
	private float lowerDistance = 0;
	private float upperDistance = 0;

	LogDepBossActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<LogDepBossActions>();
		switch (distance) 
		{
			case DistanceType.far:
				upperDistance = 1000;
				lowerDistance = 10;
				break;
			case DistanceType.middle:
				upperDistance = 10;
				lowerDistance = 5;
				break;
			case DistanceType.close:
				upperDistance = 5;
				lowerDistance = 2;
				break;
			case DistanceType.suparclose:
				upperDistance = 2;
				lowerDistance = -1;
				break;
		}
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		float distance = Vector3.Distance(actions.transform.position, actions.player.position);

		if (lowerDistance < distance && distance < upperDistance)
		{
			return State.Success;
		}
		return State.Failure;
	}
}
