using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

    public string NextScene;
    World _manager;


	// Use this for initialization
	void Start () {
        _manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<World>();
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter()
    {
        PlayerPrefs.SetInt(Application.loadedLevelName + "_time", _manager.CurrentTime);
        Application.LoadLevel(NextScene);
    }
}
