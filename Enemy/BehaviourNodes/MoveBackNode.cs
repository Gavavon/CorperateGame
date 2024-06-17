using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using UnityEngine.AI;
using Unity.VisualScripting;

[System.Serializable]
public class MoveBackNode : ActionNode
{
	AiActions actions;
	NavMeshAgent agent;

	public float amountToMove = 5;
	public float amountToMoveLess = 2;

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
		Vector3 fixedDestLess;

		if (actions.aiAgent.player.gameObject.transform.position.x > actions.gameObject.transform.position.x)
		{
			fixedDest = new Vector3(
				actions.gameObject.transform.position.x - 
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
				actions.gameObject.transform.position.y - 
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
				actions.gameObject.transform.position.z - 
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
			fixedDestLess = new Vector3(
				actions.gameObject.transform.position.x - 
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z - 
					actions.aiAgent.player.gameObject.transform.position.z) / 
					(actions.gameObject.transform.position.x - 
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess, 
					0, 
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z - 
					actions.aiAgent.player.gameObject.transform.position.z) / 
					(actions.gameObject.transform.position.x - 
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).x, 
				actions.gameObject.transform.position.y - 
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z - 
					actions.aiAgent.player.gameObject.transform.position.z) / 
					(actions.gameObject.transform.position.x - 
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess, 
					0, 
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z - 
					actions.aiAgent.player.gameObject.transform.position.z) / 
					(actions.gameObject.transform.position.x - 
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).y, 
				actions.gameObject.transform.position.z - 
				new Vector3(
					Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z - 
					actions.aiAgent.player.gameObject.transform.position.z) / 
					(actions.gameObject.transform.position.x - 
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess, 
					0, 
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z - 
					actions.aiAgent.player.gameObject.transform.position.z) / 
					(actions.gameObject.transform.position.x - 
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).z
			);
		}
		else
		{
			fixedDest = new Vector3(
				actions.gameObject.transform.position.x +
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
				actions.gameObject.transform.position.y +
				new Vector3(Mathf.Cos(Mathf.Atan((
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
				actions.gameObject.transform.position.z +
				new Vector3(Mathf.Cos(Mathf.Atan((
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
			fixedDestLess = new Vector3(
				actions.gameObject.transform.position.x +
				new Vector3(Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).x,
				actions.gameObject.transform.position.y +
				new Vector3(Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).y,
				actions.gameObject.transform.position.z +
				new Vector3(Mathf.Cos(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess,
					0,
					Mathf.Sin(Mathf.Atan((
					actions.gameObject.transform.position.z -
					actions.aiAgent.player.gameObject.transform.position.z) /
					(actions.gameObject.transform.position.x -
					actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).z
			);
			//fixedDest = new Vector3(actions.gameObject.transform.position.x + new Vector3(Mathf.Cos(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove, 0, Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).x, actions.gameObject.transform.position.y + new Vector3(Mathf.Cos(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove, 0, Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).y, actions.gameObject.transform.position.z + new Vector3(Mathf.Cos(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove, 0, Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMove).z);
			//fixedDestLess = new Vector3(actions.gameObject.transform.position.x + new Vector3(Mathf.Cos(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess, 0, Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).x, actions.gameObject.transform.position.y + new Vector3(Mathf.Cos(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess, 0, Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).y, actions.gameObject.transform.position.z + new Vector3(Mathf.Cos(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess, 0, Mathf.Sin(Mathf.Atan((actions.gameObject.transform.position.z - actions.aiAgent.player.gameObject.transform.position.z) / (actions.gameObject.transform.position.x - actions.aiAgent.player.gameObject.transform.position.x))) * amountToMoveLess).z);
		}
		if (!actions.room.roomCollider.bounds.Contains(fixedDest))
		{
			if (!actions.room.roomCollider.bounds.Contains(fixedDestLess))
			{
				Vector3 randPost = RandomPointInBounds(actions.room.roomCollider.bounds);
				if (agent.remainingDistance <= 1)
				{
					actions.aiAgent.navMeshAgent.SetDestination(randPost);
					return State.Success;
				}
			}
			if (agent.remainingDistance <= 1)
			{
				actions.aiAgent.navMeshAgent.SetDestination(fixedDestLess);
				return State.Success;
			}
		}
		if (agent.remainingDistance <= 1)
		{
			actions.aiAgent.navMeshAgent.SetDestination(fixedDest);
			return State.Success;
		}
		return State.Success;
    }

	//spawn customers in a random area within the bounds of a box collider
	public static Vector3 RandomPointInBounds(Bounds bounds)
	{
		return new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			0,
			//Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z)
		);
	}
}

/*
 * OnUpdate method Unoptimized
 * 
		float amountToMove = 5;
		float amountToMoveLess = 2;
		Vector3 ePosition = actions.gameObject.transform.position;
		Vector3 pPosition = actions.aiAgent.player.gameObject.transform.position;

		Vector3 dest = new Vector3(
			Mathf.Cos(Mathf.Atan((ePosition.z - pPosition.z) / (ePosition.x - pPosition.x))) * amountToMove,
			0,
			Mathf.Sin(Mathf.Atan((ePosition.z - pPosition.z) / (ePosition.x - pPosition.x))) * amountToMove
			);
		Vector3 destLess = new Vector3(
			Mathf.Cos(Mathf.Atan((ePosition.z - pPosition.z) / (ePosition.x - pPosition.x))) * amountToMoveLess,
			0,
			Mathf.Sin(Mathf.Atan((ePosition.z - pPosition.z) / (ePosition.x - pPosition.x))) * amountToMoveLess
			);

		Vector3 fixedDest;
		Vector3 fixedDestLess;

		if (pPosition.x > ePosition.x)
		{
			fixedDest = new Vector3(ePosition.x - dest.x, ePosition.y - dest.y, ePosition.z - dest.z);
			fixedDestLess = new Vector3(ePosition.x - destLess.x, ePosition.y - destLess.y, ePosition.z - destLess.z);
		}
		else
		{
			fixedDest = new Vector3(ePosition.x + dest.x, ePosition.y + dest.y, ePosition.z + dest.z);
			fixedDestLess = new Vector3(ePosition.x + destLess.x, ePosition.y + destLess.y, ePosition.z + destLess.z);

		}
		if (!actions.room.roomCollider.bounds.Contains(fixedDest))
		{
			if (!actions.room.roomCollider.bounds.Contains(fixedDestLess))
			{
				Vector3 randPost = RandomPointInBounds(actions.room.roomCollider.bounds);
				if (agent.remainingDistance <= 1)
				{
					actions.aiAgent.navMeshAgent.SetDestination(randPost);
					return State.Success;
				}
			}
			if (agent.remainingDistance <= 1)
			{
				actions.aiAgent.navMeshAgent.SetDestination(fixedDestLess);
				return State.Success;
			}
		}
		if (agent.remainingDistance <= 1)
		{
			actions.aiAgent.navMeshAgent.SetDestination(fixedDest);
			return State.Success;
		}
		return State.Success;
*/
