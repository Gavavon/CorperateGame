using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private int impactDamage = 5;
    [SerializeField]
    private ParticleSystem fireParticles;

    private bool activated = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Despawner());
    }

    public IEnumerator ActivateExplosion() 
    {
        if (activated) 
        {
            yield break;
        }
        activated = true;
		explosion.transform.parent = null;
		explosion.SetActive(true);
        fireParticles.Stop();
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Player") 
        {
            collision.gameObject.GetComponent<PlayerHealthSystem>().TakeDamageAmount(impactDamage);
        }
		if (!collision.transform.root.CompareTag("MiniBoss"))
		{
			StartCoroutine(ActivateExplosion());
		}
	}

    private IEnumerator Despawner() 
    {
        yield return new WaitForSeconds(40f);
        Destroy(this.gameObject);
    }
}
