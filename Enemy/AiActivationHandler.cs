using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiActivationHandler : MonoBehaviour
{

    private AiActions actions;

	private bool isActive = false;

	private void Start()
	{
		actions = GetComponent<AiActions>();
	}

	public void ActivateEnemy()
    {
		Debug.Log("Here");
		if (isActive) 
		{
			return;
		}
		isActive = true;
		actions.AlertEnemy();
	}
}
