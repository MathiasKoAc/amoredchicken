using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public GameObject Explosion;

    public float Speed;
    public float SideSpeed;

    public float JumpingPower;

    public float CameraMaxUp = 3f;
    public float CameraMaxDown = 1f;

    public float MouseSpeedVertical = 2.5f;
    public float MouseSpeedHorizontal = 3.5f;

    [HideInInspector]
    public World Envoriment;

    [HideInInspector]
    public Vector3 Offset = new Vector3(0, 0, 0);


    private bool _lastJump = false;
    private float _lastFlip = 0;
    private Rigidbody _rigidbody;
    private Animator _animator;
    private AudioSource _audio;
    private GameObject _explosion;
    

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
        AudioListener.pause = (PlayerPrefs.GetInt("Mute", 0) == 1 ? true : false);
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
    }

    void Update()
    {
        goToMenu();
        UpdatePosition();
        UpdateRotation();
    }

    public void Explode()
    {
        Destroy(this.gameObject, 0.1f);
    }

    private void goToMenu()
    {
        if(CrossPlatformInputManager.GetButton("Menu"))
        {
            CrossPlatformInputManager.SetButtonUp("Menu");
            SceneManager.LoadScene("Menu");
        }

        if (CrossPlatformInputManager.GetButton("Retry"))
        {
            CrossPlatformInputManager.SetButtonUp("Retry");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (CrossPlatformInputManager.GetButton("Mute"))
        {
            CrossPlatformInputManager.SetButtonUp("Mute");
            AudioListener.pause = !AudioListener.pause;
            PlayerPrefs.SetInt("Mute", AudioListener.pause ? 1 : 0);
        }
    }

    private void UpdatePosition()
    {
        float x = 0f;
        float z = 0f;

        if (Application.isMobilePlatform)
        {
            z = CrossPlatformInputManager.GetAxis("Vertical") * Speed;
            x = CrossPlatformInputManager.GetAxis("Horizontal") * SideSpeed;
        } else
        {
            z = Input.GetAxisRaw("Vertical") * Speed;
            x = Input.GetAxisRaw("Horizontal") * SideSpeed;
        }
        x = x*x < 0.1 ? CrossPlatformInputManager.GetAxisRaw("JoyMoveH") * SideSpeed : x;
        z = z*z < 0.1 ? CrossPlatformInputManager.GetAxisRaw("JoyMoveV") * Speed : z;

        float flip = 0;// = Input.GetAxisRaw("Flip");

        var flippR = CrossPlatformInputManager.GetButton("FlippR");
        var flippL = CrossPlatformInputManager.GetButton("FlippL");

        if(flippR)
        {
            flip = 1;
        } else if(flippL)
        {
            flip = -1;

        }

        var jump = CrossPlatformInputManager.GetButton("Jump"); //Input.GetButton("Jump");
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

        var x = CrossPlatformInputManager.GetAxisRaw("Mouse X");
        var y = CrossPlatformInputManager.GetAxisRaw("Mouse Y");

        x = x * x < 0.01 ? CrossPlatformInputManager.GetAxisRaw("JoyLookH") : x;
        y = y * y < 0.01 ? CrossPlatformInputManager.GetAxisRaw("JoyLookV") : y;

        x *= this.MouseSpeedHorizontal * Time.deltaTime;
        y *= this.MouseSpeedVertical * Time.deltaTime;

        var r = transform.rotation;
        r.eulerAngles = new Vector3(0, r.eulerAngles.y + (x * 25), 0);

        transform.rotation = r;

        if (Offset.y + y < this.CameraMaxUp && Offset.y + y > this.CameraMaxDown)
        {
            Offset.y += y;
        }

    }
}

