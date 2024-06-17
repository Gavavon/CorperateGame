using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachineHandler : MonoBehaviour
{
    [SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much")]
	private int snackPrice = 50;

	[SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much")]
	private int breakPrice = 250;

	[SerializeField]
	[Tooltip("If randomized it will use this as max down to half as much rounding down")]
	private int stock = 3;

    [SerializeField]
	[Tooltip("this will randomize variables and treat the above variables as max's")]
	private bool randomizeVars = false;

    private GameObject player;

	[SerializeField]
	private List<GameObject> droppedItems;
	[SerializeField]
	private List<GameObject> thrownItems;

	

    private bool vendingMachineBroken = false;

    private VendingMachineInteraction interactor;

	private void Start()
	{
        interactor = GetComponentInChildren<VendingMachineInteraction>();

        interactor.SetText("$" + snackPrice + " FOR A SNACK");

		player = GameObject.FindGameObjectsWithTag("Player")[0];

        if (player == null) 
        {
            Debug.Log("No Player Found");
        }

        if (randomizeVars) 
        {
            snackPrice = Random.Range(snackPrice/2, snackPrice);
            stock = Random.Range(stock/2, stock);
        }
	}

    public void BreakVendingMachine() 
    {
		vendingMachineBroken = true;
		interactor.SetText("OUT OF ORDER");
		player.GetComponent<PlayerIncomeHandler>().TakeMoney(breakPrice);
		StartCoroutine(ThrowHealingItems());
	}

    public void VendingMachineInteract() 
    {
		if (vendingMachineBroken) 
		{
			interactor.SetText("OUT OF ORDER");
			//play a noise
			return;
		}

		if (stock <= 0) 
        {
			interactor.SetText("OUT OF STOCK");
            //play a noise
            return;
		}
        
        player.GetComponent<PlayerIncomeHandler>().TakeMoney(snackPrice);
        
        if (Random.Range(1, 101) < 5) 
        {
            //play a noise
            return;
        }
        DropHealingItem();
	}

	private void DropHealingItem()
	{
        droppedItems[stock-1].SetActive(true);
		droppedItems.RemoveAt(stock-1);
		stock -= 1;
	}

	IEnumerator ThrowHealingItems()
	{
		bool first = true;
        for (int i = 0; i < stock; i++) 
        {
			if (!first) 
			{
				yield return new WaitForSeconds(0.3f);
			}
			first = false;
			thrownItems[i].SetActive(true);
		}
	}

	public void VenchingMachineEasterEgg() 
    {
        if (vendingMachineBroken) 
        {
            return;
        }
    }
}
