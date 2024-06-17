using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtScript : MonoBehaviour
{
    [HideInInspector]
    [SerializeField]
    private AiActions actions;


    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponentInParent<AiActions>();
    }

    // Update is called once per frame
    void Update()
    {
        actions.LookAtPlayer();
    }
}
