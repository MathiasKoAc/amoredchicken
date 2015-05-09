using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    /// <summary>
    /// Transform to look at
    /// </summary>
    public Transform LookAt;

    /// <summary>
    /// Offset to the Object
    /// </summary>
    public Vector3 Offset;
    public Vector3 LookAtOffset;

    void Start () {
        
    }
	
	void Update () {
        if (LookAt != null)
        {
            float desiredAngle = LookAt.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

            transform.position = LookAt.position - (rotation * Offset);
            transform.LookAt(LookAt.position + LookAtOffset);
        }
    }
}
