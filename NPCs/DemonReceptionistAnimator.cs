using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonReceptionistAnimator : MonoBehaviour
{
	private Animator animator;

	// animation IDs
	private int animIDIsWriting;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponent<Animator>();
		AssignAnimationIDs();
	}

	[ContextMenu("UpdateAnimation")]
	public void UpdateAnimations(bool setWriting)
	{
		animator.SetBool(animIDIsWriting, setWriting);
	}

	private void AssignAnimationIDs()
	{
		animIDIsWriting = Animator.StringToHash("Writing");
	}
}
