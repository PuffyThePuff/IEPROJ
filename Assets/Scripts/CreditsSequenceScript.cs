using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsSequenceScript : MonoBehaviour
{
    [SerializeField] private GameObject[] creditBlocks;

    private float timer = 3.0f;
    private int currentBlock = 0;

    private void Start()
    {
        creditBlocks[currentBlock].SetActive(true);

        AudioManager.Instance.Stop("SandCollegeBGM", "bgm");
        AudioManager.Instance.Play("TheWorldIsGrayBGM", "bgm", true);
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
        {
            if (currentBlock < creditBlocks.Length) creditBlocks[currentBlock].SetActive(false);
            else SceneManager.LoadScene("OtaconnectStart");

            if (currentBlock >= creditBlocks.Length)
            {
                AudioManager.Instance.Stop("TheWorldIsGrayBGM", "bgm");
                SceneManager.LoadScene("MainMenuScene");
            }

            else
            {
                creditBlocks[currentBlock].SetActive(false);
                currentBlock++;
                if (currentBlock < creditBlocks.Length) creditBlocks[currentBlock].SetActive(true);
            }

            timer = 3.0f;
        }
    }
}