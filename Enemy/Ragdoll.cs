using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    List<Rigidbody> rigidbodies;
	Animator animator;
	[SerializeField]
	private bool excludeRigidbodies = false;
	[ShowIf("excludeRigidbodies", true)]
	[SerializeField]
	private List<Rigidbody> excludeList;

    // Start is called before the first frame update
    void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>().ToList();
		animator = GetComponentInChildren<Animator>();
		if (excludeRigidbodies) 
		{
			for (int i = 0; i < excludeList.Count; i++) 
			{
				rigidbodies.Remove(excludeList[i]);
			}
			
		}
		
		DeactivateRagdoll();
    }

	[ContextMenu("DeactivateRagdoll")]
	public void DeactivateRagdoll()
	{
		foreach (var rigidBody in rigidbodies) 
		{
			rigidBody.isKinematic = true;
		}
		animator.enabled = true;
	}

	[ContextMenu("ActivateRagdoll")]
	public void ActivateRagdoll()
	{
		foreach (var rigidBody in rigidbodies)
		{
			rigidBody.isKinematic = false;
		}
		animator.enabled = false;
	}
}
