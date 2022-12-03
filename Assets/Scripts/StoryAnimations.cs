using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        //IF DIALOGUE HAS FINISHED, THEN DO THESE IF STATEMENTS DURING UPDATE
        if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FadeTransition(Values.SceneNames.ClassroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FadeTransition(Values.SceneNames.BedroomScene));
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].hasTriggered = true;
                StartCoroutine(FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM",true);
                Values.Puzzle.isTutorial = true;
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[3].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[3].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[3].hasTriggered = true;
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[4], true);
                FindObjectOfType<AudioManager>().Stop("BattleBGM");
                FindObjectOfType<AudioManager>().Play("RoomBGM", true);
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FadeTransition(Values.SceneNames.ClassroomScene));
            }
            

        }



    }

    IEnumerator FadeTransition(string scenetoLoad)
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
