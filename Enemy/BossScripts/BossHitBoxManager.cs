using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBoxManager : MonoBehaviour
{
	public float rangedHeadDamageModifier = 1;
	public float rangedBodyDamageModifier = 1;
	public float rangedArmDamageModifier = 1;
	public float rangedLegDamageModifier = 1;

	public float meleeHeadDamageModifier = 1;
	public float meleeBodyDamageModifier = 1;
	public float meleeArmDamageModifier = 1;
	public float meleeLegDamageModifier = 1;

	private BossHealthSystem healthSystem;
	public enum BossDepartment
	{
		logistics
	}
	public BossDepartment bossDepartment = BossDepartment.logistics;

	[ShowIf("bossDepartment", BossDepartment.logistics)]
	public LogDepBossActions logisticsActions;

	// Start is called before the first frame update
	void Start()
	{
		healthSystem = GetComponent<BossHealthSystem>();
	}

	public void SendDamage(float damage)
	{
		switch (bossDepartment)
		{
			case BossDepartment.logistics:
				logisticsActions.TakeDamage(damage);
				break;
		}
	}
}
