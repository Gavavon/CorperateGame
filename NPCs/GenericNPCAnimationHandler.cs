using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericNPCAnimationHandler : MonoBehaviour
{
	[SerializeField]
	public int setTask = 1;

	[SerializeField]
	[Range(0, 35)]
	private GameObject[] attachments;
	//Attachments====
	//cellphone		0
	//cup			1

	private Animator animator;

	// animation IDs
	private int animIDTaskNum;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();

		AssignAnimationIDs();

		animator.SetFloat(animIDTaskNum, setTask);

		switch (setTask)
		{
			case 28:
				attachments[1].SetActive(true);
				break;
			case 29:
				attachments[0].SetActive(true);
				break;
			case 30:
				attachments[0].SetActive(true);
				break;
			case 35:
				attachments[0].SetActive(true);
				break;
			default:
				for (int i = 0; i < attachments.Length; i++)
				{
					attachments[i].SetActive(false);
				}
				break;
		}
	}

	[ContextMenu("UpdateAnimation")]
	public void UpdateAnimations() 
	{
		animator.SetFloat(animIDTaskNum, setTask);
	}

	private void AssignAnimationIDs()
	{
		animIDTaskNum = Animator.StringToHash("TaskNum");
	}
}
