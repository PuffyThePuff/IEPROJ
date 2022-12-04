using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryAnimations : MonoBehaviour
{
    public Animator FadeBlackTransition;
    public GameObject AlphaPopUp;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator FadeTransition(string scenetoLoad)
    {
        FadeBlackTransition.SetTrigger("DramaticSceneEnter");
        SceneChange();
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(scenetoLoad);
        yield return new WaitForSeconds(1.0f);
        
    }

    public IEnumerator FadeBackgroundChange()
    {
        
        FadeBlackTransition.SetTrigger("DramaticSceneEnter");
        yield return new WaitForSeconds(1.9f);
        BackgrondChange();
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<DialogueManager>().StartNextDialogue();
    }


    public void AlphaPopUpButton()
    {
        if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                AlphaPopUp.SetActive(false);
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].hasTriggered = true;
                StartCoroutine(FadeTransition("LevelSetupTest"));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
            }

        }

    }

    public void BackgrondChange()
    {
        if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(false);
            FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(true);
            FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(false);
            
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 5)
        {
            FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
            FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(false);
            FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(false);
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
            FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.black;
            FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(false);
            FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(false);
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 3)
        {
            FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
            FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.black;
            FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(false);
            FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(false);
        }
        else
        {
            FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(true);
            FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(false);
            FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(false);
        }
    }

    public void SceneChange()
    {
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(true);
            FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(false);
            FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(false);
        }
    }
}
