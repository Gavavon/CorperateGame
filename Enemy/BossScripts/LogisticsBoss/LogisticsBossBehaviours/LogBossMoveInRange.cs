using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class LogBossMoveInRange : ActionNode
{
	LogDepBossActions actions;
	NavMeshAgent agent;

	public float amountToMove = 6;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<LogDepBossActions>();
		agent = actions.agent;
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		actions.LookAtPlayer();

		Vector3 fixedDest;

		if (actions.player.position.x > actions.gameObject.transform.position.x)
		{
			fixedDest = new Vector3(
				actions.player.position.x -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove).x,
				actions.player.position.y -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove).y,
				actions.player.position.z -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove).z
			);
		}
		else
		{
			fixedDest = new Vector3(
				actions.player.position.x +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove).x,
				actions.player.position.y +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove).y,
				actions.player.position.z +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.position.x))) * amountToMove).z
			);
		}
		if (agent.remainingDistance <= 1)
		{
			agent.SetDestination(fixedDest);
		}
		return State.Success;
	}
}
