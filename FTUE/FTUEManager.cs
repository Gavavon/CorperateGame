using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using TurnTheGameOn.Timer;
using UnityEngine;

public class FTUEManager : MonoBehaviour
{

	public List<GameObject> fence = new List<GameObject>();

	private Timer timer;

	public DoorHandler officeDoor;
	public DoorHandler rangeDoor;

	public FiringRangeManager rangeManager;

	public HudInfoHandler hudInfoHandler;

	[Header("Debug Options")]
	[SerializeField]
	private bool forceFTUEFinish;

	static public bool FTUEDone = false;

	private void Start()
	{
		if (forceFTUEFinish)
		{
			DialogueLua.SetVariable("FiringRangeDone", true);
			QuestLog.SetQuestState("FiringRange", QuestState.Success);
			FiringRangeCompleted();
			//ResetRange();
			rangeManager.gameObject.SetActive(false);
		}

		timer = GetComponent<Timer>();
	}

	public void ResetRange()
	{
		timer.ResetTimer();
		rangeManager.resetDoors();
		rangeManager.resetTargets();
	}

	public void UnlockRangeDoor()
	{
		rangeDoor.UnLockDoor();
	}

	public bool CheckRangeTimerStarted()
	{
		return timer.GetTimerValue() < 120;
	}
	public bool CheckRangeTimerIsZero()
	{
		return timer.GetTimerValue() == 0;
	}

	public bool CheckRangeTimerStopped()
	{
		return timer.timerState == TimerState.Disabled;
	}

	public void FiringRangeCompleted()
	{
		rangeDoor.CloseDoor();
		rangeDoor.LockDoor();
		hudInfoHandler.HideTimer();
		officeDoor.UnLockDoor();
		for (int i = 0; i < fence.Count; i++) 
		{
			fence[i].SetActive(false);
		}
		FTUEDone = true;
	}
	
	#region Register with Lua
	void OnEnable()
	{
		// Make the functions available to Lua: (Replace these lines with your own.)
		Lua.RegisterFunction(nameof(UnlockRangeDoor), this, SymbolExtensions.GetMethodInfo(() => UnlockRangeDoor()));
		Lua.RegisterFunction(nameof(FiringRangeCompleted), this, SymbolExtensions.GetMethodInfo(() => FiringRangeCompleted()));
		Lua.RegisterFunction(nameof(ResetRange), this, SymbolExtensions.GetMethodInfo(() => ResetRange()));
		Lua.RegisterFunction(nameof(CheckRangeTimerStarted), this, SymbolExtensions.GetMethodInfo(() => CheckRangeTimerStarted()));
		Lua.RegisterFunction(nameof(CheckRangeTimerIsZero), this, SymbolExtensions.GetMethodInfo(() => CheckRangeTimerIsZero()));
		Lua.RegisterFunction(nameof(CheckRangeTimerStopped), this, SymbolExtensions.GetMethodInfo(() => CheckRangeTimerStopped()));
	}

	void OnDisable()
	{
		// Remove the functions from Lua: (Replace these lines with your own.)
		Lua.UnregisterFunction(nameof(UnlockRangeDoor));
		Lua.UnregisterFunction(nameof(FiringRangeCompleted));
		Lua.UnregisterFunction(nameof(ResetRange));
		Lua.UnregisterFunction(nameof(CheckRangeTimerStarted));
		Lua.UnregisterFunction(nameof(CheckRangeTimerIsZero));
		Lua.UnregisterFunction(nameof(CheckRangeTimerStopped));
	}
	#endregion

}
