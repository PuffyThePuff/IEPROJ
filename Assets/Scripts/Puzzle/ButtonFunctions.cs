using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void Attack()
    {
        if(!GameManager.isTutorial)
        {
            if(GameManager.Instance.selected.Count >= 3)
            {
                GameManager.Instance.Attack();
                StartCoroutine(GameManager.Instance.DelayedRefreshBoard());
            }
            
        }
        
        else
        {
            if(GameManager.tutorialPhase == 1 && GameManager.Instance.selected.Count == 2)
            {
                GameManager.tutorialPhase = 2;
                GameManager.Instance.Attack();
                StartCoroutine(GameManager.Instance.DelayedRefreshBoard());
                
            }

            else if (GameManager.tutorialPhase == 3 && GameManager.Instance.selected.Count >= 3)
            {
                GameManager.tutorialPhase = 4;
                GameManager.Instance.Attack();
                StartCoroutine(GameManager.Instance.DelayedRefreshBoard());
            }
        }
    }
}
