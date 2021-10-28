using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaSystem : MonoBehaviour
{
    //leaving this public for now, might turn private later
    public int[] gachaWeights = {
        985,    //3 star 98.5%
        14,     //4 star 1.4%
        1       //5 star 0.1%
    };

    private int weightTotal = 0;
    private int randomNumber;

    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onGachaRoll += RollGacha;
    }

    private void RollGacha()
    {
        foreach (var item in gachaWeights)
        {
            weightTotal += item;
        }

        randomNumber = Random.Range(0, weightTotal);

        foreach (var weight in gachaWeights)
        {
            if (randomNumber <= weight)
            {
                //give item
            }
            else
            {
                randomNumber -= weight;
            }
        }
    }
}
