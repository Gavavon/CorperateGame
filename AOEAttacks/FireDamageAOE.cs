using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamageAOE : MonoBehaviour
{
	[SerializeField]
	private int initalDamageChunck = 0;
	[SerializeField]
	private int damagePerSecond = 1;
	[SerializeField]
	private float damageRateSeconds = 1f;

	[HideInInspector]
	[SerializeField]
	private bool canTakeDamage = true;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.GetComponent<PlayerHealthSystem>().TakeDamageAmount(initalDamageChunck);
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "Player" && canTakeDamage)
		{
			other.GetComponent<PlayerHealthSystem>().TakeDamageAmount(damagePerSecond);
			StartCoroutine(DamageWaiter());
			canTakeDamage = false;
		}
	}

	public IEnumerator DamageWaiter()
	{
		yield return new WaitForSeconds(damageRateSeconds);
		canTakeDamage = true;
	}
}
