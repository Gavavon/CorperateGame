using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianController : MonoBehaviour
{
	[SerializeField]
	private int setTask = 1;

	private Animator animator;

	// animation IDs
	private int animIDTaskNum;

	//Task Number IDs
	/*
	 * Sitting=======
	 * Meeting1		1
	 * Meeting2		2
	 * SitTalk1		3
	 * SitTalk2		4
	 * SitIdle1		5
	 * SitIdle2		6
	 * Typing		7
	 * Standing======
	 * Argue		8
	 * PhoneTalk	9
	 * PhoneTalk2	10
	 * Idle			11
	 * Talk1		12
	 */


	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();

		AssignAnimationIDs();

		animator.SetFloat(animIDTaskNum, setTask);
	}

	private void AssignAnimationIDs()
	{
		animIDTaskNum = Animator.StringToHash("TaskNum");
		//animIDDied = Animator.StringToHash("Died");
	}
}
