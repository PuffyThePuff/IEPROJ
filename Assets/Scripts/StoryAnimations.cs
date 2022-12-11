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

    public IEnumerator FadeBackgroundChange(bool isMidDialogue = false)
    {
        FindObjectOfType<DialogueManager>().onAnimation = true;
        FindObjectOfType<DialogueManager>().dialogueUI.SetActive(false);
        FadeBlackTransition.SetTrigger("DramaticSceneEnter");
        yield return new WaitForSeconds(1.9f);
        BackgrondChange(isMidDialogue);
        yield return new WaitForSeconds(1.0f);
        FindObjectOfType<DialogueManager>().StartNextDialogue();
        FindObjectOfType<DialogueManager>().dialogueUI.SetActive(true);
        FindObjectOfType<DialogueManager>().onAnimation = false;
    }

    public IEnumerator FlashBangBackgroundChange(bool isMidDialogue = false)
    {
        FindObjectOfType<DialogueManager>().onAnimation = true;
        FindObjectOfType<DialogueManager>().dialogueUI.SetActive(false);
        FadeBlackTransition.SetTrigger("FlashBang");
        yield return new WaitForSeconds(0.5f);
        BackgrondChange(isMidDialogue);
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<DialogueManager>().dialogueUI.SetActive(true);
        FindObjectOfType<DialogueManager>().onAnimation = false;
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
                //FindObjectOfType<AudioManager>().Stop("RoomBGM");
            }

        }

    }

    public void BackgrondChange(bool isMidDialogue = false)
    {
        FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(false);
        FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(false);
        FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(false);
        FindObjectOfType<BackgroundManager>().InGameBackground.SetActive(false);
        FindObjectOfType<BackgroundManager>().CaveBackground.SetActive(false);
        FindObjectOfType<BackgroundManager>().BedYuukiBackground.SetActive(false);
        FindObjectOfType<BackgroundManager>().NagiSpawn.SetActive(false);
        FindObjectOfType<BackgroundManager>().ChessSpawn.SetActive(false);
        FindObjectOfType<BackgroundManager>().SakuraSpawn.SetActive(false);
        FindObjectOfType<BackgroundManager>().AfterPCSellBG.SetActive(false);
        FindObjectOfType<BackgroundManager>().HallwayClassroom.SetActive(false);
        FindObjectOfType<BackgroundManager>().DepressionBG.SetActive(false);
        FindObjectOfType<BackgroundManager>().FinalText.gameObject.SetActive(false);
        FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.white;

        if (!isMidDialogue)
        {
            if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 0)
            {
                FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(true);
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 5)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 1 && FindObjectOfType<StoryManager>().currentDialogue == 2))
            {
                FindObjectOfType<BackgroundManager>().CharacterBedroom.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 2))
            {
                FindObjectOfType<BackgroundManager>().BedYuukiBackground.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 4))
            {
                FindObjectOfType<BackgroundManager>().BedYuukiBackground.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 2))
            {
                FindObjectOfType<BackgroundManager>().AfterPCSellBG.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 3))
            {
                FindObjectOfType<BackgroundManager>().DepressionBG.SetActive(true);
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 0)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.black;
                FindObjectOfType<BackgroundManager>().FinalText.gameObject.SetActive(true);
                FindObjectOfType<BackgroundManager>().FinalText.text = "...and I played...";
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 1)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.black;
                FindObjectOfType<BackgroundManager>().FinalText.gameObject.SetActive(true);
                FindObjectOfType<BackgroundManager>().FinalText.text = "How long has it been...";
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 2)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.black;
                FindObjectOfType<BackgroundManager>().FinalText.gameObject.SetActive(true);
                FindObjectOfType<BackgroundManager>().FinalText.text = "I'm..tired...";
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 3)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().GachaBackground.GetComponent<Image>().color = Color.black;
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 5 && FindObjectOfType<StoryManager>().currentDialogue == 0)
            {
                FindObjectOfType<BackgroundManager>().HallwayClassroom.SetActive(true);
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 4)
            {
                FindObjectOfType<BackgroundManager>().InGameBackground.SetActive(true);
            }
            else
            {
                FindObjectOfType<BackgroundManager>().EmptyBedroom.SetActive(true);
            }
        }
        else
        {
            if ((FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 2)
                && (FindObjectOfType<DialogueManager>().dequeueIndex >= 3 && FindObjectOfType<DialogueManager>().dequeueIndex < 8))
            {
                FindObjectOfType<BackgroundManager>().CaveBackground.GetComponent<Image>().color = Color.black;
                FindObjectOfType<BackgroundManager>().CaveBackground.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 2)
                && (FindObjectOfType<DialogueManager>().dequeueIndex >= 8 && FindObjectOfType<DialogueManager>().dequeueIndex < 24))
            {
                FindObjectOfType<BackgroundManager>().CaveBackground.GetComponent<Image>().color = Color.white;
                FindObjectOfType<BackgroundManager>().CaveBackground.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 2)
                     && (FindObjectOfType<DialogueManager>().dequeueIndex >= 24))
            {
                FindObjectOfType<BackgroundManager>().InGameBackground.SetActive(true);
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 1 && FindObjectOfType<StoryManager>().currentDialogue == 5)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().NagiSpawn.SetActive(true);
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 3 
                                                                          && (FindObjectOfType<DialogueManager>().dequeueIndex >= 14 && FindObjectOfType<DialogueManager>().dequeueIndex < 24))
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
            }
            else if (FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 3
                                                                          && FindObjectOfType<DialogueManager>().dequeueIndex >= 24)
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().ChessSpawn.SetActive(true);
            }
            else if ((FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 2) 
                     && ( FindObjectOfType<DialogueManager>().dequeueIndex >= 10))
            {
                FindObjectOfType<BackgroundManager>().GachaBackground.SetActive(true);
                FindObjectOfType<BackgroundManager>().SakuraSpawn.SetActive(true);
            }
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
