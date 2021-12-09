using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;
    public string otherName;

    [TextArea(3, 10)]
    public string[] sentences;

    public string[] decision1;
    public string[] decision2;

    public int[] indexWithDecision;

    public int[] speaker1Lines;
    public int[] speaker2Lines;

    public Sprite[] speaker1Sprites;
    public Sprite[] speaker2Sprites;

    public bool isDone = false;
    public bool hasTriggered = false;
}
