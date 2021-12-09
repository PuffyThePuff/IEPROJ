using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryAnimations : MonoBehaviour
{
    public Animator MoveToSchoolAnim;
    public Animator FadeBlackTransition;

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
                MoveToSchoolAnim = null;
                FindObjectOfType<StoryManager>().StoryChapters[0].ChapterDialogues[1].hasTriggered = true;
                StartCoroutine(FadeTransition());
            }

        }


    }

    IEnumerator FadeTransition()
    {
        FadeBlackTransition.SetTrigger("DramaticSceneEnter");
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadSceneAsync("TransitionSample");
        yield return new WaitForSeconds(1.0f);
    }
}
