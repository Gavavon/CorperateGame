using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepartmentClearUpdater : MonoBehaviour
{
    private bool cleared = false;

	private void UpdateStats() 
	{
		if (cleared) 
		{
			return;
		}
		cleared = true;
		GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PlayerProgressManager>().IncreaseStat(PlayerProgressManager.StatTypes.department, 1);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			UpdateStats();
		}
	}



}
