using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgradeHandler : MonoBehaviour
{



	private void Awake()
	{
		
	}

	// Start is called before the first frame update
	void Start()
    {
		DontDestroyOnLoad(this.gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ApplyUpgrades() 
	{
		try 
		{
			GameObject[] playerObj = GameObject.FindGameObjectsWithTag("Player");

			//access upgrader on player
			//apply certain upgrades

		}
		catch 
		{
			Debug.Log("PLAYER NOT FOUND could not apply upgrades");
		}
	}
}
