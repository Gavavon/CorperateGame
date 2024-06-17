using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> miniBosses = new List<GameObject>();
    public List<GameObject> bosses = new List<GameObject>();
    public List<AiAgent> eAgents = new List<AiAgent>();

	private PlayerProgressManager playerProgress;

	private List<GateHandler> gates = new List<GateHandler>();

    // Start is called before the first frame update
    void Start()
    {
		List<GameObject> tempList = GameObject.FindGameObjectsWithTag("Gate").ToList();
		foreach (GameObject temp in tempList) 
		{
			gates.Add(temp.GetComponent<GateHandler>());
		}
		playerProgress = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PlayerProgressManager>();
		enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        for (int i = 0; i < enemies.Count; i++) 
        {
            if (enemies[i].GetComponent<AiAgent>() != null) 
            {
				eAgents.Add(enemies[i].GetComponent<AiAgent>());
			}
        }

		miniBosses = GameObject.FindGameObjectsWithTag("MiniBoss").ToList();
		bosses = GameObject.FindGameObjectsWithTag("Boss").ToList();
	}

	public bool CheckInCombat() 
    {
		for (int i = 0; i < eAgents.Count; i++) 
        {
            if (eAgents[i].stateMachine.GetCurrentState() == AiStateId.Combat) 
            {
                return true;
            }
        }
		return false;
	}

	public void RemoveSelfFromList(GameObject enemy) 
	{
		enemies.Remove(enemy);
		playerProgress.IncreaseStat(PlayerProgressManager.StatTypes.enemy);
	}

    public void TurnEnemiesOff() 
    {
        if (enemies.Count > 0) 
        {
			for (int i = 0; i < enemies.Count; i++)
			{
				enemies[i].SetActive(false);
			}
		}
		if (miniBosses.Count > 0)
		{
			for (int i = 0; i < miniBosses.Count; i++)
			{
				miniBosses[i].SetActive(false);
			}
		}
		if (bosses.Count > 0)
		{
			for (int i = 0; i < bosses.Count; i++)
			{
				bosses[i].SetActive(false);
			}
		}
	}
}
