using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerMoveToPlayer : ActionNode
{
	public float amountToMove = 6;
	private ManagerActions actions;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<ManagerActions>();
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

		if (actions.player.gameObject.transform.position.x > actions.gameObject.transform.position.x)
		{
			fixedDest = new Vector3(
				actions.player.gameObject.transform.position.x -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove).x,
				actions.player.gameObject.transform.position.y -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove).y,
				actions.player.gameObject.transform.position.z -
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove).z
			);
		}
		else
		{
			fixedDest = new Vector3(
				actions.player.gameObject.transform.position.x +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove).x,
				actions.player.gameObject.transform.position.y +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove).y,
				actions.player.gameObject.transform.position.z +
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.player.gameObject.transform.position.x))) * amountToMove).z
			);
		}
		if (actions.agent.remainingDistance <= 1 && actions.stopDurationDone)
		{
			actions.agent.SetDestination(fixedDest);
		}
		return State.Success;
	}
}
