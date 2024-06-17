using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BigSniperAnimator : MonoBehaviour
{
	private NavMeshAgent agent;
	private Animator animator;
	private AkimboActions actions;

	[SerializeField]
	[Range(0, 35)]
	private int setTask;

	// animation IDs
	private int animIDXVel;
	private int animIDYVel;
	private int animIDTaskNum;
	private int animIDDoingTask;
	private int animIDDropGuns;
	private int animIDisAttacking;
	private int animIDCharging;

	// Start is called before the first frame update
	void Start()
	{
		actions = GetComponent<AkimboActions>();
		animator = GetComponentInChildren<Animator>();
		AssignAnimationIDs();
		animator.SetFloat(animIDTaskNum, setTask);
	}

	[ContextMenu("DebugTaskUpdater")]
	public void DebugTaskUpdater() 
	{
		animator.SetFloat(animIDTaskNum, setTask);
	}

	public void DoneWithTask()
	{
		animator.SetBool(animIDDoingTask, false);
	}

	private void AssignAnimationIDs()
	{
		animIDTaskNum = Animator.StringToHash("TaskNum");
		animIDDoingTask = Animator.StringToHash("DoingTask");
	}
}
