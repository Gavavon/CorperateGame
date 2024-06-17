using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDespawner : MonoBehaviour
{
    [SerializeField]
    private float despawnAfter = 0;
    void Awake()
    {
        StartCoroutine(Despawner());
    }
    private IEnumerator Despawner() 
    {
        yield return new WaitForSeconds(despawnAfter);
        Destroy(gameObject);
    }
}
