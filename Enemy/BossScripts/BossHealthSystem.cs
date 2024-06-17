using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealthSystem : MonoBehaviour
{

	private MMProgressBar hudHealthInfo;
	[HideInInspector]
	public float currentHealth;
	public float totalHealth;

	// Start is called before the first frame update
	void Start()
    {
		currentHealth = totalHealth;
		HudInfoHandler temp = GameObject.FindGameObjectWithTag("HUD").GetComponent<HudInfoHandler>();
		hudHealthInfo = temp.mmBossHealthBar.GetComponent<MMProgressBar>();
	}

	public void TakeDamage(float dmg)
	{
		currentHealth -= dmg;
		hudHealthInfo.MinusXPercent((float)dmg / (float)totalHealth);
		
	}
}