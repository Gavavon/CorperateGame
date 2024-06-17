using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBossWeaponLookAt : MonoBehaviour
{
    public LogDepBossActions actions;
    private bool look = false;
	private Quaternion myRotation = Quaternion.identity;
	private Sequence mySequence;

	private void Start()
	{
		this.enabled = false;
	}

	void Update()
	{
        if (look) 
        {
			transform.LookAt(actions.playerTarget.transform);
		}
    }

    public void StartLooking()
    {
		mySequence.Kill();
		look = true;
	}

    public void StopLooking()
    {
        look = false;
		mySequence.Append(transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(0, 0, 0), 2f));
	}
}
