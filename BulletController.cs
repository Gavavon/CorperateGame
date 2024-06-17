using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
	/*
	private Rigidbody rb;

	public int forcePower = 0;

	public GameObject player;

	void Awake()
	{
		//rb = this.GetComponent<Rigidbody>();
		/*
		Vector3 direction = -(this.transform.position - player.transform.position);
		direction.Normalize();
		this.GetComponent<Rigidbody>().AddForce(direction * forcePower);
		this.transform.LookAt(player.transform.position);
	}
	*/

	private void OnCollisionEnter(Collision collision)
	{
		switch (collision.gameObject.tag) 
		{
			case "Enemy":
				break;
			case "Boss":
				break;
			case "Enviroment":
				Destroy(this);
				break;
		}
	}
}
