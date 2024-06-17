using DG.Tweening;
using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorDoorHandler : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioMoveSource;
	[SerializeField]
	private AudioSource audioBashSource;
	[SerializeField]
	private AudioClip doorBash;
	[SerializeField]
	private AudioClip doorOpen;
	[SerializeField]
	private AudioClip doorClose;

	[SerializeField]
	private Transform janitor;


	Quaternion myRotation = Quaternion.identity;

	private bool isDoorOpen = false;

	public void InteractWithDoor()
	{
		if (!isDoorOpen) 
		{
			OpenDoor();
		}
	}

	#region Doors
	[ContextMenu("Open Door")]
	public void OpenDoor()
	{
		audioMoveSource.clip = doorOpen;
		audioMoveSource.Play();
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 27.808f, 0), 1);
		janitor.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, -103.319f, 0), 0.4f);
		isDoorOpen = true;
	}

	[ContextMenu("Close Door")]
	public void CloseDoor()
	{
		audioMoveSource.clip = doorClose;
		janitor.DOLocalRotate(myRotation.eulerAngles = new Vector3(-13.019f, -103.319f, 0), 1);
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 1).OnComplete(() => {
			audioMoveSource.Play();
			isDoorOpen = false;
		});
		
	}
	#endregion
}
