using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeCutScreenCharacter : MonoBehaviour
{
	public List<GameObject> PlayerSkin;
	public List<Material> PlayerMat;
	private void Start()
	{
		PlayerSkin[Random.Range(0, PlayerSkin.Count)].SetActive(true);
		this.GetComponentInChildren<Renderer>().material = PlayerMat[Random.Range(0, PlayerMat.Count)];
	}
}
