using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ManagerAnimator : MonoBehaviour
{
	private NavMeshAgent agent;
	private Animator animator;

	[SerializeField]
	[Range(0, 35)]
	private int setTask;

	// animation IDs
	private int animIDGVel;
	private int animIDXVel;
	private int animIDYVel;
	private int animIDTaskNum;
	private int animIDDoingTask;
	private int animIDMouthOpen;
	private int animIDScratch;

	// Start is called before the first frame update
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		agent = GetComponent<NavMeshAgent>();
		AssignAnimationIDs();
		animator.SetFloat(animIDTaskNum, setTask);
	}

	void FixedUpdate()
	{
		animator.SetFloat(animIDGVel, agent.velocity.magnitude);
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

	public void ScratchAttack(bool x)
	{
		animator.SetBool(animIDScratch, x);
	}

	public void AnimateMouthOpen()
	{
		animator.SetBool(animIDMouthOpen, true);
	}
	public void AnimateMouthClosed()
	{
		animator.SetBool(animIDMouthOpen, false);
	}

	private void AssignAnimationIDs()
	{
		animIDGVel = Animator.StringToHash("GenVelocity");
		animIDXVel = Animator.StringToHash("XVelocity");
		animIDYVel = Animator.StringToHash("YVelocity");
		animIDTaskNum = Animator.StringToHash("TaskNum");
		animIDDoingTask = Animator.StringToHash("DoingTask");
		animIDMouthOpen = Animator.StringToHash("MouthOpen");
		animIDScratch = Animator.StringToHash("Attack");
	}

    
}
