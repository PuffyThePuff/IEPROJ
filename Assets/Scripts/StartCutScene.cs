using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartCutScene : MonoBehaviour
{
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
