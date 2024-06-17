using InfimaGames.LowPolyShooterPack.Legacy;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	[HideInInspector]
	[SerializeField]
	private AiActions actions;

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
		actions = GetComponentInParent<AiActions>();
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Projectile")
		{
			float damage = collision.gameObject.GetComponent<Projectile>().projectileDamage;
			switch (bodyPart)
			{
				case BodyPart.Head:
					damage *= actions.aiAgent.config.rangedHeadDamageModifier;
					break;
				case BodyPart.Body:
					damage *= actions.aiAgent.config.rangedBodyDamageModifier;
					break;
				case BodyPart.Arm:
					damage *= actions.aiAgent.config.rangedArmDamageModifier;
					break;
				case BodyPart.Leg:
					damage *= actions.aiAgent.config.rangedLegDamageModifier;
					break;
			}
			//Debug.Log(damage);
			if (actions.aiAgent.stateMachine.currentState == AiStateId.Stunned) 
			{
				damage = damage * 2;
			}
			actions.TakeDamage(damage);
		}
		/*
		switch (collision.gameObject.tag) 
		{
			case "Projectile":
				float damage = collision.gameObject.GetComponent<Projectile>().projectileDamage;
				switch (bodyPart)
				{
					case BodyPart.Head:
						damage *= headDMGMod;
						break;
					case BodyPart.Body:
						damage *= BodyDMGMod;
						break;
					case BodyPart.Arm:
						damage *= ArmDMGMod;
						break;
					case BodyPart.Leg:
						damage *= LegDMGMod;
						break;
				}
				//Debug.Log(damage);
				actions.TakeDamage(damage);
				break;
			case "Melee":
				Debug.Log("Here");
				if (actions.aiType == AiAgentConfig.AiBehaviors.Chargers) 
				{
					actions.ChangeEnemyState(AiStateId.Stunned);
				}
				actions.TakeDamage(5);
				break;
		}
		*/
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Melee")
		{
			float damage = other.gameObject.GetComponent<PlayerMeleeDamageDealer>().meleeDamage;

			switch (bodyPart)
			{
				case BodyPart.Head:
					damage *= actions.aiAgent.config.meleeHeadDamageModifier;
					break;
				case BodyPart.Body:
					damage *= actions.aiAgent.config.meleeBodyDamageModifier;
					break;
				case BodyPart.Arm:
					damage *= actions.aiAgent.config.meleeArmDamageModifier;
					break;
				case BodyPart.Leg:
					damage *= actions.aiAgent.config.meleeLegDamageModifier;
					break;
			}

			if (actions.aiAgent.stateMachine.currentState == AiStateId.Stunned)
			{
				damage = damage * 2;
			}

			actions.TakeDamage(damage);

			if (actions.aiType == AiAgentConfig.AiBehaviors.Chargers)
			{
				actions.ChangeEnemyState(AiStateId.Stunned);
			}
		}
		/*
		switch (other.gameObject.tag)
		{
			case "Projectile":
				float damage = other.gameObject.GetComponent<Projectile>().projectileDamage;
				switch (bodyPart)
				{
					case BodyPart.Head:
						damage *= headDMGMod;
						break;
					case BodyPart.Body:
						damage *= BodyDMGMod;
						break;
					case BodyPart.Arm:
						damage *= ArmDMGMod;
						break;
					case BodyPart.Leg:
						damage *= LegDMGMod;
						break;
				}
				//Debug.Log(damage);
				actions.TakeDamage(damage);
				break;
			case "Melee":
				Debug.Log("Here");
				if (actions.aiType == AiAgentConfig.AiBehaviors.Chargers)
				{
					actions.ChangeEnemyState(AiStateId.Stunned);
				}
				actions.TakeDamage(5);
				break;
		}
		*/
	}
}
