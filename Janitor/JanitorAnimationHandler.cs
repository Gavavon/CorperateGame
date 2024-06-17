using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanitorAnimationHandler : MonoBehaviour
{
	[SerializeField]
	private int setTask = 1;

	private Animator animator;

	// animation IDs
	private int animIDTaskNum;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();

		AssignAnimationIDs();

		
	}

	private void Update()
	{
		animator.SetFloat(animIDTaskNum, setTask);
	}

	private void AssignAnimationIDs()
	{
		animIDTaskNum = Animator.StringToHash("TaskNum");
	}
}
