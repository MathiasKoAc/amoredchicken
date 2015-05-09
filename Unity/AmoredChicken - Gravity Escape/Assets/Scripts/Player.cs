using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float Speed;

    public float JumpingPower;

    [HideInInspector]
    public World Envoriment;

    private bool _lastJump = false;

    private float _lastFlip = 0;

    private Rigidbody _rigidbody;

    private bool CanFlip
    {
        get
        {
            return true;
        }
    }

    private bool CanJump
    {
        get
        {
            return true;
        }
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);

        var z = Input.GetAxisRaw("Vertical") * Speed;
        var x = Input.GetAxisRaw("Horizontal") * Speed;
        var flip = Input.GetAxisRaw("Flip");
        

        var jump = Input.GetButton("Jump");

        if (CanFlip && flip != _lastFlip)
        {

            Debug.Log(flip);
            Envoriment.FlipWorld(90 * flip);
        }

        if (jump != _lastJump && jump && CanJump)
        {
            _rigidbody.AddForce(Vector3.up * JumpingPower);
        }

        _lastJump = jump;
        _lastFlip = flip;

        this.transform.position = new Vector3(transform.position.x + x * Time.deltaTime, transform.position.y,transform.position.z + z * Time.deltaTime); 
    }
}

