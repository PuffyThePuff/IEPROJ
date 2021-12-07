using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dialogueUI;
    public Text nameText;
    public Text dialogueText;
    private Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartDialogue(Dialogue dialogue)
    {
        
        FindObjectOfType<StoryManager>().isOnDialogue = true;
        sentences = new Queue<string>(100);
        dialogueUI.SetActive(true);

        Debug.Log("Starting conversation with " + dialogue.name);

        nameText.text = dialogue.name;

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
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        StopAllCoroutines();
        string sentence = sentences.Dequeue();
        Debug.Log("after dequeue " + sentences.Count);

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
        FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter].ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = false;
        FindObjectOfType<StoryManager>().currentDialogue++;
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<StoryManager>().isOnDialogue)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StopAllCoroutines();
                DisplayNextSentence();
            }
        }
    }
}
