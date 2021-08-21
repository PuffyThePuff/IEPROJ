using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOnShadowcaster : MonoBehaviour
{
    //this turns on shadowcaster in sprite renderer so you don't have to do it manually every time
    void Start()
    {
        GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
}
