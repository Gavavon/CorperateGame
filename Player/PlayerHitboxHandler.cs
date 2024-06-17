using InfimaGames.LowPolyShooterPack.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxHandler : MonoBehaviour
{
	private PlayerHealthSystem healthSystem;

	private void Start()
	{
		healthSystem = GetComponent<PlayerHealthSystem>();
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "EnemyProjectile")
		{
			healthSystem.TakeDamageAmount((int)other.gameObject.GetComponent<EnemyProjectile>().projectileDamage);
			Destroy(other.gameObject);
		}
	}
}
