using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassDoorHandler : MonoBehaviour
{
	[SerializeField]
	private AudioSource audioMoveSource;
	[SerializeField]
	private AudioClip doorOpen;
	[SerializeField]
	private AudioClip doorClose;

	Quaternion myRotation = Quaternion.identity;

	private bool isDoorOpen = false;

	[HideInInspector]
	public bool canPunchDoor = false;

	[ContextMenu("Interact Door")]
	public void InteractWithDoor()
	{
		if (isDoorOpen)
		{
			CloseDoor();
			return;
		}
		OpenDoor();
	}

	#region Doors
	[ContextMenu("Open Door")]
	private void OpenDoor()
	{
		audioMoveSource.clip = doorOpen;
		audioMoveSource.Play();
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 90, 0), 2);
		isDoorOpen = true;
	}

	[ContextMenu("Close Door")]
	private void CloseDoor()
	{
		audioMoveSource.clip = doorClose;
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2).OnComplete(() => {
			audioMoveSource.Play();
		});
		isDoorOpen = false;
	}
	#endregion
}
