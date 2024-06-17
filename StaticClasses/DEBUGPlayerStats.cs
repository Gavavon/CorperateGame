using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class DEBUGPlayerStats
{
	private static bool infiniteAmmo = false;
	private static bool infiniteHealth = false;

	public static void SetInfiniteAmmo(bool ammoBool) 
	{
		infiniteAmmo = ammoBool;
	}

	public static bool GetInfiniteAmmo()
	{
		return infiniteAmmo;
	}

	public static void SetInfiniteHealth(bool healthBool)
	{
		infiniteHealth = healthBool;
	}

	public static bool GetInfiniteHealth()
	{
		return infiniteHealth;
	}

}
