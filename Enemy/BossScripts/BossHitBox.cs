using InfimaGames.LowPolyShooterPack.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHitBox : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	private BossHitBoxManager hitBoxManager;

	private enum BodyPart
	{
		None,
		Body,
		Leg,
		Arm,
		Head
	}
	[SerializeField]
	private BodyPart bodyPart;

	private void Start()
	{
		hitBoxManager = GetComponentInParent<BossHitBoxManager>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Projectile")
		{
			float damage = collision.gameObject.GetComponent<Projectile>().projectileDamage;
			switch (bodyPart)
			{
				case BodyPart.Head:
					damage *= hitBoxManager.rangedHeadDamageModifier;
					break;
				case BodyPart.Body:
					damage *= hitBoxManager.rangedBodyDamageModifier;
					break;
				case BodyPart.Arm:
					damage *= hitBoxManager.rangedArmDamageModifier;
					break;
				case BodyPart.Leg:
					damage *= hitBoxManager.rangedLegDamageModifier;
					break;
			}
			hitBoxManager.SendDamage(damage);
		}
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Melee")
		{
			float damage = other.gameObject.GetComponent<PlayerMeleeDamageDealer>().meleeDamage;
			switch (bodyPart)
			{
				case BodyPart.Head:
					damage *= hitBoxManager.meleeHeadDamageModifier;
					break;
				case BodyPart.Body:
					damage *= hitBoxManager.meleeBodyDamageModifier;
					break;
				case BodyPart.Arm:
					damage *= hitBoxManager.meleeArmDamageModifier;
					break;
				case BodyPart.Leg:
					damage *= hitBoxManager.meleeLegDamageModifier;
					break;
			}
			hitBoxManager.SendDamage(damage);
		}
	}
}
