using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;

[System.Serializable]
public class MoveForwardNoShootNode : ActionNode
{
	AiActions actions;
	NavMeshAgent agent;

	public float amountToMove = 6;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
		agent = actions.aiAgent.navMeshAgent;
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		actions.LookAtPlayer();
		if (actions.CheckForPlayer())
		{
			actions.PlayerInRange();
		}
		else
		{
			actions.PlayerOutRange();
		}

		Vector3 fixedDest;

		if (actions.aiAgent.player.gameObject.transform.position.x > actions.gameObject.transform.position.x)
		{
			fixedDest = new Vector3(
				actions.aiAgent.player.gameObject.transform.position.x -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).x,
				actions.aiAgent.player.gameObject.transform.position.y -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).y,
				actions.aiAgent.player.gameObject.transform.position.z -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).z
			);
		}
		else
		{
			fixedDest = new Vector3(
				actions.aiAgent.player.gameObject.transform.position.x +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).x,
				actions.aiAgent.player.gameObject.transform.position.y +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).y,
				actions.aiAgent.player.gameObject.transform.position.z +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).z
			);
		}
		if (agent.remainingDistance <= 1 && actions.stopDurationDone)
		{
			actions.aiAgent.navMeshAgent.SetDestination(fixedDest);
		}
		return State.Success;
	}
}
