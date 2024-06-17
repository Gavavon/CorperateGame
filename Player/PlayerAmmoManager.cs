using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAmmoManager : MonoBehaviour
{
	public static List<Weapon> playersWeapons = new List<Weapon>();

	public void AddPlayerWeapons(Weapon wep) 
	{
		playersWeapons.Add(wep);
	}

	public void SetUpWeaponAmmo(Weapon wep) 
	{
		wep.SetAmmoReserves(wep.totalWepAmmoReserves);
	}

	public void RefillAmmoReserves(Weapon wep, int amount) 
	{
		//check if ammount ends up being greater than ammo reserves
	}
}
