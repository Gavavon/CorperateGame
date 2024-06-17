using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIncomeHandler : MonoBehaviour
{
	[SerializeField]
	public int currentCC = 0;

	private int ccIncome = 50;

	private HudInfoHandler HUD;

	private void Start()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag("HUD");
		HUD = temp[0].GetComponent<HudInfoHandler>();

		UpdateUIs();
	}

	public void UpdateUIs()
	{
		//Below is HUD menu
		HUD.UpdateHud(currentCC);

		//Below is the pause menu object
		//uiHolder.qualitySettingsPrefab

		//the pause menu should say the department, your income, the profits the company takes
	}

	public void TakeMoney(int amount)
	{
		currentCC -= amount;
		HUD.UpdateHud(currentCC);
	}

	public void GiveMoney(int amount)
	{
		currentCC += amount;
		HUD.UpdateHud(currentCC);
	}
}
