using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAnimations : MonoBehaviour
{
    public Animator MoveToSchoolAnim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].isDone &&
            !FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered)
        {
            MoveToSchoolAnim.SetTrigger("C1D1Done");
            FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[0].hasTriggered = true;
        }
    }
}
