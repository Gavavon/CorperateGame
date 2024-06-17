using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvocationSensor : MonoBehaviour
{
    [SerializeField]
    private int annoyedThreshold = 4;
	[SerializeField]
	private int annoyedCount = 0;

    private AiActions actions;

    // Start is called before the first frame update
    void Start()
    {
        actions = GetComponentInParent<AiActions>();
    }

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Projectile" || 
            other.tag == "EnemyProjectile" ||
            other.tag == "GlassBreak" ||
			other.tag == "BulletImpact" ||
			other.tag == "Melee") 
        {
            if (annoyedCount < annoyedThreshold)
            {
                annoyedCount++;
            }
            else 
            {
                actions.AlertEnemy();
			}
        }
	}
}
