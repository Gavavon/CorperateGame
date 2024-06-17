using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static BossHitBoxManager;

public class RoomTrigger : MonoBehaviour
{
	public List<AiActions> enemiesInRoomToActivate = new List<AiActions>();
	[HideInInspector]
	public BoxCollider roomCollider;
	private bool doneSorting = false;
	[SerializeField]
	private bool hasInternalRoom = false;
	[ShowIf("hasInternalRoom", true)]
	[SerializeField]
	private RoomTrigger internalRoom;

	private void Start()
	{
		roomCollider = GetComponent<BoxCollider>();
		StartCoroutine(SortWaiter());
	}

	private IEnumerator SortWaiter() 
	{
		yield return new WaitForSeconds(2f);
		doneSorting = true;
		if (hasInternalRoom) 
		{
			for (int j = 0; j < enemiesInRoomToActivate.Count; j++) 
			{
				for (int i = 0; i < internalRoom.enemiesInRoomToActivate.Count; i++)
				{
					if (enemiesInRoomToActivate[j] == internalRoom.enemiesInRoomToActivate[i]) 
					{
						enemiesInRoomToActivate.Remove(internalRoom.enemiesInRoomToActivate[i]);
						//break;
					}
				}
			}
			
		}
	}

	private void SortEnemy(GameObject enemy) 
	{
		if (enemy.GetComponent<AiActions>() == null) 
		{
			return;
		}
		if (enemy.GetComponent<AiActions>().GetComponent<AiAgent>().config.enemyBehaviors == AiAgentConfig.AiBehaviors.ProvokableWorkers)
		{
			return;
		}
		enemiesInRoomToActivate.Add(enemy.GetComponent<AiActions>());
	}

	public void AlertRoom()
	{
		for (int i = 0; i < enemiesInRoomToActivate.Count; i++)
		{
			enemiesInRoomToActivate[i].AlertEnemy();
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" || other.gameObject.tag == "GlassBreak" || other.gameObject.tag == "AlertObject")
		{
			AlertRoom();
		}
		if (other.gameObject.tag == "Enemy" && !doneSorting)
		{
			SortEnemy(other.gameObject);
		}
		if (other.gameObject.tag == "Enemy")
		{
			other.GetComponent<AiActions>().room = this;
		}
	}
}
