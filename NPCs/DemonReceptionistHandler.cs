using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonReceptionistHandler : MonoBehaviour
{
	Transform player;
	public Transform tempObject;

	private Quaternion myRotation = Quaternion.identity;

	private void Start()
	{
		player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
		StartCoroutine(DoneInteractingWithDemon());
	}

	public IEnumerator InteractWithDemon()
    {
		GetComponent<DemonReceptionistAnimator>().UpdateAnimations(false);
		Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position, Vector3.up);
		transform.DORotate(targetRotation.eulerAngles, 0.5f);
		yield return new WaitForSeconds(4);
		StartCoroutine(DoneInteractingWithDemon());
	}

	public IEnumerator DoneInteractingWithDemon()
	{
		transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 0.5f);
		yield return new WaitForSeconds(2);
		GetComponent<DemonReceptionistAnimator>().UpdateAnimations(true);
	}
}