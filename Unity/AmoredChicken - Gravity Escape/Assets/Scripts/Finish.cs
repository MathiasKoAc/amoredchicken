using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {

    public string NextScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter()
    {
        Application.LoadLevel(NextScene);
    }
}
