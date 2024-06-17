using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomInfo : MonoBehaviour
{
    [SerializeField]
    private Transform[] fortSpots;

	//public List<GameObject> EnemiesInRoom;

	private bool alertEnemies = false;

	[HideInInspector]
	public bool isPlayerInRoom = false;

	//Upon collision with another GameObject, this GameObject will reverse direction
	private void OnTriggerEnter(Collider collider)
	{
		if (collider.transform.tag == "Enemy")
		{
			//this aint working
		}
		if (collider.transform.tag == "Player") 
		{
			alertEnemies = true;
			isPlayerInRoom = true;
		}		
	}

	private void OnTriggerStay(Collider collider)
	{
		if (collider.gameObject.tag == "Enemy")
		{
			Debug.Log(collider.gameObject);
			if (alertEnemies && collider.gameObject.GetComponent<AiAgent>().stateMachine.currentState == AiStateId.Idle) 
			{
				collider.gameObject.GetComponent<AiAgent>().stateMachine.ChangeState(AiStateId.Alerted);
			}
		}
	}

	
	private void OnTriggerExit(Collider collider)
	{
		if (collider.transform.tag == "Enemy")
		{
			//collider.GetComponent<AiActions>().room = null;
		}
		if (collider.transform.tag == "Player")
		{
			isPlayerInRoom = false;
		}
	}
	
}
