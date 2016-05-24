using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    void Start()
    {
        GetComponent<AudioSource>().Play();
    }

    public void ButtonStart()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ButtonClose()
    {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                 Application.Quit();
        #endif
    }

    public void ButtonMute()
    {
        AudioListener aListener = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioListener>();
        aListener.enabled = !aListener.enabled;
        if (aListener.enabled)
        {
            GetComponent<AudioSource>().Play();
        }
        else {
            GetComponent<AudioSource>().Stop();
        }
    }

    public void ButtonMenu()
	{
        SceneManager.LoadScene("Menu");
    }

    public void ButtonCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void ButtonSelectLevel()
    {
        SceneManager.LoadScene("SelectLevel");
    }

    public void ButtonLevel1()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void ButtonLevel2()
    {
        SceneManager.LoadScene("Level02");
    }

    public void ButtonLevel3()
    {
        SceneManager.LoadScene("Level03");
    }

    public void ButtonLevel4()
    {
        SceneManager.LoadScene("Level04");
    }

    public void ButtonLevel5()
    {
        SceneManager.LoadScene("Level05");
    }
}
