using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentTransition : MonoBehaviour
{
    public GameObject cameraObj;
    public Transform phoneOnTrans;
    public Transform phoneOffTrans;

    public void moveCameraPhoneOn()
    {
        cameraObj.transform.position = phoneOnTrans.position; 
    }

    public void moveCameraPhoneOff()
    {
        cameraObj.transform.position = phoneOffTrans.position;
    }



}
