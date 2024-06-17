using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceptionistStats : MonoBehaviour
{
    private static double npcOpinion = 0;


    public void IncreaseOpinion(double amount) 
    {
		npcOpinion += amount;
	}

	public void DecreaseOpinion(double amount)
	{
		npcOpinion -= amount;
	}

	public double CheckOpinion() 
    {
        return npcOpinion;
    }

	#region Register with Lua
	void OnEnable()
	{
		// Make the functions available to Lua: (Replace these lines with your own.)
		Lua.RegisterFunction(nameof(IncreaseOpinion), this, SymbolExtensions.GetMethodInfo(() => IncreaseOpinion((double)0)));
		Lua.RegisterFunction(nameof(DecreaseOpinion), this, SymbolExtensions.GetMethodInfo(() => DecreaseOpinion((double)0)));
		Lua.RegisterFunction(nameof(CheckOpinion), this, SymbolExtensions.GetMethodInfo(() => CheckOpinion()));

	}

	void OnDisable()
	{
		// Remove the functions from Lua: (Replace these lines with your own.)
		Lua.UnregisterFunction(nameof(IncreaseOpinion));
		Lua.UnregisterFunction(nameof(DecreaseOpinion));
		Lua.UnregisterFunction(nameof(CheckOpinion));
	}
	#endregion

}
