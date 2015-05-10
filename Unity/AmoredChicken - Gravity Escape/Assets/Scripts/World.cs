using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public ThirdPersonCamera Camera;
    public GameObject Player;
    

    private Player _currentPlayer;

    private Transform _start;
    private ThirdPersonCamera _camera;
    private GameObject _rotation;

    void Start () {
        _start = GameObject.FindGameObjectWithTag("Spawn").transform;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPersonCamera>();

        _camera.Offset = _start.transform.position - _camera.transform.position;
        _rotation = GameObject.FindGameObjectWithTag("Rotation");

        Cursor.lockState = CursorLockMode.Confined;
    }
	
	// Update is called once per frame
	void Update () {
	    if(_currentPlayer == null)
        {
            Spawn();
        }
	}

    public void FlipWorld(Vector3 axis, float direction)
    {
        Debug.Log(axis);
        _rotation.transform.RotateAround(_currentPlayer.transform.position, axis, direction);
    }

    public void RespawnPlayer()
    {
        Destroy(_currentPlayer.gameObject);
        Spawn();
    }

    private void Spawn()
    {
        _currentPlayer = _currentPlayer = ((GameObject)Instantiate(Player.gameObject, _start.position, Quaternion.identity)).GetComponent<Player>();
        _camera.LookAt = _currentPlayer;
        _currentPlayer.Envoriment = this;
    }
}
