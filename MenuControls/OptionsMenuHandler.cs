using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsMenuHandler : MonoBehaviour
{

	public GameObject[] Tabs;


	public void ActivateGameplayTab()
	{
		SwapTab(0);

	}
	public void ActivateAudioTab()
	{
		SwapTab(1);
	}
	public void ActivateGraphicsTab()
	{
		SwapTab(2);
	}
	public void ActivateControlsTab()
	{
		SwapTab(3);
	}

	private void SwapTab(int x) 
	{
		for (int i = 0; i < Tabs.Length; i++)
		{
			if (i == x)
				Tabs[i].SetActive(true);
			else
				Tabs[i].SetActive(false);
		}
	}
}
