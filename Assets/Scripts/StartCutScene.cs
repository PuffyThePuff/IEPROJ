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
        StartCoroutine(FadeTransition());
    }

    IEnumerator FadeTransition()
    {
        
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync("MainMenu");
        yield return new WaitForSeconds(1.0f);
    }
}
