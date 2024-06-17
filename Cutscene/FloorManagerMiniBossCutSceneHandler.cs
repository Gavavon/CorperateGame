using DG.Tweening;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManagerMiniBossCutSceneHandler : MonoBehaviour
{
	public GameObject bossCutOut;

	public void TurnOnBossIntro()
	{
		StartCoroutine(ShowBossCutOut());
	}

	private IEnumerator ShowBossCutOut() 
	{
		yield return new WaitForSeconds(1.6f);
		bossCutOut.SetActive(true);
	}

	#region Register with Lua
	void OnEnable()
	{
		// Make the functions available to Lua: (Replace these lines with your own.)
		Lua.RegisterFunction(nameof(TurnOnBossIntro), this, SymbolExtensions.GetMethodInfo(() => TurnOnBossIntro()));
	}

	void OnDisable()
	{
		// Remove the functions from Lua: (Replace these lines with your own.)
		Lua.UnregisterFunction(nameof(TurnOnBossIntro));
	}
	#endregion
}
