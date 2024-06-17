using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatePlayerCollider : MonoBehaviour
{

	private GateHandler gate;

	private void Start()
	{
		gate = this.gameObject.GetComponentInParent<GateHandler>();
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Player" && gate.autoClose && gate.currentState != GateHandler.GateState.closed)
		{
			gate.ForceCloseGate();
		}
	}
}
