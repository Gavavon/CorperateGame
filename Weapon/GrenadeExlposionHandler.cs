using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeExlposionHandler : MonoBehaviour
{
	[SerializeField]
	public int explosionDamage = 0;
	[SerializeField]
	private float despawnWaiter = 3f;

	private void Awake()
	{
		StartCoroutine(Despawner());
		StartCoroutine(DeactivateDamage());

	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.GetComponent<PlayerHealthSystem>().TakeDamageAmount(explosionDamage);
		}
		if (other.gameObject.GetComponent<AiActions>() != null)
		{
			other.GetComponent<AiActions>().TakeDamage(explosionDamage);
		}
		if (other.gameObject.tag == "Boss")
		{
			other.GetComponent<BossHealthSystem>().TakeDamage(explosionDamage);
		}
		if (other.gameObject.tag == "MiniBoss")
		{
			other.GetComponent<ManagerActions>().TakeDamage(explosionDamage);
		}
		if (other.gameObject.tag == "Glass") 
		{
			other.GetComponent<GlassBreakHandler>().BreakGlass(gameObject.transform);
		}
		if (other.gameObject.GetComponent<DoorBreakHandler>() != null)
		{
			other.GetComponent<DoorBreakHandler>().BreakDoor(gameObject.transform);
		}
	}

	private IEnumerator Despawner()
	{
		yield return new WaitForSeconds(despawnWaiter);
		Destroy(gameObject);
	}
	private IEnumerator DeactivateDamage()
	{
		yield return new WaitForSeconds(0.5f);
		this.GetComponent<SphereCollider>().enabled = false;
	}
}
