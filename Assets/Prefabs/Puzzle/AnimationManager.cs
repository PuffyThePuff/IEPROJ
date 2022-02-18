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
        ArrowAnimator.Play("ArrowTutorialAnimation1");
    }
    public void PlayTutorialAnimation2()
    {
        ArrowAnimator.Play("ArrowTutorialAnimation2");
    }

    public void PlayHitAnimation()
    {
        HitAnimator.SetTrigger("Damaged");
        Debug.Log("playing hit animation");
    }
}
