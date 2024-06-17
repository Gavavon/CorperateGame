using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KeyItem", order = 1)]
public class KeyItemObject : ScriptableObject
{
	public Sprite uiImage;
	public bool isEquipped = false;

	public int keyItemID = 0;
}
