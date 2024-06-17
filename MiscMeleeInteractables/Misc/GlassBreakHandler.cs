using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreakHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject glassBreak;

	[SerializeField]
	private GameObject brokenGlassPieces;

	public float breakForce;

	private Transform player;

	[SerializeField]
	private Transform glassBreakSpawnLocation;

	private enum GlassType 
	{
		Breakable,
		PunchProof,
		BulletProof,
		UnBreakable
	}

	[SerializeField]
	private GlassType glassType = GlassType.Breakable;

	private void Start()
	{
		player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
	}

	public void BreakGlass(Transform directionObject, bool projectileHit = false)
	{
		if (!projectileHit) 
		{
			Instantiate(glassBreak, glassBreakSpawnLocation.position, Quaternion.identity);

		}
		this.GetComponent<BoxCollider>().enabled = false;

		GameObject pieces = Instantiate(brokenGlassPieces, transform.position, transform.rotation);
		foreach (Rigidbody rb in pieces.GetComponentsInChildren<Rigidbody>())
		{
			//Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;

			//float amountToMove = 5;
			Vector3 dPosition = transform.position;
			Vector3 pPosition = directionObject.position;

			Vector3 dest = new Vector3(
				Mathf.Cos(Random.Range(-0.5f, 0.5f) + Mathf.Atan((dPosition.z - pPosition.z) / (dPosition.x - pPosition.x))) * breakForce,
				0,
				Mathf.Sin(Random.Range(-0.5f, 0.5f) + Mathf.Atan((dPosition.z - pPosition.z) / (dPosition.x - pPosition.x))) * breakForce
				);

			Vector3 fixedDest;

			if (pPosition.x > dPosition.x)
			{
				fixedDest = new Vector3(dPosition.x - dest.x, dPosition.y - dest.y, dPosition.z - dest.z);
			}
			else
			{
				fixedDest = new Vector3(dPosition.x + dest.x, dPosition.y + dest.y, dPosition.z + dest.z);
			}
			rb.AddForce(fixedDest);
			Destroy(gameObject);
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Projectile" || collision.gameObject.tag == "EnemyProjectile" && glassType != GlassType.BulletProof && glassType != GlassType.UnBreakable) 
		{
			//Instantiate(glassBreak, this.transform.position, Quaternion.identity);
			BreakGlass(collision.gameObject.transform, true);
		}
	}
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Melee" && glassType != GlassType.PunchProof && glassType != GlassType.UnBreakable)
		{
			BreakGlass(player);
		}
		if (collision.gameObject.tag == "Enemy" && glassType != GlassType.PunchProof && glassType != GlassType.UnBreakable)
		{
			BreakGlass(collision.gameObject.transform);
		}
	}
}
