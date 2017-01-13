using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    void Start()
    {
        AudioListener.pause = (PlayerPrefs.GetInt("Mute", 0) == 1 ? true : false);
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
        AudioListener.pause = !AudioListener.pause;
        PlayerPrefs.SetInt("Mute", AudioListener.pause ? 1 : 0);
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
        SceneManager.LoadScene("Level01");
    }

    public void ButtonLevelT1()
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
