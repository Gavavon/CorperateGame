using System.Collections;
using System.Collections.Generic;
using TurnTheGameOn.Timer;
using UnityEngine;

public class StartRange : MonoBehaviour
{

	public FTUEManager manager;
	private Timer questTimer;

	public DoorHandler finishLineDoor;

	public enum RangeColliderType 
	{
		start,
		end
	}
	public RangeColliderType type = RangeColliderType.start;

	private void Start()
	{
		questTimer = manager.GetComponent<Timer>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Player") 
		{
			switch (type) 
			{ 
				case RangeColliderType.start:
					if (questTimer.GetTimerValue() == 120) 
					{
						questTimer.timerState = TimerState.Counting;
					}
					break;
				case RangeColliderType.end:
					questTimer.timerState = TimerState.Disabled;
					finishLineDoor.UnLockDoor();
					manager.rangeManager.stopFiringRangeEntry();
					break;
			}
			
		}
	}
}
