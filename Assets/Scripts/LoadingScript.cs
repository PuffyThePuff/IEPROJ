using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScript : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private Slider slider;

    public void onQuit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        StartCoroutine(LoadAsync());
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation i = SceneManager.LoadSceneAsync("TransitionSample");
        //AsyncOperation j = SceneManager.LoadSceneAsync("RoomSample", LoadSceneMode.Additive);
        loadingScreen.SetActive(true);
        mainMenuScreen.SetActive(false);
        //yield return new WaitForSeconds(3.0f);

        while (!i.isDone)
        {
            float progress = Mathf.Clamp01(i.progress / 0.9f);
            slider.value = progress;
            yield return null;
        }
    }
}
