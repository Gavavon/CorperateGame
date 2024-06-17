using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDepBossManager : MonoBehaviour
{
	[SerializeField]
	private LogDepBossActions actions;

	private HudInfoHandler hudInfo;

	[SerializeField]
	private GameObject doorOpener;

	void Start()
	{
		hudInfo = GameObject.FindGameObjectsWithTag("HUD")[0].GetComponent<HudInfoHandler>();
	}

	public void BossSlayed()
	{
		hudInfo.HideBossHealthBar();

		GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PlayerProgressManager>().IncreaseStat(PlayerProgressManager.StatTypes.boss, 1);

		//reward player
		doorOpener.SetActive(true);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !actions.GetActive())
		{
			hudInfo.ShowBossHealthBar();
			actions.ActivateBoss();
		}
	}
}
