using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryAnimations : MonoBehaviour
{
    public Animator MoveToSchoolAnim;
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
            if (MoveToSchoolAnim != null)
            {
                MoveToSchoolAnim.SetTrigger("C1D1Done");
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered = true;
                
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FadeTransition("TransitionSample"));
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].hasTriggered = true;
                StartCoroutine(FadeTransition("Puzzle"));
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
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[4].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[4].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[4].hasTriggered = true;
                StartCoroutine(FadeTransition("GachaSample"));
               
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].hasTriggered)
        {
            //POP UP THE ALPHA BUILD TEXT
            AlphaPopUp.SetActive(true);
            //TODO: PROCEED TO NEXT CHAPTER

        }



    }

    IEnumerator FadeTransition(string scenetoLoad)
    {
        FadeBlackTransition.SetTrigger("DramaticSceneEnter");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync(scenetoLoad);
        yield return new WaitForSeconds(1.0f);
    }

    public void gachatutorReturnButton()
    {
        if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[5].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[5].hasTriggered)
        {
            if (FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[5].hasTriggered = true;
                StartCoroutine(FadeTransition("TransitionSample"));

            }

        }
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
}
