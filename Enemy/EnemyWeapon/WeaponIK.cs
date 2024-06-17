using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HumanBone
{
    public HumanBodyBones bone;
    public float weight = 1.0f;
}

public class WeaponIK : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
	private Transform defaultTarget;
	[SerializeField]
    private Transform aimTransform;

    public int iterations = 10;

    public float verticalOffset = 0;

    [Range(0, 1)]
    public float weight = 1.0f;

    public float angleLimit = 90f;
    public float distanceLimit = 1.5f;

    public HumanBone[] humanBones;
    Transform[] boneTransforms;

    private AiSensor sensor;


    // Start is called before the first frame update
    void Start()
    {
        defaultTarget = targetTransform;
		sensor = GetComponentInChildren<AiSensor>();
		Animator animator = GetComponentInChildren<Animator>();
        boneTransforms = new Transform[humanBones.Length];
        for (int i = 0; i < boneTransforms.Length; i++) 
        {
            boneTransforms[i] = animator.GetBoneTransform(humanBones[i].bone);
        }
        //Debug.Log(boneTransforms.Length);
    }

    Vector3 GetTargetPosition() 
    {
        Vector3 targetDirection = targetTransform.position - aimTransform.position;
        Vector3 aimDirection = aimTransform.forward;
        float blendOut = 0.0f;

        float targetAngle = Vector3.Angle(targetDirection, aimDirection);
        if (targetAngle > angleLimit) 
        {
            blendOut += (targetAngle - angleLimit) / 50.0f;
        }

        float targetDistance = targetDirection.magnitude;
        if (targetDistance < distanceLimit) 
        {
            blendOut += distanceLimit - targetDistance;
        }


        Vector3 direction = Vector3.Slerp(targetDirection, aimDirection, blendOut);
        return aimTransform.position + direction;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (aimTransform  == null)
        {
            return;
        }

        if (targetTransform == null) 
        {
            return;
        }

        Vector3 targetPosition = GetTargetPosition();
        for (int i = 0; i < iterations; i++) 
        {
            for (int b = 0; b < boneTransforms.Length; b++) 
            {
                Transform bone = boneTransforms[b];
                float boneWeight = humanBones[b].weight * weight;
				AimAtTarget(bone, targetPosition, boneWeight);
			}
        }
    }

	private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
	{
        if (!sensor.IsInSight(targetTransform.gameObject))
            return;

		Vector3 aimDirection = aimTransform.forward;
        //Vector3 targetDirection = targetPosition - aimTransform.position;
		Vector3 targetDirection = new Vector3(targetPosition.x - aimTransform.position.x, (targetPosition.y - aimTransform.position.y) + verticalOffset, targetPosition.z - aimTransform.position.z);
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
		Quaternion blendedRotation = Quaternion.Slerp(Quaternion.identity, aimTowards, weight);
        bone.rotation = blendedRotation * bone.rotation;
	}

    public void SetTargetTransform(Transform target) 
    {
        targetTransform = target;
    }

    public void SetTargetTransformDefault() 
    {
        targetTransform = defaultTarget;

	}

	public void SetAimTransform(Transform aim)
	{
        aimTransform = aim;
	}

}
