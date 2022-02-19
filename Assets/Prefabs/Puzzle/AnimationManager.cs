using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public static AnimationManager Instance;
    public Animator ArrowAnimator;
    public Animator HitAnimator;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    // Start is called before the first frame update
    void Start()
    {

        //Debug.Log("play animation");
        //HitAnimator.Play("Hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayTutorialAnimation1()
    {
        //if (ArrowAnimator != null && ArrowAnimator.gameObject.activeSelf && ArrowAnimator.isActiveAndEnabled)
        //{
        //    ArrowAnimator.Play("Idle", 0, 0.0f);
        //}

        // ArrowAnimator.SetTrigger("TutorialPhase1Start");

        ArrowAnimator.Play("ArrowTutorialAnimation1");
    }

    public void StopTutorialAnimation1()
    {
        ArrowAnimator.Play("Idle");
    }
    public void PlayTutorialAnimation2()
    {
        ArrowAnimator.Play("ArrowTutorialAnimation2");
    }

    public void StopTutorialAnimation2()
    {
        ArrowAnimator.Play("Idle");
    }

    public void PlayHitAnimation()
    {
        HitAnimator.SetTrigger("Damaged");
        Debug.Log("playing hit animation");
    }
}
