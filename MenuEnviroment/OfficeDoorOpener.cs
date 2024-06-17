using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeDoorOpener : MonoBehaviour
{
	[SerializeField]
    private GameObject door1;
	[SerializeField]
	private GameObject door2;

	Quaternion myRotation = Quaternion.identity;

	public static OfficeDoorOpener instance;

	private void Awake()
	{
		instance = this;
	}

	private void OpenDoors()
	{
		door1.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, -90, -0), 2);
		door2.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(-0, 90, 0), 2);
	}

	private void CloseDoors()
	{
		door1.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2);
		door2.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2);
	}

	public IEnumerator DoorCutScene() 
	{
		OpenDoors();
		yield return new WaitForSeconds(5f);
		CloseDoors();
	}
}
