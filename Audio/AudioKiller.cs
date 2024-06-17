using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioKiller : MonoBehaviour
{

	[SerializeField]
	private bool selfKill = false;
	[SerializeField]
	private float killTime = 5f;

    public static AudioKiller Instance;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		if(selfKill)
			KillAudio(killTime);
	}

	public void KillAudio() 
	{
		StartCoroutine(AudioKillerWaiter(0f));
	}

	public void KillAudio(float time)
	{
		StartCoroutine(AudioKillerWaiter(time));
	}

	private IEnumerator AudioKillerWaiter(float time) 
	{
		yield return new WaitForSeconds(time);
		Destroy(gameObject);
	}
}
