using InfimaGames.LowPolyShooterPack.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyHitbox : MonoBehaviour
{
	[SerializeField]
	private DummyEnemyHandler enemyInfo;

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Projectile")
		{
			//Debug.Log(damage);
			enemyInfo.Die();
		}
	}
}
