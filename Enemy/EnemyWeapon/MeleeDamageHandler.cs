using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageHandler : MonoBehaviour
{
	private AiActions actions;

	private void Start()
	{
		actions = GetComponentInParent<AiActions>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player")// && actions.swordCanDealDamage) 
		{
			if (actions.recentlyCharging) 
			{
				other.GetComponent<PlayerHealthSystem>().TakeDamageAmount(actions.aiAgent.config.damage*2);
				return;
			}
			other.GetComponent<PlayerHealthSystem>().TakeDamageAmount(actions.aiAgent.config.damage);
		}
	}
}
