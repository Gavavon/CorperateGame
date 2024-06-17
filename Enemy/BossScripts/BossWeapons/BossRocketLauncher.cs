using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRocketLauncher : MonoBehaviour
{
	[Title(label: "References")]

	[Tooltip("Gun Projectile Prefab")]
	[SerializeField]
	private GameObject gunProjectile;

	[Tooltip("Transform for bullet spawn point")]
	[SerializeField]
	private Transform bulletSpawnPoint;

	[SerializeField]
	private AudioSource SFXPlayer;

	[SerializeField]
	[HideInInspector]
	public GameObject playerTarget;

	[Title(label: "Stats")]
	[SerializeField]
	private int impactDamage = 1;

	[SerializeField]
	private int AOEDamage = 1;

	[ContextMenu("Shoot Gun")]
	public void Fire()
	{
		GameObject projectile = Instantiate(gunProjectile, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.eulerAngles));
		//Override projectile damage.
		projectile.GetComponent<BossMissileHandler>(). _target = playerTarget;
		projectile.GetComponent<EnemyProjectile>().projectileDamage = impactDamage;
		projectile.GetComponent<BossMissileHandler>()._AOEDamage = AOEDamage;
		
		SFXPlayer.Play();
	}
}
