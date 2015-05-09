using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

    public ThirdPersonCamera Camera;
    public GameObject Player;

    private Player _currentPlayer;

    private Transform _start;
    private ThirdPersonCamera _camera;

    void Start () {
        _start = GameObject.FindGameObjectWithTag("Spawn").transform;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPersonCamera>();

        _camera.Offset = _start.transform.position - _camera.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
	    if(_currentPlayer == null)
        {
            _currentPlayer = ((GameObject) Instantiate(Player.gameObject, _start.position, Quaternion.identity)).GetComponent<Player>();
            _camera.LookAt = _currentPlayer.transform;
        }
	}
}
