using InfimaGames.LowPolyShooterPack.Legacy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeHandler : MonoBehaviour
{
	[SerializeField]
	private GameObject explosion;

	private bool activated = false;

	[Header("Timer")]
	//Time before the grenade explodes
	[Tooltip("Time before the grenade explodes")]
	public float grenadeTimer = 5.0f;

	[Header("Throw Force")]
	[Tooltip("Minimum throw force")]
	public float minimumForce = 1500.0f;

	[Tooltip("Maximum throw force")]
	public float maximumForce = 2500.0f;

	private float throwForce;

	private void Awake()
	{
		//Generate random throw force
		//based on min and max values
		throwForce = Random.Range
			(minimumForce, maximumForce);

		//Random rotation of the grenade
		GetComponent<Rigidbody>().AddRelativeTorque
		(Random.Range(500, 1500), //X Axis
			Random.Range(0, 0), //Y Axis
			Random.Range(0, 0) //Z Axis
			* Time.deltaTime * 5000);
	}

	// Start is called before the first frame update
	void Start()
	{
		//Launch the projectile forward by adding force to it at start
		GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * throwForce);

		//Start the explosion timer
		StartCoroutine(ExplosionTimer());
	}

	private IEnumerator ExplosionTimer()
	{
		if (activated)
		{
			yield break;
		}
		activated = true;

		//Wait set amount of time
		yield return new WaitForSeconds(grenadeTimer);
		explosion.transform.parent = null;
		explosion.SetActive(true);
		Destroy(this.gameObject);
	}
}
