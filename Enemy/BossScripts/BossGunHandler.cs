using DG.Tweening;
using InfimaGames.LowPolyShooterPack;
using InfimaGames.LowPolyShooterPack.Legacy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BossGunHandler : MonoBehaviour
{

	[Title(label: "References")]

	[Tooltip("Gun Projectile Prefab")]
	[SerializeField]
	private GameObject gunProjectile;

	[Tooltip("Casing Prefab")]
	[SerializeField]
	private GameObject prefabCasing;

	[Tooltip("Transform for bullet spawn point")]
	[SerializeField]
	private Transform bulletSpawnPoint;

	[Tooltip("Transform that represents the weapon's ejection port")]
	[SerializeField]
	private Transform socketEjection;

	[Range(1, 12)]
	[Tooltip("Amount of shots that get shot per fire")]
	[SerializeField]
	private int shotCount = 1;

	[Tooltip("How far the weapon can fire from the center of the screen.")]
	[SerializeField]
	private float spread = 0.25f;

	[Tooltip("How fast the projectiles are.")]
	[SerializeField]
	private float projectileImpulse = 400.0f;

	[Title(label: "Audio Clips Reloads")]
	[SerializeField]
	private AudioClip shootSFX;
	[SerializeField]
	private AudioSource SFXPlayer;

	[SerializeField]
	private int projectileDamage;

	[ContextMenu("Shoot Gun")]
	public void ShootGun()
	{
		Fire();
	}

	private void Fire()
	{

		SFXPlayer.clip = shootSFX;

		//eject bullet casings
		EjectCasing();
		//Spawn as many projectiles as we need.
		for (var i = 0; i < shotCount; i++)
		{
			//Determine a random spread value using all of our multipliers.
			Vector3 spreadValue = UnityEngine.Random.insideUnitSphere * (spread);
			//Remove the forward spread component, since locally this would go inside the object we're shooting!
			spreadValue.z = 0;
			//Convert to world space.
			spreadValue = bulletSpawnPoint.TransformDirection(spreadValue);

			//Spawn projectile from the projectile spawn point.
			GameObject projectile = Instantiate(gunProjectile, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.eulerAngles + spreadValue));
			//Override projectile damage.
			projectile.GetComponent<EnemyProjectile>().projectileDamage = projectileDamage;
			//Add velocity to the projectile.
			projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileImpulse;
		}


		SFXPlayer.Play();
	}

	private void EjectCasing()
	{
		//Spawn casing prefab at spawn point.
		if (prefabCasing != null && socketEjection != null)
			Instantiate(prefabCasing, socketEjection.position, socketEjection.rotation);
	}
}

