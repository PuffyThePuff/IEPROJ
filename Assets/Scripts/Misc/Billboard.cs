using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    //Makes the object always face the camera
    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 45f, 0f);
    }
}
