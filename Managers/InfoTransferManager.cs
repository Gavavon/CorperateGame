using InfimaGames.LowPolyShooterPack;
using JetBrains.Annotations;
using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InfoTransferManager : MonoBehaviour
{
	public List<GameObject> totalGunList = new List<GameObject>();
	public List<GameObject> playerGunList = new List<GameObject>();
	public List<int> gunReserves = new List<int>();
	public List<int> gunClips = new List<int>();
	public int playerHealth = 100;

	public GameObject player;
	public Transform playerInventory;
	public PlayerHealthSystem playerHealthSystem;

	private void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	public void UpdatePlayerStats(GameObject player, PlayerHealthSystem healthSystem) 
	{
		Scene scene = SceneManager.GetActiveScene();

		if (scene.name == "HubWorld")
		{
			healthSystem.correctedHealth = 100;
			playerHealth = 100;
			playerGunList.Clear();
			gunReserves.Clear();
			gunClips.Clear();
			return;
		}
		if (playerGunList.Count != 0) 
		{
			for (int i = 0; i < playerGunList.Count; i++)
			{
				
				player.GetComponent<Character>().TransferWeaponToInventory(playerGunList[i], gunReserves[i], gunClips[i]);
			}
		}

		//set the players health (preferably in a way that doesnt show any read)
		healthSystem.correctedHealth = playerHealth;
	}

	public void SetPlayerStats() 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerInventory = GameObject.FindGameObjectWithTag("Inventory").transform;
		playerHealthSystem = player.GetComponent<PlayerHealthSystem>();

		playerHealth = playerHealthSystem.currentHealth;

		foreach (Transform child in playerInventory)
		{
			if (child.gameObject.GetComponent<Weapon>().GetWeaponName() == "DefaultHandGun") 
			{
				continue;
			}
			for (int i = 0; i < totalGunList.Count; i++)
			{
				if(child.gameObject.GetComponent<Weapon>().GetWeaponName() == totalGunList[i].gameObject.GetComponent<Weapon>().GetWeaponName())
				{
					if (playerGunList.Contains(totalGunList[i])) 
					{
						int index = playerGunList.FindIndex(a => totalGunList[i] == totalGunList[i]);
						Debug.Log(child.gameObject);
						UpdateAmmoRes(child.gameObject.GetComponent<Weapon>(), index, false);
						continue;
					}
					playerGunList.Add(totalGunList[i]);
					UpdateAmmoRes(child.gameObject.GetComponent<Weapon>(), i, true);
				}
			}
		}
	}
	public void UpdateAmmoRes(Weapon wep, int index, bool add)
	{
		int amount = wep.GetAmmunitionTotal() - wep.GetAmmunitionCurrent();
		int reloadAmount = wep.TakeAmmoFromReserves(amount);
		wep.SetCurrentAmmo(Mathf.Clamp(wep.GetAmmunitionCurrent() + reloadAmount, 0, wep.GetAmmunitionTotal()));
		if (add)
		{
			gunReserves.Add(wep.GetAmmoReservesCurrent());
			gunClips.Add(wep.GetAmmunitionCurrent());
		}
		else
		{
			//this dont work
			gunReserves[index] = wep.GetAmmoReservesCurrent();
			gunClips[index] = wep.GetAmmunitionCurrent();
		}
		
	}
}
