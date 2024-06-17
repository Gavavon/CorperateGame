using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineGlassBreakHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject glassBreak;

	private enum GlassType
	{
		Breakable,
		PunchProof,
		BulletProof,
		UnBreakable
	}

	[SerializeField]
	private GlassType glassType = GlassType.Breakable;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Projectile" && glassType != GlassType.BulletProof && glassType != GlassType.UnBreakable)
		{
			Destroy(gameObject);
			GetComponentInParent<VendingMachineHandler>().BreakVendingMachine();
		}
	}
	void OnTriggerEnter(Collider collision)
	{
		if (collision.gameObject.tag == "Melee" && glassType != GlassType.PunchProof && glassType != GlassType.UnBreakable)
		{
			Instantiate(glassBreak, this.transform.position, Quaternion.identity);
			Destroy(gameObject);
			GetComponentInParent<VendingMachineHandler>().BreakVendingMachine();
		}
	}
}
