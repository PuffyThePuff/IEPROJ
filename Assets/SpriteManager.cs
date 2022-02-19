using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public SpriteRenderer BossImage;

    public Sprite Slime;
    public Sprite Rat;
    public Sprite Imp;
    public Sprite Plant;
    public Sprite Orge;

    // Start is called before the first frame update
    void Start()
    {
        if (Values.Enemy.enemyLevel == 0)
        {
            BossImage.sprite = Slime;
        }
        else if (Values.Enemy.enemyLevel == 1)
        {
            BossImage.sprite = Rat;
        }
        else if (Values.Enemy.enemyLevel == 2)
        {
            BossImage.sprite = Imp;
        }
        else if (Values.Enemy.enemyLevel == 3)
        {
            BossImage.sprite = Plant;
        }
        else if (Values.Enemy.enemyLevel == 4)
        {
            BossImage.sprite = Orge;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
