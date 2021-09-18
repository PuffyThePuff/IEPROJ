using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutScene : MonoBehaviour
{
    public AudioSource otaconnect;
    public void playOtaconnect()
    {
        otaconnect.Play();
    }
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
