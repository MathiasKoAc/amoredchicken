﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class World : MonoBehaviour {

    public ThirdPersonCamera Camera;
    public GameObject Player;

    public int CurrentTime;
    private int _lastTime;

    private Player _currentPlayer;

    private Text _text;

    private Transform _start;
    private ThirdPersonCamera _camera;
    private GameObject _rotation;
    private bool _running;

    void Start () {
        _start = GameObject.FindGameObjectWithTag("Spawn").transform;
        _camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<ThirdPersonCamera>();

        _camera.Offset = _start.transform.position - _camera.transform.position;
        _rotation = GameObject.FindGameObjectWithTag("Rotation");

        Cursor.lockState = CursorLockMode.Confined;
        _text = GameObject.FindGameObjectWithTag("Counter").GetComponent<Text>();

        _lastTime = PlayerPrefs.GetInt(Application.loadedLevelName + "_time", -1);

        _running = true;
        StartCoroutine(TimerFunction());
    }
	
    private IEnumerator TimerFunction()
    {
        while(_running)
        {
            CurrentTime++;
            _text.text = CurrentTime.ToString() + ((_lastTime > 0) ? " - " + _lastTime.ToString() : "");
            yield return new WaitForSeconds(1);
        }
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
        _rotation.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        _currentPlayer.Envoriment = this;
    }
}
