using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using System.Threading;
using System;
using Michsky.LSS;
using UnityEditor;

public class ElevatorHandler : MonoBehaviour
{
	#region Declerations
	private enum DoorState
	{
		Open,
		Close
	}

	private DoorState state = DoorState.Close;
	

	[Header("Elevator Adjustable Vars")]

	[Tooltip("The time it takes for doors to full open in seconds.")]
	[SerializeField]
	private float elevatorDoorTime = 3f;

	[Tooltip("The time it takes for elevator to reach destination in seconds.")]
	[SerializeField]
	private float elevatorMoveTime = 3f;

	[Tooltip("The time elevator stays open without player in seconds.")]
	[SerializeField]
	private float elevatorOpenDuration = 3f;


	[Header("Elevator Lift Vars")]
	private bool canInteractElevator = true;
	[HideInInspector]
	public bool isPlayerInside = false;

	[Header("Change Scenes")]
	[SerializeField]
	public bool canChangeScenes = false;
	[ShowIf("canChangeScenes", true)]
	[SerializeField]
	public LSS_Manager lssManager;
	[ShowIf("canChangeScenes", true)]
	[SerializeField]
	private string nextLevel = "null";

	[Header("Elevator Door References")]

	[Tooltip("The right outer elevator door.")]
	[SerializeField]
	private Transform elevatorDoorOutRight;

	[Tooltip("The left outer elevator door.")]
	[SerializeField]
	private Transform elevatorDoorOutLeft;

	[Tooltip("The right inner elevator door.")]
	[SerializeField]
	private Transform elevatorDoorInRight;

	[Tooltip("The left inner elevator door.")]
	[SerializeField]
	private Transform elevatorDoorInLeft;


	[Header("Elevator Audio References")]

	[Tooltip("The elevator's Audio Source component.")]
	[SerializeField, NotNull]
	private AudioSource bellAudioSource;

	[Tooltip("The elevator's Audio Source component.")]
	[SerializeField, NotNull]
	private AudioSource moveAudioSource;

	[Tooltip("The elevator's Audio Source component.")]
	[SerializeField, NotNull]
	private AudioSource doorAudioSource;
	#endregion

	#region Unity

	void Start()
	{
		//StartCoroutine(LiftElevator());
		//Make sure we have an Audio Source assigned.
		if (moveAudioSource != null)
		{
			moveAudioSource.loop = true;
		}

	}
	#endregion
	#region Public Methods
	[ContextMenu("Elevator")]
	public void InteractWithElevator() 
	{
		switch (state) 
		{
			case DoorState.Open:
				StartCoroutine(InteractElevatorClose());
				break;
			case DoorState.Close:
				StartCoroutine(InteractElevatorOpen());
				break;
		}
	}

	#endregion
	#region Private Methods
	private IEnumerator InteractElevatorOpen()
	{
		if (!canInteractElevator)
		{
			yield break;
		}
		state = DoorState.Open;
		canInteractElevator = false;
		MoveDoorsOpen();
		yield return new WaitForSeconds(elevatorOpenDuration);
		canInteractElevator = true;
	}
	private IEnumerator InteractElevatorClose()
	{
		if (!canInteractElevator)
		{
			yield break;
		}
		state = DoorState.Close;
		canInteractElevator = false;
		MoveDoorClose();
		GameObject.FindGameObjectWithTag("TransferHandler").GetComponent<InfoTransferManager>().SetPlayerStats();
		yield return new WaitForSeconds(elevatorOpenDuration);
		if (isPlayerInside && canChangeScenes) 
		{
			lssManager.LoadScene(nextLevel);
		}
		canInteractElevator = true;
	}
	/*
	public async Task CloseWhenAvailable()
	{
		await Task.Yield();
		await Task.Delay(1000);
		while (!canInteractElevator && !isPlayerInside)
		{
			await Task.Delay(1000);
		}
		_ = Task.Run(() => InteractElevatorClose(0));
	}

	private async void InteractElevatorClose(int i) 
	{
		state = DoorState.Close;
		canInteractElevator = false;
		//doorAudioSource.Play();
		elevatorDoorOutRight.DOLocalMoveX(-1.246985f, elevatorDoorTime, false);
		elevatorDoorOutLeft.DOLocalMoveX(-1.246985f, elevatorDoorTime, false);
		elevatorDoorInRight.DOLocalMoveX(-1.249996f, elevatorDoorTime, false);
		elevatorDoorInLeft.DOLocalMoveX(-1.249996f, elevatorDoorTime, false);
		await Task.Delay((int)elevatorDoorTime * 100);
		canInteractElevator = true;
	}
	*/ //this for having the elevator close behind the player (doesn't work because audio cant be called)
	private void MoveDoorsOpen()
	{
		doorAudioSource.Play();
		elevatorDoorOutRight.DOLocalMoveX(-1.95f, elevatorDoorTime, false);
		elevatorDoorOutLeft.DOLocalMoveX(-0.51f, elevatorDoorTime, false);

		elevatorDoorInRight.DOLocalMoveX(-1.954f, elevatorDoorTime, false);
		elevatorDoorInLeft.DOLocalMoveX(-0.514f, elevatorDoorTime, false);
	}

	private void MoveDoorClose()
	{
		doorAudioSource.Play();
		elevatorDoorOutRight.DOLocalMoveX(-1.246985f, elevatorDoorTime, false);
		elevatorDoorOutLeft.DOLocalMoveX(-1.246985f, elevatorDoorTime, false);

		elevatorDoorInRight.DOLocalMoveX(-1.249996f, elevatorDoorTime, false);
		elevatorDoorInLeft.DOLocalMoveX(-1.249996f, elevatorDoorTime, false);
	}
	
	#endregion
}
