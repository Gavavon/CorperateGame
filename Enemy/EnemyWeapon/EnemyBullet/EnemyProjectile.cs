using InfimaGames.LowPolyShooterPack.Legacy;
using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	[Tooltip("How much damage this projectile does")]
	public float projectileDamage = 1;

	[Range(2, 100)]
	[Tooltip("After how long time should the bullet prefab be destroyed?")]
	public float destroyAfter;

	public Transform[] metalImpactPrefabs;
	public Transform[] dirtImpactPrefabs;
	public Transform[] concreteImpactPrefabs;
	public Transform[] glassImpactPrefabs;
	public Transform[] defaultImpactPrefabs;

	private void Start()
	{
		//Start destroy timer
		StartCoroutine(DestroyAfter());
	}

	//If the bullet collides with anything
	private void OnCollisionEnter(Collision collision)
	{
		//If bullet collides with "Metal" tag
		if (collision.transform.tag == "Metal")
		{
			//Instantiate random impact prefab from array
			Instantiate(metalImpactPrefabs[Random.Range
					(0, metalImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "Dirt" tag
		if (collision.transform.tag == "Dirt")
		{
			//Instantiate random impact prefab from array
			Instantiate(dirtImpactPrefabs[Random.Range
					(0, dirtImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "Concrete" tag
		if (collision.transform.tag == "Concrete")
		{
			//Instantiate random impact prefab from array
			Instantiate(concreteImpactPrefabs[Random.Range
					(0, concreteImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "Glass" tag
		if (collision.transform.tag == "Glass")
		{
			//Instantiate random impact prefab from array
			Instantiate(glassImpactPrefabs[Random.Range
					(0, glassImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}

		//If bullet collides with "Default" tag or untagged items
		if (collision.transform.tag == "Default" || collision.transform.tag == "Untagged")
		{
			//Instantiate random impact prefab from array
			Instantiate(defaultImpactPrefabs[Random.Range
					(0, defaultImpactPrefabs.Length)], transform.position,
				Quaternion.LookRotation(collision.contacts[0].normal));
			//Destroy bullet object
			Destroy(gameObject);
		}
		Destroy(gameObject);
	}

	private IEnumerator DestroyAfter()
	{
		//Wait for set amount of time
		yield return new WaitForSeconds(destroyAfter);
		//Destroy bullet object
		Destroy(gameObject);
	}
}
