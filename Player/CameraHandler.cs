using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    public GameObject cameraSocket;
    public GameObject camera;

    [ContextMenu("Reset Camera")]
    public void resetCamera()
    {
        if (camera.transform.parent == null) 
        {
            camera.transform.parent = cameraSocket.transform;
        }
        camera.transform.localPosition = Vector3.zero;
		camera.transform.localEulerAngles = Vector3.zero;
	}
}
