using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteManager : MonoBehaviour
{
    public SpriteRenderer BossImage;

    public Sprite TutorialMob;
    public Sprite BlackHexMob;
    public Sprite LockHexMob;
    public Sprite PainHexMob;
    public Sprite FinalBoss;
    public Sprite TrashMob;

    // Start is called before the first frame update
    void Start()
    {
        if (Values.Enemy.enemyLevel == 0)
        {
            BossImage.sprite = TutorialMob;
        }
        else if (Values.Enemy.enemyLevel == 1)
        {
            BossImage.sprite = BlackHexMob;
        }
        else if (Values.Enemy.enemyLevel == 2)
        {
            BossImage.sprite = PainHexMob;
        }
        else if (Values.Enemy.enemyLevel == 3)
        {
            BossImage.sprite = LockHexMob;
        }
        else if (Values.Enemy.enemyLevel == 4)
        {
            BossImage.sprite = FinalBoss;
        }
        else if (Values.Enemy.enemyLevel == 5)
        {
            BossImage.sprite = TrashMob;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
