using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class StoryChapter
{
    public int ChapterNumber;
    public string ChapterTitle;
    public string[] ChapterScenes;
    public Dialogue[] ChapterDialogues;
    public Animation[] ChapterAnimations;

    public Vector3 playerSpawn;

}
