using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using MoreMountains.Tools;
using InfimaGames.LowPolyShooterPack;
using Michsky.LSS;

public class HudInfoHandler : MonoBehaviour
{
	[SerializeField]
	public MMProgressBar mmPlayerHealthBar;

	[SerializeField]
	private TMP_Text currentCredits;

	[SerializeField]
	private TMP_Text questTimer;

	[SerializeField]
	private Color negativeCC;
	[SerializeField]
	private Color positiveCC;

	[SerializeField]
	private RectTransform newGunAlert;

	[SerializeField]
	private RectTransform newSwordAlert;

	[SerializeField]
	private RectTransform bossHealthBar;
	public MMProgressBar mmBossHealthBar;

	[SerializeField]
	private DeathMenuHandler deathMenu;
	[HideInInspector]
	[SerializeField]
	public bool deathMenuShown = false;

	[SerializeField]
	private GameObject standImage;
	[SerializeField]
	private GameObject crouchImage;

	private void Start()
	{
		ToggleCrouchImages(false);
	}


	public void ShowGunAlertGetter() 
	{
		StartCoroutine(ShowGunAlert());
	}

	public void ShowSwordAlertGetter()
	{
		StartCoroutine(ShowSwordAlert());
	}

	public void ShowTimer() 
	{
		questTimer.gameObject.SetActive(true);
	}

	public void HideTimer()
	{
		questTimer.gameObject.SetActive(true);
	}

	public void UpdateHud(int cc) 
    {
		if (cc < 0)
		{
			currentCredits.color = negativeCC;
		}
		else 
		{
			currentCredits.color = positiveCC;
		}
		currentCredits.text = "CC: " + cc;
    }

	private IEnumerator ShowGunAlert() 
	{
		newGunAlert.DOAnchorPosX(0, 0.5f);
		yield return new WaitForSeconds(2);
		newGunAlert.DOAnchorPosX(-380, 0.5f);
	}
	private IEnumerator ShowSwordAlert()
	{
		newSwordAlert.DOAnchorPosX(0, 0.5f);
		yield return new WaitForSeconds(2);
		newSwordAlert.DOAnchorPosX(-380, 0.5f);
	}

	public void ShowBossHealthBar()
	{
		bossHealthBar.DOAnchorPosX(780, 0.5f);
	}
	public void HideBossHealthBar()
	{
		bossHealthBar.DOAnchorPosX(1000, 0.5f);
	}

	public void ShowDeathScreen() 
	{
		deathMenuShown = true;
		deathMenu.gameObject.SetActive(true);
		deathMenu.StartCalc();
	}

	public void CloseDeathScreen() 
	{
		deathMenuShown = false;
		deathMenu.gameObject.SetActive(false);
	}

	public void ToggleCrouchImages(bool crouching) 
	{
		switch (crouching) 
		{
			case false:
				standImage.SetActive(true);
				crouchImage.SetActive(false);
				break;
			case true:
				standImage.SetActive(false);
				crouchImage.SetActive(true);
				break;
				
		}

	}

	#region Register with Lua
	void OnEnable()
	{
		// Make the functions available to Lua: (Replace these lines with your own.)
		Lua.RegisterFunction(nameof(ShowTimer), this, SymbolExtensions.GetMethodInfo(() => ShowTimer()));
		Lua.RegisterFunction(nameof(HideTimer), this, SymbolExtensions.GetMethodInfo(() => HideTimer()));
	}

	void OnDisable()
	{
		// Remove the functions from Lua: (Replace these lines with your own.)
		Lua.UnregisterFunction(nameof(ShowTimer));
		Lua.UnregisterFunction(nameof(HideTimer));
	}
	#endregion
}
