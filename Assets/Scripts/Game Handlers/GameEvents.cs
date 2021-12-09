using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    //sample basic event
    public event Action onAntReachGoal;
    public void AntReachGoal()
    {
        onAntReachGoal?.Invoke();
    }

    //event that passes an int
    public event Action<int> onPuzzleMatch;
    public void PuzzleMatch(int dmg)
    {
        onPuzzleMatch?.Invoke(dmg);
    }

    public event Action onGachaRoll;
    public void GachaRoll()
    {
        onGachaRoll?.Invoke();
    }

    public event Action<int> onGachaSuccess;
    public void GachaSuccess(int rarity)
    {
        onGachaSuccess?.Invoke(rarity);
    }
}
