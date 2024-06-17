using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIKeyItemsManager : MonoBehaviour
{

    public List<Image> imageSpots = new List<Image>();

	[HideInInspector]
	public List<Sprite> keyItemImages;

    public RectTransform line;

	[HideInInspector]
	public int numKeyItems = 0;

    public void updateUI() 
    {
        line.sizeDelta = new Vector2((100 * numKeyItems) + 20, 4);
        for (int i = 0; i < numKeyItems; i++) 
        {
			imageSpots[i].enabled = true;
			imageSpots[i].sprite = keyItemImages[i];
		}
        
    }
}
