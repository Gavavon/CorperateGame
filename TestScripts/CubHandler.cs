using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubHandler : MonoBehaviour
{

	public Interactable helklo;

	public void Start()
	{
		helklo.Interact();
	}


	public void Interact() 
    {
		Debug.Log("Try1");
	}
	public void Interact(GameObject n)
	{
        Debug.Log("Try2");
	}
}
