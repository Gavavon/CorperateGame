using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunVendingMachineHandler : MonoBehaviour
{
	[SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much")]
	private int gunPrice = 50;


	[SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much rounding down")]
	private int stock = 1;

	private GameObject player;

	[SerializeField]
	private List<GameObject> guns;

	private GunVendingInteraction interactor;

	private void Start()
	{
		interactor = GetComponentInChildren<GunVendingInteraction>();

		interactor.SetText("$" + gunPrice + " FOR A GUN");

		player = GameObject.FindGameObjectsWithTag("Player")[0];

		if (player == null)
		{
			Debug.Log("No Player Found");
		}
	}

	public void GunVendingMachineInteract()
	{
		if (stock <= 0)
		{
			interactor.SetText("OUT OF STOCK");
			//play a noise
			return;
		}
		stock -= 1;

		player.GetComponent<PlayerIncomeHandler>().TakeMoney(gunPrice);

		
		int tempChoice = Random.Range(0, guns.Count);

		player.GetComponent<Character>().AddWeaponToInventory(guns[tempChoice]);

		//play a noise

		guns.RemoveAt(tempChoice);
	}
}
