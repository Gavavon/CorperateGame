using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalDoorHandler : MonoBehaviour
{
    public Transform metalDoor;

    enum MetalDoorState 
    {
        open,
        close
    }
    MetalDoorState state = MetalDoorState.close;

    [ContextMenu("Open")]
    public void OpenMetalDoor() 
    {
        if (state == MetalDoorState.open) 
        {
            return;
        }
		metalDoor.DOLocalMoveY(4.309f, 1f);
		state = MetalDoorState.open;
	}
	[ContextMenu("Close")]
	public void CloseMetalDoor()
	{
		if (state == MetalDoorState.close)
		{
			return;
		}
		metalDoor.DOLocalMoveY(1.463942f, 1f);
        state = MetalDoorState.close;
	}

}
