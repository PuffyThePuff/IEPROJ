using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    
    public GameObject dialogueUI;

    public Image Speaker1Image;
    public Image Speaker2Image;

    public GameObject name1TextBox;
    public GameObject name2TextBox;
    public Text name1Text;
    public Text name2Text;
    
    public Text dialogueText;
    public GameObject bottomRightMC;

    private Queue<string> sentences;
    private bool isStoryDialogue = false;
    private int dequeueIndex = 0;

    private string currentSentence;
    private bool isFullSentence = false;

    private int ChapterNum;
    private int DialogueNum;

    

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartDialogue(Dialogue dialogue, bool isStoryRelated = false)
    {
        

        bottomRightMC.SetActive(false);
        name1TextBox.SetActive(false);
        name2TextBox.SetActive(false);
        Speaker2Image.gameObject.SetActive(false);

        isStoryDialogue = isStoryRelated;
        dequeueIndex = 0;
        FindObjectOfType<StoryManager>().isOnDialogue = true;
        sentences = new Queue<string>();
        dialogueUI.SetActive(true);

        Debug.Log("Starting conversation with " + dialogue.name);

        name1Text.text = dialogue.name;
        Speaker1Image.sprite = dialogue.speaker1Sprites[0];

        ChapterNum = dialogue.chapterNum;
        DialogueNum = dialogue.dialogueIndex;
        Speaker1Image.gameObject.SetActive(true);
        if ((ChapterNum == 0 && DialogueNum == 0) || (ChapterNum == 0 && DialogueNum == 5))
        {
            Speaker1Image.gameObject.SetActive(false);
        }
       
        if (dialogue.otherName != "")
        {
            name2Text.text = dialogue.otherName;
            Speaker2Image.sprite = dialogue.speaker2Sprites[0];
            Speaker2Image.gameObject.SetActive(true);
        }
        else
        {
            name2Text.text = "";
        }

        if (sentences != null)
            sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        bool isSpeaker1 = false;
        bool isSpeaker2 = false;

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        string sentence = sentences.Dequeue();
        Speaker1Image.gameObject.SetActive(true);
        if ((ChapterNum == 0 && DialogueNum == 0) || (ChapterNum == 0 && DialogueNum == 5))
        {
            Debug.Log("mc false 2");
            Speaker1Image.gameObject.SetActive(false);
        }

        if (name2Text.text != "")
            Speaker2Image.gameObject.SetActive(true);

        foreach (int index in FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
            .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker1Lines)
        {
            if (index == dequeueIndex)
            {
                //grey out sprite2
                name1TextBox.SetActive(true);
                name2TextBox.SetActive(false);
                isSpeaker1 = true;
            }
        }

        if (name2Text.text != "")
        {
            foreach (int index in FindObjectOfType<StoryManager>()
                .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker2Lines)
            {
                if (index == dequeueIndex)
                {
                    //grey out sprite1
                    name1TextBox.SetActive(false);
                    name2TextBox.SetActive(true);
                    isSpeaker2 = true;
                }
            }
        }

        if (!isSpeaker1 && !isSpeaker2)
        {
            Debug.Log("mc false 3");

            name1TextBox.SetActive(false);
            name2TextBox.SetActive(false);
            Speaker1Image.gameObject.SetActive(false);
            Speaker2Image.gameObject.SetActive(false);
        }
        dequeueIndex++;
        

        currentSentence = sentence;
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        if (sentence != null)
        {
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }
        }
        else
        {
            Debug.Log("sentence is blank");
        }
    }


    void EndDialogue()
    {
        FindObjectOfType<StoryManager>().isOnDialogue = false;
        Debug.Log("End of Conversation");
        dialogueUI.SetActive(false);
        if (isStoryDialogue)
        {
            FadeToBlackTransitions(); //ALSO ADDS CURRENT DIALOGUE/CHAPTER

            FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;

            FindObjectOfType<StoryManager>().currentDialogue++;

            if (FindObjectOfType<StoryManager>().currentDialogue >= FindObjectOfType<StoryManager>()
                    .StoryChapters[FindObjectOfType<StoryManager>().currentChapter].ChapterDialogues.Length)
            {

                FindObjectOfType<StoryManager>().currentDialogue = 0;
                FindObjectOfType<StoryManager>().currentChapter++;

            }

            onDialogueFinish();

        }
        
        //bottomRightMC.SetActive(true);
        isStoryDialogue = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<StoryManager>().isOnDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (isFullSentence == false)
                {
                    StopAllCoroutines();
                    dialogueText.text = currentSentence;
                    isFullSentence = true;
                }
                else
                {
                    DisplayNextSentence();
                    isFullSentence = false;
                }
            }
        }
    }

    void FadeToBlackTransitions()
    {
        if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 4)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 5)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            
        }
        //before chapter 1
        else if (FindObjectOfType<StoryManager>().currentChapter == 0 && FindObjectOfType<StoryManager>().currentDialogue == 6)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            FindObjectOfType<AudioManager>().Play("Birds", true);
            FindObjectOfType<AudioManager>().Stop("RoomBGM");
        }
    }

    public void StartNextDialogue()
    {
        //DO THIS ONLY IF NEXT DIALOGUE IS SAME SCENE
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 5)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[5].isDone
                && FindObjectOfType<BackgroundManager>().GachaBackground.activeInHierarchy)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[5], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 0 &&
            FindObjectOfType<StoryManager>().currentDialogue == 6)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[6], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 1 &&
            FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].isDone)
            {
                FindObjectOfType<StoryManager>().currentChapter = 1;
                FindObjectOfType<StoryManager>().currentDialogue = 0;

                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0], true);
            }
        }
        

    }

    void onDialogueFinish()
    {
        //IF DIALOGUE HAS FINISHED, THEN DO THESE IF STATEMENTS DURING UPDATE
        //DO THIS WHEN LOADING SCENES
        if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.ClassroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.BedroomScene));
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[2].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM",true);
                Values.Puzzle.isTutorial = true;
            }

        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.ClassroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[1].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[1].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.BedroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[2].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[2].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[2].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.ClassroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[3].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[3].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[3].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.BedroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[4].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[4].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[4].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = true;
            }
        }
    }
}
