using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProgressManager : MonoBehaviour
{
    public static int miniBossesKilled = 0;
    public static int bossesKilled = 0;
    public static int enemiesKilled = 0;
	public static int departmentsCleared = 0;

    public int cashEarned = 0;

    public int cashEarnedFromMiniBosses = 0;
    public int cashEarnedFromBosses = 0;
    public int cashEarnedFromEnemies = 0;
    public int cashEarnedFromDepartmentsCleared = 0;
	public int currentDebt = 0;
	public int netProfits = 0;

	public int currentFunds = 0;


	public enum StatTypes 
	{
		enemy,
		miniBoss,
		boss,
		department
	}

	public void IncreaseStat(StatTypes stat, int amount = 1) 
	{
		switch (stat) 
		{
			case StatTypes.enemy:
				enemiesKilled += amount;
				return;
			case StatTypes.miniBoss:
				miniBossesKilled += amount;
				return;
			case StatTypes.boss:
				bossesKilled += amount;
				return;
			case StatTypes.department:
				departmentsCleared += amount;
				return;
		}
	}

	public void CalcMiniBossCash() 
    {
        for (int i = 0; i < miniBossesKilled; i++) 
        {
            cashEarnedFromMiniBosses += 300;
		}
		cashEarned += cashEarnedFromMiniBosses;
	}
	public void CalcBossCash()
	{
		for (int i = 0; i < bossesKilled; i++)
		{
			cashEarnedFromBosses += 500;
		}
		cashEarned += cashEarnedFromBosses;
	}
	public void CalcEnemyCash()
	{
		for (int i = 0; i < enemiesKilled; i++)
		{
			cashEarnedFromEnemies += 10;
		}
		cashEarned += cashEarnedFromEnemies;
	}

	public void CalcDepartmentsClearedCash()
	{
		for (int i = 0; i < departmentsCleared; i++)
		{
			cashEarnedFromDepartmentsCleared += 1000;
		}
		cashEarned += cashEarnedFromDepartmentsCleared;
	}

	public void RunCalculations() 
	{
		currentFunds = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerIncomeHandler>().currentCC;

		CalcEnemyCash();
		CalcMiniBossCash();
		CalcBossCash();
		CalcDepartmentsClearedCash();

		netProfits = currentFunds + cashEarned;
	}

	public void ResetStats() 
    {
        
    }
}
