using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleUIManager : MonoBehaviour
{
    public static PuzzleUIManager Instance;
    //enemy hp bar ui
    public Image enemyHpBar;

    //characters hp bar ui
    public Image c1HpBar;
    public Image c2HpBar;
    public Image c3HpBar;

    //characters special piece charge bar ui
    public Image c1ChargeBar;
    public Image c2ChargeBar;
    public Image c3ChargeBar;

    //characters 2d model art ui
    public Image c1Sprite;
    public Image c2Sprite;
    public Image c3Sprite;

    //assigned speaker image
    public Image speakerPortrait;

    //tutorial help dialogues ui
    public GameObject helpDialogue1;
    public GameObject helpDialogue2;
    public GameObject helpDialogue3;
    public GameObject helpDialogue4;
    public GameObject helpDialogue5;
    public GameObject endText;

    public Text charDialogue1;
    public Text charDialogue2;
    public Text charDialogue3;

    public float catchphraseDuration = 3.5f;
    public float catchPhraseTick = 0.0f;

    //tutorial arrow ui
    public GameObject mainArrow;
    public GameObject arrowGroup1;
    public GameObject arrowGroup2;
    public GameObject arrowGroup3;
    public GameObject arrowGroup4;
    public GameObject arrowGroup5;

    public GameObject stunIndicator;
    public Text stunCounter;

    public Image painHexTriggerBar;
    public Image lockHexTransferBar;
    public Image BackgroundImage;

    public Sprite[] BGSprites;

    //enemy
    public SpriteRenderer enemyBoss;

    //mob sprites
    public Sprite[] enemySprites;

    public GameObject AllGameHUD;
    public GameObject FadeToBlackPanel;
    public GameObject Text;

    public Color FadeToBlackColor;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    public void SetEnemyBossSprite(int index)
    {
        enemyBoss.sprite = enemySprites[index];
    }

    public void Update()
    {
        
    }

}
