using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;
using Unity.VisualScripting;

[System.Serializable]
public class MoveForwardNode : ActionNode
{
	private bool takePotShots = false;

	private float percentTakePotShots;

	private float potShotCoolDown;
	private float potShotSpread;
	private float potShotCoolDownBetweenShots;
	private int potShotMax;
	private int potShotMin;

	AiActions actions;
	NavMeshAgent agent;
	protected override void OnStart()
	{
		actions = blackboard.attachedObject.GetComponent<AiActions>();
		agent = actions.aiAgent.navMeshAgent;
		takePotShots = actions.aiAgent.config.moveForwardPotShots;
		if (takePotShots) 
		{
			potShotCoolDown = actions.coolDownBetweenPotShots;
			percentTakePotShots = actions.percentTakePotShots / 100;
			potShotSpread = actions.potShotSpread;
			potShotCoolDownBetweenShots = actions.potShotCoolDownBetweenShots;
			potShotMax = actions.potShotMax;
			potShotMin = actions.potShotMin;
		}
		
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

		float amountToMove = 6;

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
		if (Random.Range(0, 1f) <= percentTakePotShots && takePotShots)
		{
			actions.ShootGunGetterWithCoolDown(cooldown: potShotCoolDown, useVariantSpread: true, newSpread: potShotSpread, multipleShots: true, shotsAmount: Random.Range(potShotMin, potShotMax), secondsBetweenShots: potShotCoolDownBetweenShots);
		}
		return State.Success;
	}
}
