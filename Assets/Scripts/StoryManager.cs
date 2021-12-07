using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    public List<StoryChapter> StoryChapters = new List<StoryChapter>(MAX_CHAPTERS);
    public bool isOnDialogue = false;

    public int currentChapter = 0;
    public int currentDialogue = 0;

    private const int MAX_CHAPTERS = 2;

    public void setAllDialogues()
    {
        StoryChapters[0].ChapterDialogues = new Dialogue[10];

        //along in dimly lit room
        //chapter 1 - dialogue 1 - only 1 speaker
        StoryChapters[0].ChapterDialogues[0] = new Dialogue();
        StoryChapters[0].ChapterDialogues[0].name = "MC";
        StoryChapters[0].ChapterDialogues[0].sentences = new string[6];
        StoryChapters[0].ChapterDialogues[0].sentences[0] = "Well then… I guess that’s it for us then.";
        StoryChapters[0].ChapterDialogues[0].sentences[1] = "Five years, gone, just like that.";
        StoryChapters[0].ChapterDialogues[0].sentences[2] = "...";
        StoryChapters[0].ChapterDialogues[0].sentences[3] = "It’s only been a week and...";
        StoryChapters[0].ChapterDialogues[0].sentences[4] = "*sigh*";
        StoryChapters[0].ChapterDialogues[0].sentences[5] = "Time for school I guess.";
        //fade to black i guess...

        //goes to school and meets new friend 
        //chapter 1 - dialogue 2 - 2 speakers
        StoryChapters[0].ChapterDialogues[1] = new Dialogue();
        StoryChapters[0].ChapterDialogues[1].name = "MC";
        StoryChapters[0].ChapterDialogues[1].otherName = "NewFriend";
        StoryChapters[0].ChapterDialogues[1].sentences = new string[6];
        StoryChapters[0].ChapterDialogues[1].sentences[0] = "Well then… I guess that’s it for us then.";
        StoryChapters[0].ChapterDialogues[1].sentences[1] = "Five years, gone, just like that.";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "...";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "It’s only been a week and...";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "*sigh*";
        StoryChapters[0].ChapterDialogues[1].sentences[2] = "Time for school I guess.";
    }

    public void setAllChapters()
    {
        
        for (int i = 0; i < MAX_CHAPTERS; i++)
        {
            StoryChapters[i] = new StoryChapter();
            StoryChapters[i].ChapterNumber = i + 1;
        }
    }

    public void NextChapter()
    {
        currentChapter++;
        currentDialogue = 0;
    }

    public void Chapter1Dialogue2()
    {
        if (StoryChapters[0].ChapterDialogues[0].isDone)
        {
            //move character animation
            //load to classroom
            //then play next dialogue
            FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[1]);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        setAllChapters();
        setAllDialogues();
        FindObjectOfType<DialogueManager>().StartDialogue(StoryChapters[0].ChapterDialogues[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
