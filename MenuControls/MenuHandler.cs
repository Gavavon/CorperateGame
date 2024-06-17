using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandler : MonoBehaviour
{

	[Header("Main Menu")]
	[SerializeField]
	private RectTransform MainMenu;

	[Header("Options Menu")]
	[SerializeField]
	private RectTransform OptionsMenu; 

	[Header("Portfolio Menu")]
	[SerializeField]
	private RectTransform PortfolioMenu;

	[Header("Gerneral Vars")] 
	[SerializeField]
	private RectTransform MenuOnScreen;
	[SerializeField]
	private RectTransform MenuOffScreen;

	[Header("Menu Blocks")]
	[SerializeField]
	private GameObject BlockMainMenu;
	[SerializeField]
	private GameObject BlockOptionsMenu;
	[SerializeField]
	private GameObject BlockPortfolioMenu;

	[Header("Folder Mover")]
	[SerializeField]
	private RectTransform FolderBottom;
	[SerializeField]
	private RectTransform FolderBottomOnScreen;
	[SerializeField]
	private RectTransform FolderBottomOffScreen;

	#region Resume

	public void OpenPortfolioMenu()
	{
		BlockMainMenu.SetActive(true);
		BlockPortfolioMenu.SetActive(true);
		PortfolioMenu.DOLocalMoveY(MenuOnScreen.localPosition.y, 1f).OnComplete(() => {
			BlockPortfolioMenu.SetActive(false);
		});
	}
	public void ClosePortfolioMenu()
	{
		BlockPortfolioMenu.SetActive(true);
		PortfolioMenu.DOLocalMoveY(MenuOffScreen.localPosition.y + 62.427f, 1f).OnComplete(() => {
			BlockMainMenu.SetActive(false);
		});
	}

	#endregion

	#region Options Menu

	public void OpenOptionsMenu()
	{
		BlockMainMenu.SetActive(true);
		BlockOptionsMenu.SetActive(true);
		OptionsMenu.DOLocalMoveY(MenuOnScreen.localPosition.y, 2f, true).OnComplete(() => {
			FolderBottom.DOLocalMoveY(FolderBottomOffScreen.localPosition.y, 1f).OnComplete(() => {
				BlockOptionsMenu.SetActive(false);
			});
		});
	}
	public void CloseOptionsMenu()
	{
		BlockOptionsMenu.SetActive(true);
		OptionsMenu.DOLocalMoveY(MenuOffScreen.localPosition.y, 2f, true).OnComplete(() => {
			FolderBottom.DOLocalMoveY(FolderBottomOnScreen.localPosition.y, 0f).OnComplete(() => {
				BlockMainMenu.SetActive(false);
				GetComponentInChildren<OptionsMenuHandler>().ActivateGameplayTab();
			});
			
		});
	}

	#endregion
}
