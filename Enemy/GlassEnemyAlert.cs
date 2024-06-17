using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassEnemyAlert : MonoBehaviour
{
	private void OnTriggerStay(Collider collider)
	{
		if (collider.transform.tag == "Enemy")
		{
			//collider.gameObject.GetComponent<AiActions>().AlertEnemy();
		}
	}
}
