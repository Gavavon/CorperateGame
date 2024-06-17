using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamageAOEDespawner : MonoBehaviour
{
	[SerializeField]
	private bool despawnOverTime = true;
	[SerializeField]
    private float lifeSpan = 30;
    [SerializeField]
    private ParticleSystem particleSystem;
    [SerializeField]
    private Collider collider;

    // Start is called before the first frame update
    void Start()
    {
        if (despawnOverTime) 
        {
			StartCoroutine(StartDespawner());
		}
    }

    private IEnumerator StartDespawner() 
    {
        yield return new WaitForSeconds(lifeSpan);
        particleSystem.Stop();
		collider.enabled = false;
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
	}

    public IEnumerator ForceDespawn() 
    {
		particleSystem.Stop();
		collider.enabled = false;
		yield return new WaitForSeconds(2);
		Destroy(this.gameObject);
	}
}