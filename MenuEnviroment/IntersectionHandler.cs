using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class IntersectionHandler : MonoBehaviour
{
	//run on start an invoke
	public LaneHandler[] CarLanesA;
	public LaneHandler[] CarLanesB;
	[Tooltip("Delay before Traffic Light switch in second")]
	public float TrafficLightDelay = 20.0f;
	

	private void Start()
	{
		

		StartCoroutine(SwitchLights());
	}


	[ContextMenu("SwitchLights")]
	IEnumerator SwitchLights()
	{
		yield return new WaitForSeconds(TrafficLightDelay);
		foreach (LaneHandler lane in CarLanesA)
		{
			lane.CanPass = !lane.CanPass;
		}
		foreach (LaneHandler lane in CarLanesB)
		{
			lane.CanPass = !lane.CanPass;
		}

		StartCoroutine(SwitchLights());
	}

}
