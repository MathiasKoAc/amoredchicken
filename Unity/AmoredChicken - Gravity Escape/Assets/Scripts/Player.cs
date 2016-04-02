using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float Speed;

    public float JumpingPower;

    [HideInInspector]
    public World Envoriment;

    [HideInInspector]
    public Vector3 Offset = new Vector3(0, 0, 0);

    private bool _lastJump = false;
    private float _lastFlip = 0;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private AudioSource _audio;
    
    private BoxCollider _collider;
    private bool _lastCanJump = true;

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
            return Physics.Raycast(transform.position, -Vector3.up, 0.2f); ;
        }
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<BoxCollider>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        UpdatePosition();
        UpdateRotation();

        //Debug.Log(CanJump);

        //if(_lastCanJump != CanJump)
        //{
        //    _animator.SetBool("Jumping", CanJump);
        //}

        _lastCanJump = CanJump;
    }


    private void UpdatePosition()
    {
        var z = Input.GetAxisRaw("Vertical") * Speed;
        var x = Input.GetAxisRaw("Horizontal") * Speed;
        var flip = Input.GetAxisRaw("Flip");

		//Debug.Log ("z" + z);

        var jump = Input.GetButton("Jump");
		_animator.SetBool("isJumping", jump);
		_animator.SetBool("isRunning", (z*z > 0.9) || (x*x > 0.9));

        if (CanFlip && flip != _lastFlip)
        {
            var x_rot = Mathf.Abs(transform.forward.x);
            var z_rot = Mathf.Abs(transform.forward.z);

            if(x_rot > z_rot)
            {
                Envoriment.FlipWorld(Vector3.right, 90 * flip * -1 * Mathf.Sign(transform.forward.x));
                _audio.Play();
            }
            else
            {
                Envoriment.FlipWorld(Vector3.forward, 90 * flip * -1 * Mathf.Sign(transform.forward.z));
                _audio.Play();
            }
        }

        if (jump != _lastJump && jump && CanJump)
        {
            _rigidbody.AddForce(Vector3.up * JumpingPower);
        }

        _lastJump = jump;
        _lastFlip = flip;

        transform.Translate(Vector3.forward * z * Time.deltaTime);
        transform.Translate(Vector3.right * x * Time.deltaTime);
    }

    private void UpdateRotation()
    {

        var x = Input.GetAxis("Mouse X") * 2.5f * Time.deltaTime;

        var r = transform.rotation;
        r.eulerAngles = new Vector3(0, r.eulerAngles.y + (x * 25), 0);

        transform.rotation = r;

        var y = Input.GetAxis("Mouse Y") * 2.5f * Time.deltaTime;

        if (Offset.y + y < 1.8 && Offset.y + y > -1)
        {
            Offset.y += y;
        }

    }
}

