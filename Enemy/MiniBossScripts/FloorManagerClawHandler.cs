using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManagerClawHandler : MonoBehaviour
{
	private ManagerActions actions;

	private void Start()
	{
		actions = GetComponentInParent<ManagerActions>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player" && !actions.dead)
		{
			other.gameObject.GetComponent<PlayerHealthSystem>().TakeDamageAmount(actions.clawDamage);
		}
	}
}
