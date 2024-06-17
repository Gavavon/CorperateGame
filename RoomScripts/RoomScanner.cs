using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class RoomScanner : MonoBehaviour
{
	public GameObject box;
	[HideInInspector]
	public BoxCollider roomCollider;
	public Color meshColor = Color.white;
	public LayerMask layers;

	private List<GameObject> enemiesInRoom = new List<GameObject>();
	//[HideInInspector]
	public List<AiActions> enemiesInRoomToActivate = new List<AiActions>();

	Collider[] colliders = new Collider[20];
	int count;

	// Start is called before the first frame update
	void Start()
	{
		roomCollider = box.GetComponent<BoxCollider>();
		Scan();
		for(int i = 0; i < enemiesInRoom.Count; i ++) 
		{
			if (enemiesInRoom[i].GetComponent<AiActions>() != null)
			{
				//enemiesInRoom[i].GetComponent<AiActions>().room = this;
				if (enemiesInRoom[i].GetComponent<AiActions>().GetComponent<AiAgent>().config.enemyBehaviors != AiAgentConfig.AiBehaviors.ProvokableWorkers)
				{
					enemiesInRoomToActivate.Add(enemiesInRoom[i].GetComponent<AiActions>());
					//enemiesInRoom[i].GetComponent<AiActions>().room = this;
				}
			}
		}
	}

	public void AlertRoom() 
	{
		for (int i = 0; i < enemiesInRoomToActivate.Count; i++)
		{
			enemiesInRoomToActivate[i].AlertEnemy();
		}
	}

	private void Scan()
	{
		count = Physics.OverlapBoxNonAlloc(box.transform.position, new Vector3(box.transform.localScale.x, box.transform.localScale.y, box.transform.localScale.z), colliders, Quaternion.Euler(0,0,0),layers, QueryTriggerInteraction.Collide);
		for (int i = 0; i < count; i++)
		{
			if (colliders[i].gameObject.tag == "Enemy")
			{
				enemiesInRoom.Add(colliders[i].gameObject);
			}
			
		}
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = meshColor;
		Gizmos.DrawCube(box.transform.position, new Vector3(box.transform.localScale.x, box.transform.localScale.y, box.transform.localScale.z));
	}
	
}
