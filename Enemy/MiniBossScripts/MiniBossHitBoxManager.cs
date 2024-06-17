using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossHitBoxManager : MonoBehaviour
{
	public float rangedHeadDamageModifier = 1;
	public float rangedBodyDamageModifier = 1;
	public float rangedArmDamageModifier = 1;
	public float rangedLegDamageModifier = 1;

	public float meleeHeadDamageModifier = 1;
	public float meleeBodyDamageModifier = 1;
	public float meleeArmDamageModifier = 1;
	public float meleeLegDamageModifier = 1;

	private ManagerActions actions;
	

	// Start is called before the first frame update
	void Start()
    {
		actions = GetComponent<ManagerActions>();
	}

	public void SendDamage(float damage) 
	{
		actions.TakeDamage(damage);
	}
}
