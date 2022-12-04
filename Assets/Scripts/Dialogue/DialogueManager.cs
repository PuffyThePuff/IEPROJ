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
    public int dequeueIndex = 0;

    private string currentSentence;
    private bool isFullSentence = false;

    private int ChapterNum;
    private int DialogueNum;

    public bool onAnimation = false;
    public bool hasDialogueEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        onAnimation = false;
        hasDialogueEnded = false;
    }

    public void StartDialogue(Dialogue dialogue, bool isStoryRelated = false)
    {

        hasDialogueEnded = false;
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
        if (dialogue.speaker1Sprites != null)
        {
            Speaker1Image.sprite = dialogue.speaker1Sprites[0];
            Speaker1Image.gameObject.SetActive(true);
        }

        ChapterNum = dialogue.chapterNum;
        DialogueNum = dialogue.dialogueIndex;
        
        if ((ChapterNum == 0 && DialogueNum == 0) || (ChapterNum == 0 && DialogueNum == 5))
        {
            Speaker1Image.gameObject.SetActive(false);
        }
       
        if (dialogue.otherName != "")
        {
            name2Text.text = dialogue.otherName;
            if (dialogue.speaker2Sprites != null)
            {
                Speaker2Image.sprite = dialogue.speaker2Sprites[0];
                Speaker2Image.gameObject.SetActive(true);
            }
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
            //Debug.Log("mc false 2");
            Speaker1Image.gameObject.SetActive(false);
        }

        if (name2Text.text != "")
            Speaker2Image.gameObject.SetActive(true);

        TriggerCutSceneInDialogue();

        foreach (int index in FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                     .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker1Lines)
        {
            if (index == dequeueIndex)
            {
                //grey out sprite2
                Speaker2Image.color = Color.grey;
                Speaker1Image.color = Color.white;
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
                    Speaker1Image.color = Color.grey;
                    Speaker2Image.color = Color.white;
                    name1TextBox.SetActive(false);
                    name2TextBox.SetActive(true);
                    isSpeaker2 = true;
                }
            }
        }

        

        if (FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker1ExpressionIndex != null)
        {
            
            int expressionType = FindObjectOfType<StoryManager>()
                .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker1ExpressionIndex[dequeueIndex];

            if (FindObjectOfType<StoryManager>()
                    .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker1Sprites != null)
            {
                Speaker1Image.sprite = FindObjectOfType<StoryManager>()
                    .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker1Sprites[expressionType];
            }

        }

        if (FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker2ExpressionIndex != null)
        {
            
            int expressionType = FindObjectOfType<StoryManager>()
                .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker2ExpressionIndex[dequeueIndex];

            if (FindObjectOfType<StoryManager>()
                    .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker2Sprites != null)
            {
                Speaker2Image.sprite = FindObjectOfType<StoryManager>()
                    .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].speaker2Sprites[expressionType];

            }

        }


        if (!isSpeaker1 && !isSpeaker2)
        {
            //Debug.Log("mc false 3");

            name1TextBox.SetActive(false);
            name2TextBox.SetActive(false);
            Speaker1Image.gameObject.SetActive(false);
            Speaker2Image.gameObject.SetActive(false);
        }

        HideImageOnSpecialCondition();
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
        hasDialogueEnded = true;
        FindObjectOfType<StoryManager>().isOnDialogue = false;
        Debug.Log("End of Conversation");
        dialogueUI.SetActive(false);
        if (isStoryDialogue)
        {
            FadeToBlackTransitions(); //ALSO ADDS CURRENT DIALOGUE/CHAPTER

            FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;

            IncrementDialogue();

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
            if (Input.GetKeyDown(KeyCode.Mouse0) && !onAnimation)
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
        //IF PREVIOUS SCENE IS SAME SCENE
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
        //before chapter 2
        else if (FindObjectOfType<StoryManager>().currentChapter == 1 && FindObjectOfType<StoryManager>().currentDialogue == 6)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            FindObjectOfType<AudioManager>().Play("Birds", true);
            FindObjectOfType<AudioManager>().Stop("RoomBGM");
        }
        //before chapter 3
        else if (FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 4)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            FindObjectOfType<AudioManager>().Play("Birds", true);
            FindObjectOfType<AudioManager>().Stop("RoomBGM");
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            //FindObjectOfType<AudioManager>().Play("Birds", true);
            //FindObjectOfType<AudioManager>().Stop("BattleBGM");
        }
        else if (FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            StartCoroutine(FindObjectOfType<StoryAnimations>().FadeBackgroundChange());
            //FindObjectOfType<AudioManager>().Play("Birds", true);
            //FindObjectOfType<AudioManager>().Stop("BattleBGM");
        }
        //before chapter 4
        else if (FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 3)
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
        if (FindObjectOfType<StoryManager>().currentChapter == 2 &&
            FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[0].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[0], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 3 &&
            FindObjectOfType<StoryManager>().currentDialogue == 0)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[0].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[0], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 3 &&
            FindObjectOfType<StoryManager>().currentDialogue == 1)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[1].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[1], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 3 &&
            FindObjectOfType<StoryManager>().currentDialogue == 2)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[2].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[2], true);
            }
        }
        if (FindObjectOfType<StoryManager>().currentChapter == 3 &&
            FindObjectOfType<StoryManager>().currentDialogue == 3)
        {
            if (SceneManager.GetActiveScene().name == Values.SceneNames.BedroomScene
                && !FindObjectOfType<StoryManager>().isOnDialogue && !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3].isDone)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3], true);
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
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = true;
                Values.Enemy.enemyLevel = 1;
                Values.Enemy.maxHP = 150;
                Values.Enemy.dmg = 100;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 0.5f; //
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 200.0f;//
                Values.Puzzle.hexBlockerCount = 0;

                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[4].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
                
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[5].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[5].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = false;
                Values.Enemy.enemyLevel = 1;
                Values.Enemy.maxHP = 150;
                Values.Enemy.dmg = 100;

                //NERFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 50.0f;//
                Values.Puzzle.hexBlockerCount = 0;

                //buffed player
                Values.Player.setStunAmount = 3;
                

                FindObjectOfType<StoryManager>().StoryChapters[1].ChapterDialogues[5].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);

            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[0].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[0].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.ClassroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[1].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[1].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.BedroomScene));
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[2].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[2].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = true;
                Values.Enemy.enemyLevel = 2;
                Values.Enemy.maxHP = 300;
                Values.Enemy.dmg = 100;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 0.5f; //
                Values.Puzzle.PainHexPosionDamage = 100.0f;//
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 0;

                //set normal values
                Values.Player.setStunAmount = 1;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 5;

                FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[2].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[3].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[3].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = true;
                Values.Enemy.enemyLevel = 2;
                Values.Enemy.maxHP = 300;
                Values.Enemy.dmg = 100;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f; //
                Values.Puzzle.PainHexPosionDamage = 50.0f;//
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 0;

                //buffed player
                Values.Player.basicHeal = 0.5f;//

                FindObjectOfType<StoryManager>().StoryChapters[2].ChapterDialogues[3].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[0].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[0].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = true;
                Values.Enemy.enemyLevel = 3;
                Values.Enemy.maxHP = 400;
                Values.Enemy.dmg = 150;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 0.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 5;//

                //set normal values
                Values.Player.setStunAmount = 1;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 5;

                FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[2].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[2].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = true;
                Values.Enemy.enemyLevel = 3;
                Values.Enemy.maxHP = 400;
                Values.Enemy.dmg = 150;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 5;//

                //buff player
                Values.Player.basicDamage = 12;

                FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[2].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = false;
                Values.Enemy.enemyLevel = 0;
                Values.Enemy.maxHP = 150;
                Values.Enemy.dmg = 50;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 0;//

                //set normal values
                Values.Player.setStunAmount = 1;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 5;

                FindObjectOfType<StoryManager>().StoryChapters[3].ChapterDialogues[3].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[0].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[0].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = false;
                Values.Enemy.enemyLevel = 0;
                Values.Enemy.maxHP = 150;
                Values.Enemy.dmg = 25;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 0;//

                //set normal values
                Values.Player.setStunAmount = 1;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 5;

                FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                //FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[1].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[1].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                Values.Puzzle.isTutorial = false;
                Values.Puzzle.isRigged = false;
                Values.Enemy.enemyLevel = 0;
                Values.Enemy.maxHP = 150;
                Values.Enemy.dmg = 25;

                //BUFFED ENEMY
                Values.Enemy.attackInterval = 1.5f;//
                Values.Puzzle.PainHexPosionDamage = 0.0f;
                Values.Puzzle.BlackHexBurstDamage = 0.0f;
                Values.Puzzle.hexBlockerCount = 0;//

                //set normal values
                Values.Player.setStunAmount = 1;
                Values.Player.basicHeal = 0.01f;
                Values.Player.basicDamage = 5;

                FindObjectOfType<StoryManager>().StoryChapters[4].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.PuzzleScene));
                FindObjectOfType<AudioManager>().Stop("RoomBGM");
                //FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }
        else if (FindObjectOfType<StoryManager>().StoryChapters[5].ChapterDialogues[0].isDone &&
                 !FindObjectOfType<StoryManager>().StoryChapters[5].ChapterDialogues[0].hasTriggered)
        {
            if (FindObjectOfType<StoryAnimations>().FadeBlackTransition != null)
            {
                
                FindObjectOfType<StoryManager>().StoryChapters[5].ChapterDialogues[0].hasTriggered = true;
                StartCoroutine(FindObjectOfType<StoryAnimations>().FadeTransition(Values.SceneNames.LogoScene));
                //FindObjectOfType<AudioManager>().Stop("RoomBGM");
                //FindObjectOfType<AudioManager>().Play("BattleBGM", true);
            }
        }



    }

    void IncrementDialogue()
    {
        if (!(FindObjectOfType<StoryManager>().currentChapter == 1 && FindObjectOfType<StoryManager>().currentDialogue == 4) && 
            !(FindObjectOfType<StoryManager>().currentChapter == 1 && FindObjectOfType<StoryManager>().currentDialogue == 5) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 2) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 2 && FindObjectOfType<StoryManager>().currentDialogue == 3) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 0) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 3 && FindObjectOfType<StoryManager>().currentDialogue == 2) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 0) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 4 && FindObjectOfType<StoryManager>().currentDialogue == 1) &&
            !(FindObjectOfType<StoryManager>().currentChapter == 5 && FindObjectOfType<StoryManager>().currentDialogue == 0))
        {
            FindObjectOfType<StoryManager>().currentDialogue++;
        }
        
        
    }

    void HideImageOnSpecialCondition()
    {
        Color alphaColor = Color.clear;
        if ((FindObjectOfType<StoryManager>().currentChapter == 0 &&
             FindObjectOfType<StoryManager>().currentDialogue == 2) && dequeueIndex <= 2)
        {
            
            Speaker2Image.color = Color.clear;
            Speaker1Image.color = Color.white;
        }
        else if ((FindObjectOfType<StoryManager>().currentChapter == 0 &&
                  FindObjectOfType<StoryManager>().currentDialogue == 2) && dequeueIndex > 2)
        {
            Speaker1Image.color = Color.clear;
            Speaker2Image.color = Color.white;

            
            Speaker2Image.gameObject.SetActive(true);
        }
        else if ((FindObjectOfType<StoryManager>().currentChapter == 0 &&
                  FindObjectOfType<StoryManager>().currentDialogue == 4) )
        {
            Speaker1Image.color = Color.clear;
            Speaker2Image.color = Color.white;


            Speaker2Image.gameObject.SetActive(true);
        }
    }

    void TriggerCutSceneInDialogue()
    {
        if ((FindObjectOfType<StoryManager>().currentChapter == 0 &&
             FindObjectOfType<StoryManager>().currentDialogue == 2) && dequeueIndex == 3)
        {
            
            //FindObjectOfType<StoryManager>().isOnDialogue = false;
            //dialogueUI.SetActive(false);
                
            StartCoroutine(FindObjectOfType<StoryAnimations>().FlashBangBackgroundChange(true));

            
        }
    }


    
}
