using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    public void ButtonStart()
    {
        Application.LoadLevel("Tutorial");
    }

    public void ButtonClose()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }
}
