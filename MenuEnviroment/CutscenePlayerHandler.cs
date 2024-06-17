using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CutscenePlayerHandler : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent agent;

	[SerializeField]
	private Transform[] Dests;
	private int nextDest = 0;

	private int animationSpeed;
	private int animationMove;

	public static CutscenePlayerHandler instance;

	private void Awake()
	{
		instance = this;
	}

	// Start is called before the first frame update
	void Start()
    {
		animationSpeed = Animator.StringToHash("Speed");
		animationMove = Animator.StringToHash("Move");

		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();

		agent.isStopped = true;

		agent.destination = Dests[nextDest].transform.position;
		nextDest++;
	}

	void Update()
	{
		animator.SetFloat(animationSpeed, agent.velocity.magnitude);
		if (agent.remainingDistance <= 0.5f)
		{
			agent.destination = Dests[nextDest].transform.position;
			nextDest++;
		}
	}

	[ContextMenu("StartCutscene")]
	public void testPlayerCutsceneMover() 
	{
		StartCoroutine(MovePlayerToDoors());
	}

	private IEnumerator MovePlayerToDoors() 
    {
		animator.SetBool(animationMove, true);
		yield return new WaitForSeconds(0.5f);
		agent.isStopped = false;
		yield return new WaitForSeconds(4f);
		StartCoroutine(OfficeDoorOpener.instance.DoorCutScene());

		//move agent between list of destinations
		//open doors at certain point

	}
}
