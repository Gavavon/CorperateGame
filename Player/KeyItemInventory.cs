using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyItemInventory : MonoBehaviour
{

	[Title(label: "Key Items")]

	[SerializeField]
	public List<KeyItemObject> keyItems = new List<KeyItemObject>();

	private UIKeyItemsManager HUD;
	private void Start()
	{
		GameObject[] temp = GameObject.FindGameObjectsWithTag("HUD");
		HUD = temp[0].GetComponentInChildren<UIKeyItemsManager>();
	}
}
