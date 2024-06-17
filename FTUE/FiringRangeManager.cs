using InfimaGames.LowPolyShooterPack.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringRangeManager : MonoBehaviour
{

    public List<GameObject> doorOBJs = new List<GameObject>();
    private List<DoorHandler> doors = new List<DoorHandler>();

	public List<GameObject> targetOBJs = new List<GameObject>();
	private List<TargetScript> targets = new List<TargetScript>();

	public DoorHandler rangeEntryDoor;
	public DoorHandler rangeReEntryDoor;


	private int counter = 0;

	// Start is called before the first frame update
	void Start()
    {
        foreach (GameObject door in doorOBJs)
        {
			doors.Add(door.GetComponentInChildren<DoorHandler>());
        }
		foreach (GameObject target in targetOBJs)
		{
			targets.Add(target.GetComponentInChildren<TargetScript>());
		}
	}

    [ContextMenu("ResetDoors")]
    public void resetDoors() 
    {
        for (int i = 0; i < doors.Count; i++) 
        {
			doors[i].CloseDoor();
			doors[i].UnLockDoor();
		}
		doors[0].LockDoor();
    }

	[ContextMenu("ResetTargets")]
	public void resetTargets() 
    {
		counter = 0;
		for (int i = 0; i < targets.Count; i++)
		{
			StartCoroutine(targets[i].DelayTimer());
		}
	}

	public void stopFiringRangeEntry() 
	{
		rangeEntryDoor.CloseDoor();
		rangeEntryDoor.LockDoor();
		rangeReEntryDoor.CloseDoor();
		rangeReEntryDoor.LockDoor();
	}


	public bool CheckTargts() 
	{
		//stop timer
		for (int i = 0; i < targets.Count; i++) 
		{
			if (targets[i].isHit) 
			{
				counter++;
			}
		}

		if (counter == targets.Count) 
		{
			return true;
		}
		return false;
	}

}
