using MoreMountains.Tools;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthSystem : MonoBehaviour
{
	[SerializeField]
	private int maxHealth;
	public int currentHealth;

	[HideInInspector]
	public int correctedHealth = 100;

	public bool inCombat = false;

	private MMProgressBar hudHealthInfo;

	private HudInfoHandler hudInfo;

	public EnemyManager enemyManager;

	private bool dead = false;

	private bool hasInfiniteHealth = false;

	private void Start()
	{
		hasInfiniteHealth = DEBUGPlayerStats.GetInfiniteHealth();

		hudInfo = GameObject.FindGameObjectWithTag("HUD").GetComponent<HudInfoHandler>();
		hudHealthInfo = hudInfo.mmPlayerHealthBar.GetComponent<MMProgressBar>();

		currentHealth = maxHealth;

		Scene scene = SceneManager.GetActiveScene();

		if (scene.name != "HubWorld")
		{
			enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
		}

		if (currentHealth != correctedHealth)
		{
			SetHealthTo(correctedHealth);
		}
	}

	public void SetHealthTo(int targetAmount) 
	{
		switch (true)
		{
			case true when currentHealth < targetAmount:
				HealAmount(targetAmount - currentHealth);
				break;
			case true when currentHealth > targetAmount:
				TakeDamageAmount(targetAmount - currentHealth);
				break;
		}
	}

	public void TakeDamageAmount(int amount) 
    {
		inCombat = true;
		if (hasInfiniteHealth)
		{
			return;
		}
		hudHealthInfo.MinusXPercent((float)amount / (float)maxHealth);
		currentHealth -= amount;		
		if (currentHealth <= 0) 
		{
			Die();
		}
    }
	public void TakeDamagePercent(float amount)
	{
		inCombat = true;
		hudHealthInfo.MinusXPercent(amount);
		currentHealth -= (int)(currentHealth*amount);
		if (currentHealth <= 0)
		{
			Die();
		}
	}

	public void HealAmount(int amount)
	{
		hudHealthInfo.PlusXPercent((float)amount / (float)maxHealth);
		if (maxHealth > currentHealth + amount) 
		{
			currentHealth = maxHealth;
			return;
		}
		currentHealth += amount;
	}

	public void HealPercent(int amount)
	{
		hudHealthInfo.PlusXPercent(amount);
		if (maxHealth > currentHealth + amount)
		{
			currentHealth = maxHealth;
			return;
		}
		currentHealth += (int)(currentHealth / amount);
	}

	private void Die() 
	{
		if (dead) 
		{
			return;
		}
		dead = true;
		enemyManager.TurnEnemiesOff();
		//display a new menu
		hudInfo.ShowDeathScreen();
	}

}
