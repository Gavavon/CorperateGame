using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ALPHADEBUGHandler : MonoBehaviour
{
	public void ToggleInfiniteAmmo(bool toggle) 
    {
        if (toggle) 
        {
			DEBUGPlayerStats.SetInfiniteAmmo(true);
			return;
        }
		DEBUGPlayerStats.SetInfiniteAmmo(false);
	}

	public void ToggleInfiniteHealth(bool toggle)
	{
		if (toggle)
		{
			DEBUGPlayerStats.SetInfiniteHealth(true);
			return;
		}
		DEBUGPlayerStats.SetInfiniteHealth(false);
	}
}
