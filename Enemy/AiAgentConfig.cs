using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AiAgentConfig : ScriptableObject
{

	[Title(label: "General Info")]
	[Header("Health")]
	[Tooltip("Max health enemy has")]
	[SerializeField]
	public float maxHealth = 1;

	[Header("Damage Out")]
	[Tooltip("This is damage per bullet or per swing")]
	[SerializeField]
	public int damage = 1;

	[Header("Damage In")]
	[Tooltip("Damage multiplyer added to head")]
	[SerializeField]
	public float rangedHeadDamageModifier = 1;
	[Tooltip("Damage multiplyer added to body")]
	[SerializeField]
	public float rangedBodyDamageModifier = 1;
	[Tooltip("Damage multiplyer added to arms")]
	[SerializeField]
	public float rangedArmDamageModifier = 1;
	[Tooltip("Damage multiplyer added to legs")]
	[SerializeField]
	public float rangedLegDamageModifier = 1;

	[Tooltip("Damage multiplyer added to head")]
	[SerializeField]
	public float meleeHeadDamageModifier = 1;
	[Tooltip("Damage multiplyer added to body")]
	[SerializeField]
	public float meleeBodyDamageModifier = 1;
	[Tooltip("Damage multiplyer added to arms")]
	[SerializeField]
	public float meleeArmDamageModifier = 1;
	[Tooltip("Damage multiplyer added to legs")]
	[SerializeField]
	public float meleeLegDamageModifier = 1;

	public enum AiBehaviors
	{
		None,
		ProvokableWorkers,
		Defenders,
		Attackers,
		Suppressors,
		Chargers,
		Unique
	}
	
	[Title(label: "Enemy Types")]
	public AiBehaviors enemyBehaviors = AiBehaviors.None;

	
	[Title(label: "Behaviour Tree Node Edits")]

	//variable name should the name of the node and the bool we are editing
	#region Attacker Tree
	[Header("Attacker Tree")]
	[Tooltip("On the MoveForward Node does the enemy take potshots")]
	[SerializeField]
	public bool moveForwardPotShots = false;
	[Tooltip("On the MoveForward Node does the enemy stop when they take potshots")]
	[SerializeField]
	public bool moveForwardPotShotStop = false;
	#endregion

}
