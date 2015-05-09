using UnityEngine;
using System.Collections;

public class ThirdPersonCamera : MonoBehaviour {

    /// <summary>
    /// Transform to look at
    /// </summary>
    public Player LookAt;

    public static float MaxTop = 10f;
    public static float MaxBot = 10f;

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
            float desiredAngle = LookAt.transform.eulerAngles.y;
            Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);

            transform.position = LookAt.transform.position - (rotation * Offset);
            transform.LookAt(LookAt.transform.position + LookAtOffset + LookAt.Offset);
        }
    }
}
