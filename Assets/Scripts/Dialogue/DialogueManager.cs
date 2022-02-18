using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instanceRef;
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

    void Awake()
    {
        /*if (instanceRef == null)
        {
            instanceRef = this;
            //DontDestroyOnLoad(gameObject);
        }
        else if (instanceRef != this)
        {
        }
        //Destroy(gameObject);*/
    }

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
        if (ChapterNum == 0 && DialogueNum == 0)
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
        if (ChapterNum == 0 && DialogueNum == 0)
        {
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
            FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
            FindObjectOfType<StoryManager>().currentDialogue++;
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
}
