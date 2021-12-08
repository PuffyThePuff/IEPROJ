using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector3 playerPosition;
    public VectorValue playerStorage;
    public Animator FadeBlackTransition;

    private void Start()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FadeTransition());
            
        }
    }

    IEnumerator FadeTransition()
    {
        FadeBlackTransition.SetTrigger("DramaticSceneEnter");
        yield return new WaitForSeconds(1.0f);
        playerStorage.initialValue = playerPosition;
        SceneManager.LoadSceneAsync(sceneToLoad);
        yield return new WaitForSeconds(1.0f);

        
    }
}
