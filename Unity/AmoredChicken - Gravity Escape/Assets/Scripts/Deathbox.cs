using UnityEngine;
using System.Collections;

public class Deathbox : MonoBehaviour {

    World _manager;

    void Start()
    {
        _manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<World>();
    }

    void OnTriggerExit(Collider c)
    {
        if(c.tag == "Player")
        {
            _manager.RespawnPlayer();
        }
    }

}
