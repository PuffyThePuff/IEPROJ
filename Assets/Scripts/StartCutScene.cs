using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutScene : MonoBehaviour
{
    public void playOtaconnect()
    {
        AudioManager.Instance.Play("OtakonektoSFX", "sfx", false);
    }
    public void LoadScene()
    {
        StartCoroutine(FadeTransition());
    }

    IEnumerator FadeTransition()
    {
        
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync("MainMenuScene");
        yield return new WaitForSeconds(1.0f);
    }
}
