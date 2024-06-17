using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCleanUp : MonoBehaviour
{
    //public bool isParent = false;
    public bool isClone = false;
    public float durationSeconds = 5f;
    // Start is called before the first frame update
    void Start()
    {
        if (isClone) 
        {
			ActivateCleanUp();
		}
	}

	public void ActivateCleanUp()
	{
		StartCoroutine(WaitDestroy());
	}

	IEnumerator WaitDestroy() 
    {
        yield return new WaitForSeconds(durationSeconds);
        Destroy(this.gameObject);
    }
}
