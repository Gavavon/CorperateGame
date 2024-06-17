using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTriggerCombiner : MonoBehaviour
{
	public List<AiActions> enemiesInRoomToActivate = new List<AiActions>();
    public void Alert() 
    {
		for (int i = 0; i < enemiesInRoomToActivate.Count; i++)
		{
			enemiesInRoomToActivate[i].AlertEnemy();
		}
	}
	public void SortEnemy(GameObject enemy)
	{
		if (enemy.GetComponent<AiActions>().GetComponent<AiAgent>().config.enemyBehaviors == AiAgentConfig.AiBehaviors.ProvokableWorkers)
		{
			return;
		}
		enemiesInRoomToActivate.Add(enemy.GetComponent<AiActions>());

	}
}
