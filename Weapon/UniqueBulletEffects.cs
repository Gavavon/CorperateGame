using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueBulletEffects : MonoBehaviour
{
    public enum BulletType 
    {
        ammoHoarder,
        none
    }
    public BulletType type = BulletType.none;

	[HideInInspector]
	public Weapon wep;

	public void RunBulletEffects(GameObject obj) 
    {
		List<string> tags = new List<string>() {"Enemy", "MiniBoss", "Boss"};

		GameObject correctedObject;

		if (!tags.Contains(obj.tag))
		{
			correctedObject = FindTagOnParent(obj.transform, tags);
		}
		else 
		{
			correctedObject = obj;
		}

		if (correctedObject == null) 
		{
			return;
		}

		switch (correctedObject.tag) 
		{
			case "Enemy":
				switch (type)
				{
					case BulletType.ammoHoarder:
						wep.IncreaseAmmoReserves(1);
						break;
				}
				break;
				/*
			case "Miniboss":
				break;
			case "Boss":
				break;
			case "Enviroment":
				break;
			default:
				break;
				*/
		}
	}


	public GameObject FindTagOnParent(Transform obj, List<string> tags) 
	{
		if (obj.parent == null) 
		{
			return null;
		}

		for (int i = 0; i < tags.Count; i++) 
		{
			if (obj.parent.tag == tags[i])
			{
				return obj.parent.gameObject;
			}
		}
		
		return FindTagOnParent(obj.parent, tags);
	}

}
