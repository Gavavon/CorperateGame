using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class DummyEnemyHandler : MonoBehaviour
{
	[SerializeField]
	[Range(0, 35)]
	private int setTask = 1;

	[SerializeField]
	private int minEarned = 10;
	[SerializeField]
	private int maxEarned = 15;

	[SerializeField]
	private GameObject[] attachments;
	//Attachments====
	//cellphone		0
	//cup			1

	private Animator animator;
	
	// animation IDs
	private int animIDTaskNum;

	private bool dead = false;

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
	[ContextMenu("DebugTaskUpdater")]
	public void DebugTaskUpdater()
	{
		animator.SetFloat(animIDTaskNum, setTask);
	}

	private void AssignAnimationIDs()
	{
		animIDTaskNum = Animator.StringToHash("TaskNum");
	}

	public void Die() 
	{
		if (dead) 
		{
			return;
		}
		dead = true;
		EnemyManager enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
		enemyManager.RemoveSelfFromList(gameObject);
		GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerIncomeHandler>().GiveMoney(Random.Range(minEarned, maxEarned));

		this.GetComponent<Ragdoll>().ActivateRagdoll();
		StartCoroutine(RemoveBody());
	}

	private IEnumerator RemoveBody()
	{
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}

}
