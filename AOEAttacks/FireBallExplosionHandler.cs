using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallExplosionHandler : MonoBehaviour
{
	[SerializeField]
	public int explosionDamage = 0;
	[SerializeField]
	private float despawnWaiter = 3f;

	private void Awake()
	{
		StartCoroutine(despawner());
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			other.GetComponent<PlayerHealthSystem>().TakeDamageAmount(explosionDamage);
			this.GetComponent<SphereCollider>().enabled = false;
		}
	}

	private IEnumerator despawner() 
	{
		yield return new WaitForSeconds(despawnWaiter);
		Destroy(gameObject);
	}
}
