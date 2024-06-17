using InfimaGames.LowPolyShooterPack;
using Language.Lua;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodiumHandler : MonoBehaviour
{

    public List<GameObject> wepOptions;
    private GameObject displayWeapon;
    public Transform wepSocket;
	private GameObject wepClone;

	[SerializeField]
	private PodiumInteraction interactor;

	private GameObject player;

	// Start is called before the first frame update
	void Start()
    {
		//interactor = GetComponentInChildren<GunVendingInteraction>();

		//interactor.SetText("$" + gunPrice + " FOR A GUN");

		player = GameObject.FindGameObjectsWithTag("Player")[0];

		if (player == null)
		{
			Debug.Log("No Player Found");
		}

		displayWeapon = wepOptions[UnityEngine.Random.Range(0, wepOptions.Count)];

		interactor.SetText("Take " + displayWeapon.GetComponent<Weapon>().GetWeaponName());

		wepClone = Instantiate(displayWeapon, wepSocket.position, wepSocket.rotation, transform);
		wepClone.layer = 0;
		ChangeLayersRecursively(wepClone.transform, 0);

		
	}

    public void TakeWeapon() 
    {
		player.GetComponent<Character>().AddWeaponToInventory(displayWeapon);
		Destroy(wepClone);
		Destroy(interactor);
	}

	public void ChangeLayersRecursively(Transform trans, int layer)
	{
		foreach (Transform child in trans)
		{
			child.gameObject.layer = layer;
			ChangeLayersRecursively(child, layer);
		}
	}

}
