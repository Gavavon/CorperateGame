using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class AkimboChargeNode : ActionNode
{
	AkimboActions actions;
	AiActions genericActions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AkimboActions>();
		actions.GetAnimator().AnimateCharge();
	}

	protected override void OnStop()
	{

	}

	protected override State OnUpdate()
	{
		genericActions = actions.GetGenericActions();
		genericActions.LookAtPlayer();
		float amountToMove = 1;

		Vector3 fixedDest;

		if (genericActions.aiAgent.player.gameObject.transform.position.x > genericActions.gameObject.transform.position.x)
		{
			fixedDest = new Vector3(
				genericActions.aiAgent.player.gameObject.transform.position.x -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).x,
				genericActions.aiAgent.player.gameObject.transform.position.y -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).y,
				genericActions.aiAgent.player.gameObject.transform.position.z -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).z
			);
		}
		else
		{
			fixedDest = new Vector3(
				genericActions.aiAgent.player.gameObject.transform.position.x +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).x,
				genericActions.aiAgent.player.gameObject.transform.position.y +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).y,
				genericActions.aiAgent.player.gameObject.transform.position.z +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					genericActions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					genericActions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).z
			);
		}
		if (genericActions.aiAgent.navMeshAgent.remainingDistance <= 3 && genericActions.stopDurationDone)
		{
			genericActions.aiAgent.navMeshAgent.SetDestination(fixedDest);
		}
		return State.Success;
	}
}
