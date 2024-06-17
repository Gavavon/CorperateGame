using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGTOOLPlayerStats : MonoBehaviour
{
    public bool infiniteAmmo = false;
    public bool infiniteHealth = false;

	private static DEBUGTOOLPlayerStats Instance;

	private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
	}
}
