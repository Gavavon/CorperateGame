using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DoorBreakHandler : MonoBehaviour
{
    public List<Rigidbody> activatableObjects;
    public List<GameObject> destroyableObjects;
    public GameObject doorPieces;
	public float breakForce;
	private Transform player;

	[SerializeField]
	private Transform alertObject;


	private void Start()
	{
		player = GameObject.FindGameObjectsWithTag("Player")[0].transform;
	}

	public void BreakDoor(Transform directionObject) 
    {
		Instantiate(alertObject, transform.position, transform.rotation);

		this.GetComponent<BoxCollider>().enabled = false;

		for (int i = 0; i < destroyableObjects.Count; i++)
		{
			destroyableObjects[i].SetActive(false);
		}
		for (int i = 0; i < activatableObjects.Count; i++) 
        {
			activatableObjects[i].isKinematic = false;
			Vector3 dPosition = activatableObjects[i].transform.position;
			Vector3 pPosition = directionObject.position;

			Vector3 dest = new Vector3(
				Mathf.Cos(Random.Range(-0.5f, 0.5f)+Mathf.Atan((dPosition.z - pPosition.z) / (dPosition.x - pPosition.x))) * breakForce,
				0,
				Mathf.Sin(Random.Range(-0.5f, 0.5f)+Mathf.Atan((dPosition.z - pPosition.z) / (dPosition.x - pPosition.x))) * breakForce
				);

			Vector3 fixedDest;

			if (pPosition.x > dPosition.x)
			{
				fixedDest = new Vector3(dPosition.x - dest.x, dPosition.y - dest.y, dPosition.z - dest.z);
			}
			else
			{
				fixedDest = new Vector3(dPosition.x + dest.x, dPosition.y + dest.y, dPosition.z + dest.z);
			}

			activatableObjects[i].AddForce(fixedDest);
			activatableObjects[i].GetComponent<DoorCleanUp>().ActivateCleanUp();
		}
        GameObject pieces = Instantiate(doorPieces, transform.position, transform.rotation);
		foreach (Rigidbody rb in pieces.GetComponentsInChildren<Rigidbody>()) 
		{
			//Vector3 force = (rb.transform.position - transform.position).normalized * breakForce;

			//float amountToMove = 5;
			Vector3 dPosition = transform.position;
			Vector3 pPosition = directionObject.position;

			Vector3 dest = new Vector3(
				Mathf.Cos(Random.Range(-0.5f, 0.5f) +Mathf.Atan((dPosition.z - pPosition.z) / (dPosition.x - pPosition.x))) * breakForce,
				0,
				Mathf.Sin(Random.Range(-0.5f, 0.5f) +Mathf.Atan((dPosition.z - pPosition.z) / (dPosition.x - pPosition.x))) * breakForce
				);

			Vector3 fixedDest;

			if (pPosition.x > dPosition.x)
			{
				fixedDest = new Vector3(dPosition.x - dest.x, dPosition.y - dest.y, dPosition.z - dest.z);
			}
			else
			{
				fixedDest = new Vector3(dPosition.x + dest.x, dPosition.y + dest.y, dPosition.z + dest.z);
			}

			rb.AddForce(fixedDest);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.tag == "Melee")
		{
            BreakDoor(player.gameObject.transform);
		}
		if (collider.gameObject.tag == "Enemy")
		{
			BreakDoor(collider.gameObject.transform);
		}
	}
}
