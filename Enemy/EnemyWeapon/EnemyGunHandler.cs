using DG.Tweening;
using InfimaGames.LowPolyShooterPack;
using InfimaGames.LowPolyShooterPack.Legacy;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyGunHandler : MonoBehaviour
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

	[Tooltip("Grip1 point for enemy hands")]
	[SerializeField]
	private Transform gripPoint1;

	[Tooltip("Grip2 point for enemy hands")]
	[SerializeField]
	private Transform gripPoint2;

	[Tooltip("Transform that represents the weapon's ejection port")]
	[SerializeField]
	private Transform socketEjection;

	[Tooltip("If the gun ahs something that slides on shoot")]
	[SerializeField]
	private Transform slider;

	[Tooltip("Slider starting Z position")]
	[SerializeField]
	private float sliderOriginZ;

	[Tooltip("Slider's cocked back Z position")]
	[SerializeField]
	private float sliderNewZ;


	public enum WeaponType
	{
		AssaultRifle,
		Handgun,
		Shotgun,
		SMG,
		Sniper
	}
	[Title(label: "Weapon Stats")]
	[Tooltip("Type of weapon")]
	[SerializeField]
	public WeaponType wepType = WeaponType.Handgun;

	[Range(1, 250)]
	[Tooltip("Amount of ammunition for each clip")]
	[SerializeField]
	private int ammoPerClip;

	[HideInInspector]
	[SerializeField]
	private int currentAmmo;

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

	[Tooltip("If the gun has a slider")]
	[SerializeField]
	private bool hasSlider;

	[Tooltip("How long the slider takes to move back and forth")]
	[SerializeField]
	private float sliderDuration;

	[Title(label: "Audio Clips Reloads")]
	[SerializeField]
	private AudioClip shootSFX;
	[SerializeField]
	private AudioClip reloadSFX;
	[SerializeField]
	private AudioSource SFXPlayer;

	private AiActions actions;

	[HideInInspector]
	private bool isReloading = false;

	public void Start()
	{
		actions = GetComponentInParent<AiActions>();
		currentAmmo = ammoPerClip;
	}

	public void ShootGun(bool useVariant, float variantSpread) 
	{
		Fire(useVariant, variantSpread);
	}

	[ContextMenu("Shoot Gun")]
	public void ShootGun()
	{
		Fire(false, 0);
	}

	private void Fire(bool useVariant, float variantSpread) 
    {
		if (currentAmmo <= 0 || isReloading)
		{
			Reload();
			return;
		}

		SFXPlayer.clip = shootSFX;

		try
		{
			if (slider)
			{
				//Move whatever slider the weapon has
				MoveSlider();
			}
		}
		catch (NullReferenceException ex)
		{
			Debug.Log("Slider may not be set");
		}

		//eject bullet casings
		EjectCasing();
		//Spawn as many projectiles as we need.
		for (var i = 0; i < shotCount; i++)
		{
			//Determine a random spread value using all of our multipliers.
			Vector3 spreadValue = UnityEngine.Random.insideUnitSphere * (spread);
			if (useVariant) 
			{
				spreadValue = UnityEngine.Random.insideUnitSphere * (variantSpread);
			}
			//Remove the forward spread component, since locally this would go inside the object we're shooting!
			spreadValue.z = 0;
			//Convert to world space.
			spreadValue = bulletSpawnPoint.TransformDirection(spreadValue);

			//Spawn projectile from the projectile spawn point.
			GameObject projectile = Instantiate(gunProjectile, bulletSpawnPoint.position, Quaternion.Euler(bulletSpawnPoint.eulerAngles + spreadValue));
			//Override projectile damage.
			projectile.GetComponent<EnemyProjectile>().projectileDamage = actions.aiAgent.config.damage;
			//Add velocity to the projectile.
			projectile.GetComponent<Rigidbody>().velocity = projectile.transform.forward * projectileImpulse;
		}

		currentAmmo -= 1;

		SFXPlayer.Play();
	}

	private void MoveSlider() 
	{
		slider.DOLocalMoveZ(sliderNewZ, sliderDuration, false).OnComplete(() => {
			slider.DOLocalMoveZ(sliderOriginZ, sliderDuration, false);
		});
	}

	private void Reload()
	{
		if (isReloading) 
		{
			return;
		}
		isReloading = true;
		currentAmmo = ammoPerClip;
		StartCoroutine(actions.ReloadWeapon());
	}

	public void SetIsReload(bool temp) 
	{
		isReloading = temp;
	}

	public void PlayReloadSFX() 
	{
		SFXPlayer.clip = reloadSFX;
		SFXPlayer.Play();
	}

	private void EjectCasing()
	{
		//Spawn casing prefab at spawn point.
		if (prefabCasing != null && socketEjection != null)
			Instantiate(prefabCasing, socketEjection.position, socketEjection.rotation);
	}
}
