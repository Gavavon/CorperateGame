using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBoxHandler : MonoBehaviour
{
    private bool resumeWasSubmitted = false;

    //References
    public Transform paper;
    public Transform boxLid;
	public ParticleSystem fire;

    //internal bools
	private bool canInteract = true;
	private bool isOpen = false;
	private Quaternion myRotation = Quaternion.identity;

    public bool GetResumeStatus() 
    {
        return resumeWasSubmitted;
	}

    public void OpenMailBox() 
    {
        fire.Play();
		boxLid.DOLocalRotate(myRotation.eulerAngles = new Vector3(-57.238f, 180, 0), 0.5f);
        isOpen = true;
	}

    public void CloseMailBox() 
    {
		fire.Stop();
		boxLid.DOLocalRotate(myRotation.eulerAngles = new Vector3(38.774f, 180, 0), 0.5f);
		isOpen = false;
	}

    public IEnumerator InteractWithMailBox() 
    {
        if (!canInteract) 
        {
            yield break;
        }
        if (!isOpen) 
        {
            OpenMailBox();
            yield return new WaitForSeconds(1);
        }
        paper.gameObject.SetActive(true);
        paper.DOLocalMove(new Vector3(0, 0.9579f, -0.0185f), 1f);
		yield return new WaitForSeconds(1);
		CloseMailBox();
		canInteract = false;
        resumeWasSubmitted = true;
	}
	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Player" && canInteract) 
        {
            OpenMailBox();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player" && canInteract)
		{
            CloseMailBox();
		}
	}
}
