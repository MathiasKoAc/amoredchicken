using UnityEngine;
using System.Collections;

public class KillerSpoon : MonoBehaviour {

	World _manager;

	// Use this for initialization
	void Start () {
		_manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<World>();
	}
	
	void OnTriggerEnter(Collider c)
	{
		if(c.tag == "Player")
		{
			_manager.RespawnPlayer();
		}
	}
}
