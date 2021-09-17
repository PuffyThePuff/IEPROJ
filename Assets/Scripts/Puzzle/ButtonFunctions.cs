using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunctions : MonoBehaviour
{

    // Start is called before the first frame update
    public void Attack()
    {
        GameManager.Instance.Attack();
        StartCoroutine(GameManager.Instance.DelayedRefreshBoard());
    }
}
