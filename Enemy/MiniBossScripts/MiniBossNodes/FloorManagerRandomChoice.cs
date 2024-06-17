using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

[System.Serializable]
public class FloorManagerRandomChoice : ActionNode
{
    public int percentChance = 50;
    protected override void OnStart() 
    {
        
    }

    protected override void OnStop() 
    {
        
    }

    protected override State OnUpdate() 
    {
        if (Random.Range(1, 100) < percentChance)
        {
            return State.Success;
        }
        else 
        {
			return State.Failure;
		}
        
    }
}
