using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeVendingMachineHandler : MonoBehaviour
{
	[SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much")]
	private int meleePrice = 50;


	[SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much rounding down")]
	private int stock = 1;

	private GameObject player;

	[SerializeField]
	private List<GameObject> swords;

	private MeleeVendingInteraction interactor;

	private void Start()
	{
		interactor = GetComponentInChildren<MeleeVendingInteraction>();

		interactor.SetText("$" + meleePrice + " FOR A SWORD");

		player = GameObject.FindGameObjectsWithTag("Player")[0];

		if (player == null)
		{
			Debug.Log("No Player Found");
		}
	}

	public void MeleeVendingMachineInteract()
	{
		if (stock <= 0)
		{
			interactor.SetText("OUT OF STOCK");
			//play a noise
			return;
		}
		stock -= 1;

		player.GetComponent<PlayerIncomeHandler>().TakeMoney(meleePrice);

		int tempChoice = Random.Range(0, swords.Count);

		player.GetComponent<Character>().ChangeMelee(swords[tempChoice]);

		//play a noise

		swords.RemoveAt(tempChoice);
	}
}
