using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObjectHandler : MonoBehaviour
{

    [SerializeField]
    private int healingAmount;

	[SerializeField]
	private List<GameObject> meshes;

	private void Start()
	{
		randomizeSnack();
	}

	[ContextMenu("New Bag")]
	private void randomizeSnack() 
	{
		int temp = Random.Range(0, meshes.Count);
		for (int i = 0; i < meshes.Count; i++)
		{
			if (i == temp)
			{
				meshes[i].SetActive(true);
				return;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.tag == "Player") 
        {
            other.GetComponent<PlayerHealthSystem>().HealAmount(healingAmount);
            GameObject.Destroy(gameObject);
        }
	}

}
