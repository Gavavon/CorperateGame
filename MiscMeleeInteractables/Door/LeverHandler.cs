using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using InfimaGames.LowPolyShooterPack;
using static GateHandler;
using static UnityEngine.ParticleSystem;

public class LeverHandler : MonoBehaviour
{
	Quaternion myRotation = Quaternion.identity;

	[HideInInspector]
	public bool working = false;

	private GateHandler gate;
	private GateInteraction lever;

	private PlayerHealthSystem player;


	private void Start()
	{
		player = GameObject.FindWithTag("Player").GetComponent<PlayerHealthSystem>();
		gate = GetComponentInParent<GateHandler>();
		lever = GetComponentInParent<GateInteraction>();
	}

	public void InteractWithLever() 
	{
		if (working)
			return;
		working = true;

		if (gate.breakRoomEntrance) 
		{
			if (player.enemyManager.CheckInCombat())
			{
				gate.isGateLocked = true;
			}
			else 
			{
				gate.isGateLocked = false;
			}
		}
		gate.isGateLocked = false;
		if (gate.isGateLocked) 
		{
			switch (gate.currentState)
			{
				case GateState.open:
					TryLeverUp();
					break;
				case GateState.closed:
					TryLeverDown();
					break;
			}
			
			lever.GateIsLocked();
			return;
		}
		lever.GateIsUnLocked();
		switch (gate.currentState)
		{
			case GateState.open:
				PullLeverUp();
				break;
			case GateState.closed:
				PullLeverDown();
				
				break;
		}
	}

	public void TryLeverDown() 
	{
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(-20, 0, 0), 0.2f).OnComplete(() => {
			this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(-30, 0, 0), 0.7f).OnComplete(() => {
				working = false;
			});
		});
	}
	public void TryLeverUp()
	{
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(20, 0, 0), 0.2f).OnComplete(() => {
			this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(30, 0, 0), 0.7f).OnComplete(() => {
				working = false;
			});
		});
	}

	public void PullLeverDown() 
    {
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(30, 0, 0), 2f);
		gate.OpenGate();
	}

	public void PullLeverUp() 
    {
		this.transform.DOLocalRotate(myRotation.eulerAngles = new Vector3(-30, 0, 0), 0.5f);
		gate.CloseGate();
	}
}
