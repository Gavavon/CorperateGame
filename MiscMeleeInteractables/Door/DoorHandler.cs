using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InfimaGames.LowPolyShooterPack;

public class DoorHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject frontTrigger;
	[SerializeField]
	private GameObject backTrigger;

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

	public bool locked = false;

	Quaternion myRotation = Quaternion.identity;

	private bool isDoorOpen = false;

	[HideInInspector]
	public bool canPunchDoor = false;

	private void Start()
	{
		SideUpdater();
	}

	public void InteractWithDoor() 
	{
		if (locked)
		{
			//play a locked noise
			this.GetComponent<DoorInteraction>().DoorIsLocked();
			return;
		}

		if (isDoorOpen) 
		{
			CloseDoor();
			return;
		}
		OpenDoor();
	}

	#region Door Locks
	public void LockDoor() 
	{
		locked = true;
		//this.GetComponent<DoorInteraction>().DoorIsLocked();
	}

	public void UnLockDoor()
	{
		locked = false;
		this.GetComponent<DoorInteraction>().DoorIsUnLocked();
	}
	#endregion

	#region Doors
	[ContextMenu("Open Door")]
	public void OpenDoor() 
	{
		audioMoveSource.clip = doorOpen;
		audioMoveSource.Play();
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 90, 0), 2);
		isDoorOpen = true;
		SideUpdater();
	}

	[ContextMenu("Close Door")]
	public void CloseDoor()
	{
		audioMoveSource.clip = doorClose;
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2).OnComplete(() => {
			audioMoveSource.Play();
		});
		isDoorOpen = false;
		SideUpdater();
	}
	#endregion

	#region Fast Doors
	private void OpenDoorFast()
	{
		if (locked)
		{
			this.GetComponent<DoorInteraction>().DoorIsLocked();
			return;
		}

		audioBashSource.clip = doorBash;
		audioBashSource.Play();
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 90, 0), 0.1f);
		canPunchDoor = false;
		isDoorOpen = true;
		SideUpdater();
	}

	private void CloseDoorFast()
	{
		audioBashSource.clip = doorBash;
		audioBashSource.Play();
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 0.1f).OnComplete(() => {
			audioMoveSource.clip = doorClose;
			audioMoveSource.Play();
		});
		canPunchDoor = false;
		isDoorOpen = false;
		SideUpdater();
	}
	#endregion

	private void SideUpdater() 
	{
		if (isDoorOpen) 
		{
			frontTrigger.SetActive(false);
			backTrigger.SetActive(true);
			return;
		}
		backTrigger.SetActive(false);
		frontTrigger.SetActive(true);
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Melee" && canPunchDoor)
		{
			if (isDoorOpen)
			{
				CloseDoorFast();
				return;
			}
			OpenDoorFast();
		}
	}
}
