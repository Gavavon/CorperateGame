using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TVScreenManager : MonoBehaviour
{

	//public List<Canvas> texts = new List<TextMeshProUGUI> ();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SwapText());
    }

    IEnumerator SwapText() 
    {
        //fade out current text
        //grab random screen from list of screens
        //fade in new text
        yield return null;
    }
}
