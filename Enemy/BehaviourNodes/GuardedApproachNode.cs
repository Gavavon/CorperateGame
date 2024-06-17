using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class GuardedApproachNode : ActionNode
{
	AiActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
		actions.recentlyCharging = false;
	}

	protected override void OnStop() 
    {

	}

    protected override State OnUpdate() 
    {
		actions.aiAgent.navMeshAgent.speed = 3;
		actions.animationController.AnimateBlock();
		actions.LookAtPlayer();
		float amountToMove = 1;

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
		if (actions.aiAgent.navMeshAgent.remainingDistance <= 3 && actions.stopDurationDone)
		{
			actions.aiAgent.navMeshAgent.SetDestination(fixedDest);
		}
		return State.Success;
    }
}
