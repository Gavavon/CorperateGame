using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using PixelCrushers.DialogueSystem;
using UnityEngine.InputSystem.Users;

public class ComputerManager : MonoBehaviour
{

    public GameObject computerUI;

	public CanvasGroup loadingScreen;

	public GameObject lockScreen;

	public GameObject desktopScreen;

	public GameObject dialogueMenu;

	public bool computerInUse = false;

	// Start is called before the first frame update
	void Start()
    {
		loadingScreen.alpha = 0f;
	}

	public bool UsingComputer() 
	{
		return computerInUse;
	}

    public void TurnOnComputer() 
    {
		computerInUse = true;
		dialogueMenu.SetActive(false);
		FlashCompanyLogo();

	}

    public void TurnOffComputer() 
    {
		loadingScreen.gameObject.SetActive(false);
		lockScreen.SetActive(false);
		desktopScreen.SetActive(false);
		computerInUse = false;
		dialogueMenu.SetActive(true);
	}

	public void UnlockComputer() 
	{
		DOTween.To(() => lockScreen.GetComponent<CanvasGroup>().alpha, x => lockScreen.GetComponent<CanvasGroup>().alpha = x, 0, 0.5f).OnComplete(() => {
			lockScreen.SetActive(false);
			desktopScreen.SetActive(true);
		});
	}

    public void FlashCompanyLogo()
    {
        loadingScreen.alpha = 0f;
        loadingScreen.gameObject.SetActive(true);
		DOTween.To(() => loadingScreen.alpha, x => loadingScreen.alpha = x, 1, 0.75f).OnComplete(() => {
			DOTween.To(() => loadingScreen.alpha, x => loadingScreen.alpha = x, 0, 0.75f).OnComplete(() => {
				if (!computerInUse) 
				{
					loadingScreen.gameObject.SetActive(false);
					lockScreen.SetActive(true);
				}
			});
		});

	}

	#region Register with Lua
	void OnEnable()
	{
		// Make the functions available to Lua: (Replace these lines with your own.)
		Lua.RegisterFunction(nameof(UsingComputer), this, SymbolExtensions.GetMethodInfo(() => UsingComputer()));
		Lua.RegisterFunction(nameof(TurnOnComputer), this, SymbolExtensions.GetMethodInfo(() => TurnOnComputer()));
		Lua.RegisterFunction(nameof(TurnOffComputer), this, SymbolExtensions.GetMethodInfo(() => TurnOffComputer()));

	}

	void OnDisable()
	{
		// Remove the functions from Lua: (Replace these lines with your own.)
		Lua.UnregisterFunction(nameof(UsingComputer));
		Lua.UnregisterFunction(nameof(TurnOnComputer));
		Lua.UnregisterFunction(nameof(TurnOffComputer));
	}
	#endregion

	//https://www.youtube.com/watch?v=55I0psnQp7Q


}
