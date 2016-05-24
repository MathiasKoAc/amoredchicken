using UnityEngine;
using UnityEngine.SceneManagement;

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

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player")
        {
            int last = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + "_time", -1);
            if (last == -1 || last > _manager.CurrentTime)
            {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + "_time", _manager.CurrentTime);
            }

            SceneManager.LoadScene(NextScene);
        }
        
    }
}
