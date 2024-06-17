using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeathMenuHandler : MonoBehaviour
{

	private PlayerProgressManager progressManager;

	public TextMeshProUGUI moneyDebtOrCash;

	public TextMeshProUGUI earningsKillWorkersText;
	public TextMeshProUGUI earningsKillMiniBossesText;
	public TextMeshProUGUI earningsKillBossesText;
	public TextMeshProUGUI earningsClearDepartmentsText;
	public TextMeshProUGUI totalEarningsText;
	public TextMeshProUGUI currentMoneyText;
	public TextMeshProUGUI netProfitsText;

	private int earningsKillWorkers = 0;
	private int earningsKillMiniBosses = 0;
	private int earningsKillBosses = 0;
	private int earningsClearDepartments = 0;
	private int totalEarnings = 0;
	private int currentMoney = 0;
	private int netProfits = 0;

	// Update is called once per frame
	private void Update()
	{
		earningsKillWorkersText.SetText("$" + earningsKillWorkers.ToString());
		earningsKillMiniBossesText.SetText("$" + earningsKillMiniBosses.ToString());
		earningsKillBossesText.SetText("$" + earningsKillBosses.ToString());
		earningsClearDepartmentsText.SetText("$" + earningsClearDepartments.ToString());
		totalEarningsText.SetText("$" + totalEarnings.ToString());

		currentMoneyText.SetText("$" + currentMoney.ToString());
		netProfitsText.SetText("$" + netProfits.ToString());
		try 
		{
			if (progressManager.currentFunds > 0)
			{
				moneyDebtOrCash.SetText("Corperate Credits On Hand:");
			}
			else
			{
				moneyDebtOrCash.SetText("Debts to the Company:");
			}
		}
		catch (Exception ex) 
		{
			
		}
	}

	public void StartCalc() 
	{
		progressManager = GameObject.FindGameObjectWithTag("DeathManager").GetComponent<PlayerProgressManager>();
		StartCoroutine(UpdateMenuInfo());
	}

	private IEnumerator UpdateMenuInfo() 
	{
		progressManager.RunCalculations();
		yield return new WaitForSeconds(1f);
		DOTween.To(() => earningsKillWorkers, x => earningsKillWorkers = x, progressManager.cashEarnedFromEnemies, 2).SetEase(Ease.OutQuad);
		DOTween.To(() => earningsKillMiniBosses, x => earningsKillMiniBosses = x, progressManager.cashEarnedFromMiniBosses, 2).SetEase(Ease.OutQuad);
		DOTween.To(() => earningsKillBosses, x => earningsKillBosses = x, progressManager.cashEarnedFromBosses, 2).SetEase(Ease.OutQuad);
		DOTween.To(() => earningsClearDepartments, x => earningsClearDepartments = x, progressManager.cashEarnedFromDepartmentsCleared, 2).SetEase(Ease.OutQuad).OnComplete(() => {
			DOTween.To(() => totalEarnings, x => totalEarnings = x, progressManager.cashEarned, 2).SetEase(Ease.OutQuad).SetEase(Ease.OutQuad).OnComplete(() => {
				DOTween.To(() => netProfits, x => netProfits = x, progressManager.netProfits, 2).SetEase(Ease.OutQuad);
			});
		});
		DOTween.To(() => currentMoney, x => currentMoney = x, progressManager.currentFunds, 2).SetEase(Ease.OutQuad);
	}
}
