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
    private float _distToGround;
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
        _distToGround = _collider.bounds.extents.y;
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
                Envoriment.FlipWorld(Vector3.right, 90 * flip * -1 );
                _audio.Play();
            }
            else
            {
                Envoriment.FlipWorld(Vector3.forward, 90 * flip * -1 );
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
        var mousePos = Input.mousePosition;

        mousePos.x -= Screen.width / 2;
        mousePos.y -= (Screen.height / 2);

        float y = mousePos.y * 0.008f * Time.deltaTime;

        if (Offset.y + y < 1.8 && Offset.y + y > -1)
        {
            Offset.y += y;
        }

        transform.Rotate(mousePos.y * Time.deltaTime, mousePos.x * Time.deltaTime, 0);
        transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        
    }
}

