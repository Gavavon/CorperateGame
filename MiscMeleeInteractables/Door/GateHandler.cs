using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using static UnityEngine.ParticleSystem;
using InfimaGames.LowPolyShooterPack;
using System.Net.Sockets;
using DG.DemiLib;

public class GateHandler : MonoBehaviour
{
	public bool autoClose = false;
	public bool breakRoomEntrance = false;
    public bool isGateLocked = false;
	public bool forceClose = false;
    public ParticleSystem Particle1;
    public ParticleSystem Particle2;

	public GameObject Gate;
	private GateInteraction interacter;
	public LeverHandler lever;

	[SerializeField]
	private GameObject backCollider;

	public enum GateState 
    {
        open,
        closed
    }
	[HideInInspector]
    public GateState currentState = GateState.closed;

	private void Start()
	{
		interacter = GetComponentInChildren<GateInteraction>();
		lever = GetComponentInChildren<LeverHandler>();
	}

	public void UnLockDoor()
	{
		isGateLocked = false;
		interacter.GateIsUnLocked();
	}

	public void OpenGate() 
    {
		currentState = GateState.open;
		ParticlesOnOff(true);
		Gate.transform.DOLocalMoveY(2, 2).OnComplete(() => {
			if (!forceClose) 
			{
				ParticlesOnOff(false);
			}
			lever.working = false;
		});
	}

    public void CloseGate() 
    {
		currentState = GateState.closed;
		ParticlesOnOff(true);
		Gate.transform.DOLocalMoveY(0, 0.5f).OnComplete(() => {
			ParticlesOnOff(false);
			lever.working = false;
			forceClose = false;
		});
	}

	public void ForceCloseGate() 
	{
		forceClose = true;
		isGateLocked = true;
		lever.PullLeverUp();
	}

	public void AllowGatePassage() 
	{
		backCollider.SetActive(false);
		forceClose = false;
		autoClose = false;
		UnLockDoor();
		lever.PullLeverDown();
	}

	public void ParticlesOnOff(bool OnOffBool) 
	{
		switch (OnOffBool) 
		{
			case true:
				Particle1.Play();
				Particle2.Play();
				break;
			case false:
				Particle1.Stop();
				Particle2.Stop();
				break;
		}
	}

}
