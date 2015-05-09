using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    void Update()
    {
        var y = Input.GetAxisRaw("Vertical");
        var x = Input.GetAxisRaw("Horizontal");

        var jump = Input.GetButton("Jump");
    }
}

