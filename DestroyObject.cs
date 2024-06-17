using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{

    public ParticleSystem smoke;

    public float duration = 0.5f;


    public void ActivateObjectRemover() 
    {
        StartCoroutine(RemoveObject());

    }

    IEnumerator RemoveObject() 
    {
		smoke.Play();
        smoke.transform.parent = null;
        yield return new WaitForSeconds(duration);
        Destroy(this.gameObject);
	}

}
