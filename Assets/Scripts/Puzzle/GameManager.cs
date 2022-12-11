using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int currentTurn = 0;
    public int maxTurn = 99;

    public static GameManager Instance; //instance that can be called anywhere
    public List<GameObject> pieces; //list of all piece gameobjects where index 0-2 are plain pieces
    public List<GameObject> neutrals; //neutral pieces template;
    public int[,] nBoard;   //integer representation of board
    public GameObject[,] gBoard;    //gameobject board with reference to each piece
    public bool isBoardInteractable = true; // pieces can be selected if true
    public List<GameObject> selected;   //list of selected pieces including powerups
    public List<GameObject> powerups;   //list of selected powerups
    public List<GameObject> lockedHexes;


    public float catchphraseDuration = 3.5f;
    public float catchPhraseTick = 0.0f;
    

    //hold timer
    private float holdTime = 5.0f;
    private float holdTick = 0.0f;

    
    //player special piece count and references
    public int specialPiecesCount = 0;
    public GameObject specialPiece1 = null;
    public GameObject specialPiece2 = null;
    public GameObject specialPiece3 = null;

    //neutral pieces reference
    public GameObject neutralPiece1 = null;
    public GameObject neutralPiece2 = null;
    public GameObject PainHex = null;


    //special pieces index and characters hp
    public int c1Index = 3;
    public int c2Index = 4;
    public int c3Index = 5;
    private float c1MaxHp = 100;
    private float c2MaxHp = 100;
    private float c3MaxHp = 100;
    private float c1CurrentHp = 100;
    private float c2CurrentHp = 100;
    private float c3CurrentHp = 100;
    private float c1MaxCharge = 100;
    private float c2MaxCharge = 100;
    private float c3MaxCharge = 100;
    public float c1CurrentCharge = 0;
    public float c2CurrentCharge = 0;
    public float c3CurrentCharge = 0;
    public bool playerIsFrozen = false;
    public float playerFrozenDuration = 1.5f;
    public float playerFrozenTick = 0.0f;
    public bool playerIsPoisoned = false;
    public float playerPoisonedDuration = 1.5f;
    public float playerPoisonedTick = 0.0f;
    public float playerPoisonedDamage = 1.0f;
    private float basicDamage = 10;
    private float enhancedDamageMultiplier = 60;
    private float damageMultiplier = 0.0f;



    //player stats
    private float currentDamageCounter = 0;
    private float currentHealCounter = 0;
    private float currentDamageMultiplier = 1;
    private float currentHealMultiplier = 1;

    //status effects
    private bool enemyStunned = false;
    private int enemyStunnedRounds = 0;
    private int enemyStunSetRounds = 1;

    private bool playerStunned = false;
    private int playerStunnedRounds = 0;

    private bool manipulatedSpawnRates = false;
    private int manipulatedSpawnRatesRounds = 0;
    private bool rIncreaseRate = false;
    private bool gIncreaseRate = false;
    private bool bIncreaseRate = false;
    private bool specialPieceDecreaseRate = false;

    //enemy stats
    public float enemyMaxHp = 100;
    private float enemyCurrentHp = 100;
    private float enemyDmg = 10;
    private float enemyAttackInterval = 1.5f;
    private float enemyAttackTick = 0.0f;

    private int bossExtraAttackRoundTrigger = 4;
    private int bossExtraAttackRoundCurrent = 0;
    private float bossExtraAttackAccumulated = 0;
    private float bossExtraAttackPerStack = 25;

    private float selfHurtDamage = 0.0f;

    public int gameState = 0;
    public bool hasEnded = false;
    //board rows and columns
    private int xDimension = 7;
    private int yDimension = 4;

    public static bool isRigged = false;
    public static int enemyLevel = 0;
    public static bool is2ndLastLevel = false;
    public static int dialogueIndexFor2ndLast = 0;
    public static bool isFinalLevel = false;
    public static int dialogueIndexForFinal = 0;
    public static bool isPuzzleDone = false;

    public static float initTextPosY = 0;
    //tutorial
    public static bool isTutorial = false;
    public static int tutorialPhase = 0;

    /*tutorial phases:
     * 0 = start
     * 1 = connect 3 blues
     * 2 = dialogue "connect more pieces and deal more damage"
     * new 3 = dialogue "charge mechanic"
     * 3 = connect 4 normal piece and 1 special piece
     * 4 = dialogue take damage
     * 5 = show exit ui
     */
    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }

    void Start()
    {
        lockedHexes = new List<GameObject>();
        //debug
        Debug.Log("c1 index: " + Values.Player.equippedChar1.index + ", c2 Index: " + Values.Player.equippedChar2.index + ", c3 Index: " + Values.Player.equippedChar3.index);

        //setting up stats accoroding to values.cs
        isTutorial = Values.Puzzle.isTutorial;
        isRigged = Values.Puzzle.isRigged;
        enemyLevel = Values.Enemy.enemyLevel;
        is2ndLastLevel = Values.Puzzle.is2ndLastLevel;
        isFinalLevel = Values.Puzzle.isFinalLevel;
        dialogueIndexFor2ndLast = 0;
        isPuzzleDone = false;

        PuzzleUIManager.Instance.charDialogue1.gameObject.GetComponentInChildren<Text>().color = Color.white;
        PuzzleUIManager.Instance.charDialogue2.gameObject.GetComponentInChildren<Text>().color = Color.white;
        PuzzleUIManager.Instance.charDialogue3.gameObject.GetComponentInChildren<Text>().color = Color.white;
        if (isFinalLevel)
        {
            PuzzleUIManager.Instance.AllGameHUD.SetActive(false);
            PuzzleUIManager.Instance.FadeToBlackPanel.SetActive(true);
            PuzzleUIManager.Instance.Text.SetActive(true);

            PuzzleUIManager.Instance.FadeToBlackColor = PuzzleUIManager.Instance.FadeToBlackPanel.GetComponent<Image>().color;
            PuzzleUIManager.Instance.FadeToBlackColor.a = 0f;
            PuzzleUIManager.Instance.Text.GetComponent<Text>().text = "";
            initTextPosY =  PuzzleUIManager.Instance.Text.GetComponent<RectTransform>().position.y;

        }
        if (!isTutorial)
        {
            //setup game
            c1Index = Values.Player.equippedChar1.index;
            c1MaxHp = Values.Player.equippedChar1.hp;
            c1CurrentHp = c1MaxHp;
            c2Index = Values.Player.equippedChar2.index;
            c2MaxHp = Values.Player.equippedChar2.hp;
            c2CurrentHp = c2MaxHp;
            c3Index = Values.Player.equippedChar3.index;
            c3MaxHp = Values.Player.equippedChar3.hp;
            c3CurrentHp = c3MaxHp;
            PuzzleUIManager.Instance.charDialogue1.text = Values.Player.equippedChar1.catchPhrase;
            PuzzleUIManager.Instance.charDialogue2.text = Values.Player.equippedChar2.catchPhrase;
            PuzzleUIManager.Instance.charDialogue3.text = Values.Player.equippedChar3.catchPhrase;

            enemyMaxHp = Values.Enemy.maxHP;
            enemyCurrentHp = enemyMaxHp;
            enemyDmg = Values.Enemy.dmg;
            enemyAttackInterval = Values.Enemy.attackInterval;

            basicDamage = Values.Player.basicDamage;
            enhancedDamageMultiplier = Values.Player.enhancedDamageMultiplier;
            enemyStunSetRounds = Values.Player.setStunAmount;

            bossExtraAttackPerStack = Values.Puzzle.BlackHexBurstDamage;
            selfHurtDamage = Values.Puzzle.PainHexPosionDamage;

            PuzzleUIManager.Instance.SetEnemyBossSprite(Values.Enemy.enemyLevel);
            if ((Values.Enemy.enemyLevel == 1 && !isRigged) || (Values.Enemy.enemyLevel == 2 && isRigged))
            {
                FindObjectOfType<CharacterPortraitHolder>().ChangeRed(false);
                FindObjectOfType<CharacterPortraitHolder>().ChangeBlue(true);
                FindObjectOfType<CharacterPortraitHolder>().ChangeGreen(false);
                FindObjectOfType<CharacterPortraitHolder>().ChangeSkillBarColor();
            }
            else if ((Values.Enemy.enemyLevel == 2 && !isRigged) || (Values.Enemy.enemyLevel == 3 && isRigged))
            {
                FindObjectOfType<CharacterPortraitHolder>().ChangeRed(false);
                FindObjectOfType<CharacterPortraitHolder>().ChangeBlue(true);
                FindObjectOfType<CharacterPortraitHolder>().ChangeGreen(true);
                FindObjectOfType<CharacterPortraitHolder>().ChangeSkillBarColor();
            }
            else if ((Values.Enemy.enemyLevel == 3 && !isRigged) || (Values.Enemy.enemyLevel == 5 || Values.Enemy.enemyLevel == 5))
            {
                FindObjectOfType<CharacterPortraitHolder>().ChangeRed(true);
                FindObjectOfType<CharacterPortraitHolder>().ChangeBlue(true);
                FindObjectOfType<CharacterPortraitHolder>().ChangeGreen(true);
                FindObjectOfType<CharacterPortraitHolder>().ChangeSkillBarColor();
            }
            else
            {
                FindObjectOfType<CharacterPortraitHolder>().ChangeRed(false);
                FindObjectOfType<CharacterPortraitHolder>().ChangeBlue(false);
                FindObjectOfType<CharacterPortraitHolder>().ChangeGreen(false);
                FindObjectOfType<CharacterPortraitHolder>().ChangeSkillBarColor();
            }

        }
        else
        {
            PuzzleUIManager.Instance.SetEnemyBossSprite(0);
        }
        

        if (!isTutorial)
            nBoard = InitializeBoard();

        else
        {
            SetupTutorialBoard();
            tutorialPhase = 1;
        }

        
    }

    //manually set up board for tutorial
    private void SetupTutorialBoard()
    {
        nBoard = new int[xDimension, yDimension];
        gBoard = new GameObject[xDimension, yDimension];
        {
            {
                GameObject newPiece = createPiece(0, 0, 0);
                nBoard[0, 0] = 0;
                gBoard[0, 0] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 1, 0);
                nBoard[1, 0] = 1;
                gBoard[1, 0] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 2, 0);
                nBoard[2, 0] = 2;
                gBoard[2, 0] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 3, 0);
                nBoard[3, 0] = 0;
                gBoard[3, 0] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 4, 0);
                nBoard[4, 0] = 1;
                gBoard[4, 0] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 5, 0);
                nBoard[5, 0] = 2;
                gBoard[5, 0] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 6, 0);
                nBoard[6, 0] = 0;
                gBoard[6, 0] = newPiece;
            }
        }

        {
            {
                GameObject newPiece = createPiece(1, 0, 1);
                nBoard[0, 1] = 1;
                gBoard[0, 1] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 1, 1);
                nBoard[1, 1] = 2;
                gBoard[1, 1] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 2, 1);
                nBoard[2, 1] = 0;
                gBoard[2, 1] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 3, 1);
                nBoard[3, 1] = 2;
                gBoard[3, 1] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 4, 1);
                nBoard[4, 1] = 1;
                gBoard[4, 1] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 5, 1);
                nBoard[5, 1] = 1;
                gBoard[5, 1] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 6, 1);
                nBoard[6, 1] = 2;
                gBoard[6, 1] = newPiece;
            }
        }

        {
            {
                GameObject newPiece = createPiece(0, 0, 2);
                nBoard[0, 2] = 0;
                gBoard[0, 2] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 1, 2);
                nBoard[1, 2] = 1;
                gBoard[1, 2] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 2, 2);
                nBoard[2, 2] = 2;
                gBoard[2, 2] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 3, 2);
                nBoard[3, 2] = 0;
                gBoard[3, 2] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 4, 2);
                nBoard[4, 2] = 1;
                gBoard[4, 2] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 5, 2);
                nBoard[5, 2] = 2;
                gBoard[5, 2] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 6, 2);
                nBoard[6, 2] = 0;
                gBoard[6, 2] = newPiece;
            }
        }

        {
            {
                GameObject newPiece = createPiece(1, 0, 3);
                nBoard[0, 3] = 1;
                gBoard[0, 3] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 1, 3);
                nBoard[1, 3] = 2;
                gBoard[1, 3] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 2, 3);
                nBoard[2, 3] = 0;
                gBoard[2, 3] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 3, 3);
                nBoard[3, 3] = 1;
                gBoard[3, 3] = newPiece;
            }

            {
                GameObject newPiece = createPiece(2, 4, 3);
                nBoard[4, 3] = 2;
                gBoard[4, 3] = newPiece;
            }

            {
                GameObject newPiece = createPiece(0, 5, 3);
                nBoard[5, 3] = 0;
                gBoard[5, 3] = newPiece;
            }

            {
                GameObject newPiece = createPiece(1, 6, 3);
                nBoard[6, 3] = 1;
                gBoard[6, 3] = newPiece;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHpBars();
        UpdatePlayerStatusEffects();
        //Debug.Log(isTutorial);

        if (isTutorial)
        {
            UpdateHelpDialogue();

            if (tutorialPhase == 3)
            {
                if (c1CurrentHp == c1MaxHp)
                    c1CurrentHp -= enemyDmg;


                if (Input.GetMouseButtonUp(0))
                {
                    tutorialPhase = 4;
                }

            }

            else if (tutorialPhase == 3 && Input.GetMouseButtonUp(0))
            {
                tutorialPhase = 4;
            }

            else if (tutorialPhase == 4 && Input.GetMouseButtonUp(0))
            {
                tutorialPhase = 5;

            }

            else if (tutorialPhase == 6 && Input.GetMouseButtonUp(0))
            {
                tutorialPhase = 7;
                c1CurrentHp -= enemyDmg;
            }

            else if (tutorialPhase == 7 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
        }

        else
        {
            if (!is2ndLastLevel)
            {
                UpdateCatchphraseDialogue();
            }
            
            UpdateGameState();
        }


        enemyAttackTick += Time.deltaTime;
        if (enemyAttackTick >= enemyAttackInterval)
        {
            if (!isTutorial)
            {
                if (!enemyStunned)
                {
                    EnemyPerformAttack();
                    EnemyPerformSkill();
                }

                else
                {
                    Debug.Log("Enemy is stunned and cannot attack");
                    enemyStunnedRounds--;
                    PuzzleUIManager.Instance.stunCounter.text = enemyStunnedRounds.ToString();

                    if (enemyStunnedRounds == 0)
                    {
                        enemyStunned = false;
                        PuzzleUIManager.Instance.stunIndicator.SetActive(false);
                        PuzzleUIManager.Instance.stunIndicator.transform.rotation = Quaternion.identity;
                    }    
                        

                }


                enemyAttackTick = 0.0f;
            }
        }
        if (enemyLevel == 1)
            PuzzleUIManager.Instance.painHexTriggerBar.fillAmount = (float)bossExtraAttackRoundCurrent / bossExtraAttackRoundTrigger;
        if (enemyLevel == 3)
            PuzzleUIManager.Instance.lockHexTransferBar.fillAmount = (float)((currentTurn % 3)) / 2;

        Debug.Log(bossExtraAttackRoundCurrent + "/" + bossExtraAttackRoundTrigger);
        if (enemyStunned)
        {
            Vector3 stunIndicatorRot = PuzzleUIManager.Instance.stunIndicator.transform.rotation.eulerAngles;
            PuzzleUIManager.Instance.stunIndicator.transform.rotation = Quaternion.Euler(stunIndicatorRot.x, stunIndicatorRot.y, stunIndicatorRot.z + (150 *Time.deltaTime));
        }

        

        if (Input.GetMouseButtonUp(0))   //if left mouse button released
        {
            if(!isTutorial)
            {
                if (isFinalLevel)
                {
                    if (dialogueIndexForFinal >= FindObjectOfType<StoryManager>()
                            .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                            .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].sentences.Length - 5)
                    {

                        PuzzleUIManager.Instance.Text.GetComponent<Text>().text =
                            FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue]
                                .sentences[dialogueIndexForFinal];

                        int RisingValue = FindObjectOfType<StoryManager>()
                            .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                            .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].sentences.Length / 2;

                        float currentTextPosition = initTextPosY * 5;
                        float increaseValue = currentTextPosition / RisingValue;
                        Vector3 increaseVector = new Vector3(0, increaseValue, 0);

                        float currentAlpha = 1;
                        float increaseAlphaValue = currentAlpha / RisingValue;
                        Color increaseAlpha = new Color(0, 0, 0, increaseAlphaValue);


                        if (dialogueIndexForFinal >= RisingValue)
                        {
                            PuzzleUIManager.Instance.Text.GetComponent<RectTransform>().position += increaseVector;
                            PuzzleUIManager.Instance.FadeToBlackPanel.GetComponent<Image>().color += increaseAlpha;
                        }

                    }
                    else
                    {
                        Debug.Log("dialogueIndexForFinal:" + dialogueIndexForFinal);
                        StartCoroutine(WaitAnimation());
                    }
                }
                if (GameManager.Instance.selected.Count >= 3)   //if more than 3 pieces selected
                {
                    GameManager.Instance.Attack();  //delete normal pieces and trigger special pieces effects
                    GameManager.Instance.InstantRefreshBoard(); //replace deleted pieces

                    
                }

                else
                {
                    UnselectAllPieces();
                }
                
            }

            else
            {
                
                Debug.Log("true2");
                if (GameManager.Instance.selected.Count < 3)   //if more than 3 pieces selected
                {
                    foreach (GameObject selectedPiece in GameManager.Instance.selected) //for each selected piece
                    {
                        //unselect
                        //Debug.Log("before: " + GameManager.Instance.selected.Count);
                        Destroy(selectedPiece.transform.GetChild(0).gameObject);


                        if (selectedPiece.GetComponent<PieceBehavior>().ID >= 3)
                        {
                            GameManager.Instance.powerups.Remove(selectedPiece);
                        }

                        if (selectedPiece.GetComponent<LineRenderer>() != null)
                        {
                            selectedPiece.GetComponent<LineRenderer>().positionCount = 0;
                        }

                        selectedPiece.transform.localScale /= 1.10f;
                        //Debug.Log("after: " + GameManager.Instance.selected.Count);

                    }

                    //clear slected list
                    GameManager.Instance.selected.Clear();
                }

                else if (GameManager.tutorialPhase == 1 && GameManager.Instance.selected.Count == 3)
                {
                    GameManager.tutorialPhase = 2;
                    GameManager.Instance.Attack();
                    GameManager.Instance.InstantRefreshBoard();
                    c2CurrentCharge = c2MaxCharge;
                    PuzzleUIManager.Instance.c2ChargeBar.fillAmount = c2MaxCharge / c2CurrentCharge;
                    Debug.Log("true3");
                }

                else if (GameManager.tutorialPhase == 5 && GameManager.Instance.selected.Count >= 5)
                {
                    GameManager.tutorialPhase = 6;
                    GameManager.Instance.Attack();
                    GameManager.Instance.InstantRefreshBoard();
                }

                else
                {
                    UnselectAllPieces();
                }
            }
            
        }
    }

    private static void UnselectAllPieces()
    {
        foreach (GameObject selectedPiece in GameManager.Instance.selected) //for each selected piece
        {
            //unselect
            //Debug.Log("before: " + GameManager.Instance.selected.Count);
            Destroy(selectedPiece.transform.GetChild(0).gameObject);


            if (selectedPiece.GetComponent<PieceBehavior>().ID >= 3)
            {
                GameManager.Instance.powerups.Remove(selectedPiece);
            }

            if (selectedPiece.GetComponent<LineRenderer>() != null)
            {
                selectedPiece.GetComponent<LineRenderer>().positionCount = 0;
            }

            selectedPiece.transform.localScale /= 1.10f;


            //Debug.Log("after: " + GameManager.Instance.selected.Count);
        }

        //clear slected list
        GameManager.Instance.selected.Clear();
        //if (GameManager.Instance.selected.Count == 1)
        //{
        //    Destroy(GameManager.Instance.selected[0].transform.GetChild(0).gameObject);
        //    GameManager.Instance.selected.Remove(GameManager.Instance.selected[0]);


        //    if (GameManager.Instance.selected[0].GetComponent<PieceBehavior>().ID >= 3)
        //    {
        //        GameManager.Instance.powerups.Remove(GameManager.Instance.selected[0]);
        //    }

        //    GameManager.Instance.InstantRefreshBoard();
        //}

        //undo transparency effect
        GameManager.Instance.InstantRefreshBoard();
    }

    private void UpdateGameState()
    {
        if (c1CurrentHp <= 0 && c2CurrentHp <= 0 && c3CurrentHp <= 0)
            gameState = -1;

        else if (enemyCurrentHp <= 0)
        {
            gameState = 1;
        }


        if (gameState != 0 && hasEnded == false)
        {
            if (gameState == 1)
            {
                OnWin();
            }

            else if (gameState == -1)
            {
                OnLose();
            }

            hasEnded = true;
        }

        if (isPuzzleDone)
        {
            SceneManager.LoadScene(Values.SceneNames.BedroomScene);
            FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
            FindObjectOfType<StoryManager>().currentDialogue = 0;
            FindObjectOfType<StoryManager>().currentChapter = 5;
        }

        if (hasEnded && gameState == 1)
        {
            if (enemyLevel == 1 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (enemyLevel == 2 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (enemyLevel == 3 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (enemyLevel == 0 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (enemyLevel == 5 && Input.GetMouseButtonUp(0)) 
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }

        }
        else if (hasEnded && gameState == -1)
        {
            if (isRigged && enemyLevel == 1 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (!isRigged && enemyLevel == 1 && Input.GetMouseButtonUp(0)) //try again
            {
                SceneManager.LoadScene(Values.SceneNames.PuzzleScene);
            }

            if (isRigged && enemyLevel == 2 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (!isRigged && enemyLevel == 2 && Input.GetMouseButtonUp(0)) //try again
            {
                SceneManager.LoadScene(Values.SceneNames.PuzzleScene);
            }

            if (isRigged && enemyLevel == 3 && Input.GetMouseButtonUp(0))
            {
                SceneManager.LoadScene(Values.SceneNames.BedroomScene);
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                FindObjectOfType<StoryManager>().currentDialogue++;
            }
            else if (!isRigged && enemyLevel == 3 && Input.GetMouseButtonUp(0)) //try again
            {
                SceneManager.LoadScene(Values.SceneNames.PuzzleScene);
            }

            if (!isRigged && enemyLevel == 0 && Input.GetMouseButtonUp(0)) //try again
            {
                SceneManager.LoadScene(Values.SceneNames.PuzzleScene);
            }

            if (!isRigged && enemyLevel == 5 && Input.GetMouseButtonUp(0)) //try again
            {
                SceneManager.LoadScene(Values.SceneNames.PuzzleScene);
            }
        }
    }

    private void EnemyPerformSkill()
    {
        bool performSkill = false;
        if (UnityEngine.Random.Range(0, 5) == 4)
            performSkill = true;

        if(performSkill)
        {
            Debug.Log("Performing enemy skill");
            if (Values.Enemy.skill == Values.Enemy.SkillType.Burst)
            {
                int charToBurst = UnityEngine.Random.Range(0, 2);
                switch (charToBurst)
                {

                    case 0: // prioritize attack on char1
                        {
                            if (c1CurrentHp > 0)
                                c1CurrentHp -= enemyDmg * 3;

                            else if (c2CurrentHp > 0)
                                c2CurrentHp -= enemyDmg * 3;

                            else if (c3CurrentHp > 0)
                                c3CurrentHp -= enemyDmg * 3;
                        }
                        break;
                    case 1: // prioritize attack on char2
                        {
                            if (c2CurrentHp > 0)
                                c2CurrentHp -= enemyDmg * 3;

                            else if (c3CurrentHp > 0)
                                c3CurrentHp -= enemyDmg * 3;

                            else if (c1CurrentHp > 0)
                                c1CurrentHp -= enemyDmg * 3;
                        }
                        break;
                    case 2: // prioritize attack on char3
                        {
                            if (c3CurrentHp > 0)
                                c3CurrentHp -= enemyDmg * 3;

                            else if (c1CurrentHp > 0)
                                c1CurrentHp -= enemyDmg * 3;

                            else if (c2CurrentHp > 0)
                                c2CurrentHp -= enemyDmg * 3;
                        }
                        break;
                }
            }

            else if (Values.Enemy.skill == Values.Enemy.SkillType.Freeze)
            {
                if (!playerIsFrozen)
                {
                    playerIsFrozen = true;
                    isBoardInteractable = false;
                    Debug.Log("Freeze player");
                    UnselectAllPieces();

                }

            }

            else if (Values.Enemy.skill == Values.Enemy.SkillType.Poison)
            {
                if (!playerIsPoisoned)
                {
                    playerIsPoisoned = true;
                    Debug.Log("Poision player");
                }

            }
        }

        else
        {
            Debug.Log("Not performing enemy skill");

        }

    }

    private void EnemyPerformAttack()
    {
        int charToAttack = UnityEngine.Random.Range(0, 2);
        switch (charToAttack)
        {

            case 0: // prioritize attack on char1
                {
                    if (c1CurrentHp > 0)
                        c1CurrentHp -= enemyDmg;

                    else if (c2CurrentHp > 0)
                        c2CurrentHp -= enemyDmg;

                    else if (c3CurrentHp > 0)
                        c3CurrentHp -= enemyDmg;
                }
                break;
            case 1: // prioritize attack on char2
                {
                    if (c2CurrentHp > 0)
                        c2CurrentHp -= enemyDmg;

                    else if (c3CurrentHp > 0)
                        c3CurrentHp -= enemyDmg;

                    else if (c1CurrentHp > 0)
                        c1CurrentHp -= enemyDmg;
                }
                break;
            case 2: // prioritize attack on char3
                {
                    if (c3CurrentHp > 0)
                        c3CurrentHp -= enemyDmg;

                    else if (c1CurrentHp > 0)
                        c1CurrentHp -= enemyDmg;

                    else if (c2CurrentHp > 0)
                        c2CurrentHp -= enemyDmg;
                }
                break;
        }

        if(PainHex != null)
        {
            if (bossExtraAttackRoundCurrent >= bossExtraAttackRoundTrigger)
            {
                c1CurrentHp = Mathf.Max(0, c1CurrentHp - bossExtraAttackAccumulated);
                c2CurrentHp = Mathf.Max(0, c1CurrentHp - bossExtraAttackAccumulated);
                c3CurrentHp = Mathf.Max(0, c1CurrentHp - bossExtraAttackAccumulated);
                bossExtraAttackRoundCurrent = 0;
            }

            bossExtraAttackAccumulated += bossExtraAttackPerStack;
            bossExtraAttackRoundCurrent++;
        }
        
    }

    private void UpdateHpBars()
    {
        if (PuzzleUIManager.Instance.enemyHpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.enemyHpBar.fillAmount = enemyCurrentHp / enemyMaxHp;
        }

        if (PuzzleUIManager.Instance.c1HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.c1HpBar.fillAmount = c1CurrentHp / c1MaxHp;
        }

        if (PuzzleUIManager.Instance.c2HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.c2HpBar.fillAmount = c2CurrentHp / c2MaxHp;
        }

        if (PuzzleUIManager.Instance.c3HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.c3HpBar.fillAmount = c3CurrentHp / c3MaxHp;
        }
    }

    private void UpdateChargeBars(int i)
    {
        if (selected[i].GetComponent<PieceBehavior>().ID == 0)
        {
            c1CurrentCharge += 10;

            if (c1CurrentCharge >= c1MaxCharge)
            {
                c1CurrentCharge = c1MaxCharge;
            }
        }

        if (selected[i].GetComponent<PieceBehavior>().ID == 1)
        {
            c2CurrentCharge += 10;

            if (c2CurrentCharge >= c2MaxCharge)
            {
                c2CurrentCharge = c2MaxCharge;
            }
        }

        if (selected[i].GetComponent<PieceBehavior>().ID == 2)
        {
            c3CurrentCharge += 10;

            if (c3CurrentCharge >= c3MaxCharge)
            {
                c3CurrentCharge = c3MaxCharge;
            }
        }


        if (PuzzleUIManager.Instance.c1ChargeBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.c1ChargeBar.fillAmount = c1CurrentCharge / c1MaxCharge;
        }

        if (PuzzleUIManager.Instance.c2ChargeBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.c2ChargeBar.fillAmount = c2CurrentCharge / c2MaxCharge;
        }

        if (PuzzleUIManager.Instance.c3ChargeBar != null)
        {
            //currentHP -= 0.25f;   //testing
            PuzzleUIManager.Instance.c3ChargeBar.fillAmount = c3CurrentCharge / c3MaxCharge;
        }
    }

    void UpdatePlayerStatusEffects()
    {
        if(playerIsFrozen)
        {
            if(playerFrozenTick > playerFrozenDuration)
            {
                playerIsFrozen = false;
                playerFrozenTick = 0.0f;
                isBoardInteractable = true;
                Debug.Log("Unfreeze player");
            }

            else
            {
                playerFrozenTick += Time.deltaTime;
            }
        }

        else if (playerIsPoisoned)
        {
            if (playerPoisonedTick > playerPoisonedDuration)
            {
                playerIsPoisoned = false;
                playerPoisonedTick = 0.0f;
                Debug.Log("Unpoison player");
            }

            else
            {
                playerPoisonedTick += Time.deltaTime;
                if (c1CurrentHp > 0) c1CurrentHp -= playerPoisonedDamage; c1CurrentHp = Mathf.Max(0.0f, c1CurrentHp);
                if (c2CurrentHp > 0) c2CurrentHp -= playerPoisonedDamage; c2CurrentHp = Mathf.Max(0.0f, c2CurrentHp);
                if (c3CurrentHp > 0) c3CurrentHp -= playerPoisonedDamage; c3CurrentHp = Mathf.Max(0.0f, c3CurrentHp);
            }
        }
    }


    // initialize randomization of board pieces
    int[,] InitializeBoard()
    {

        gBoard = new GameObject[xDimension, yDimension];

        int[,] newBoard = new int[xDimension, yDimension];

        for (int i = 0; i < xDimension; i++)
        {
            for(int j = 0; j < yDimension; j++)
            {
                GameObject newPiece;
                int n = UnityEngine.Random.Range(0, 7); //2nd parameter dictates special piece rng: higher = more rare

                if(n > 0 || specialPiecesCount >= 3)
                {
                    //create normal piece
                    n = UnityEngine.Random.Range(0, 3); //randomize between normal pieces index (0-2)
                    newPiece = createPiece(n, i, j);
                }

                else
                {
                    //Debug.Log("creating special piece");

                    if(specialPiece1 == null && c1CurrentHp > 0 && c1CurrentCharge == c1MaxCharge)   //if no specialPiece1 on board & character 1 is alive
                    {
                        //Debug.Log("creating special piece1");
                        n = c1Index;
                        newPiece = createPiece(n, i, j);
                        specialPiece1 = newPiece;
                        specialPiecesCount++;
                        
                    }

                    else if(specialPiece2 == null && c2CurrentHp > 0 && c2CurrentCharge == c2MaxCharge)  //if no specialPiece2 on board & character 2 is alive
                    {
                        //Debug.Log("creating special piece2");
                        n = c2Index;
                        newPiece = createPiece(n, i, j);
                        specialPiece2 = newPiece;
                        specialPiecesCount++;
                        c2CurrentCharge = 0;
                    }

                    else if (specialPiece3 == null && c3CurrentHp > 0 && c3CurrentCharge == c3MaxCharge) //if no specialPiece3 on board & character 3 is alive
                    {
                        //Debug.Log("creating special piece3");
                        n = c3Index;
                        newPiece = createPiece(n, i, j);
                        specialPiece3 = newPiece;
                        specialPiecesCount++;
                        c3CurrentCharge = 0;
                    }   

                    //int specialCharacterIndex = UnityEngine.Random.Range(0, 2); // 0-1

                    //switch(specialCharacterIndex)
                    //{
                    //    case 0: n = c1Index; break;
                    //    case 1: n = c2Index; break;
                    //}

                    //Debug.Log(specialCharacterIndex);

                    else
                    {
                        //Debug.Log("creating special piece4");
                        n = UnityEngine.Random.Range(0, 3);
                        newPiece = createPiece(n, i, j);
                    }
                }

                

                newBoard[i, j] = n;
                gBoard[i, j] = newPiece;
            }
        }

        for (int i = 0; i < Values.Puzzle.hexBlockerCount; i++)
        {

            int blockerXIndex = 0;
            int blockerYIndex = 0;

            do
            {
                blockerXIndex = UnityEngine.Random.Range(0, xDimension);
                blockerYIndex = UnityEngine.Random.Range(0, yDimension);
                Debug.Log(blockerXIndex + " " + blockerYIndex);
            } while (newBoard[blockerXIndex, blockerYIndex] == -2);
            

            Destroy(gBoard[blockerXIndex, blockerYIndex]);

            newBoard[blockerXIndex, blockerYIndex] = -2;

            GameObject hexBlockerObj = createNeutral(0, blockerXIndex, blockerYIndex);

            lockedHexes.Add(hexBlockerObj);
            gBoard[blockerXIndex, blockerYIndex] = hexBlockerObj;
        }


        return newBoard;
    }

    GameObject createPiece(int pieceIndex, int x, int y)
    {
        GameObject newPiece;

        if(x%2 == 0)
            newPiece = Instantiate(pieces[pieceIndex], new Vector3((0.2965f * x) -0.925f, (0.3225f * y) + 0.2875f, 0.0f), Quaternion.identity, null);

        else
            newPiece = Instantiate(pieces[pieceIndex], new Vector3((0.2965f * x) - 0.925f, (0.3225f * y) + 0.4490f, 0.0f), Quaternion.identity, null);

        if (newPiece.TryGetComponent(out PieceBehavior PB))
        {
            PB.SetValues(pieceIndex, x, y);
        }
            

        return newPiece;
    }

    GameObject createNeutral(int pieceIndex, int x, int y)
    {
        GameObject newPiece;

        if (x % 2 == 0)
            newPiece = Instantiate(neutrals[pieceIndex], new Vector3((0.2965f * x) - 0.925f, (0.3225f * y) + 0.2875f, 0.0f), Quaternion.identity, null);

        else
            newPiece = Instantiate(neutrals[pieceIndex], new Vector3((0.2965f * x) - 0.925f, (0.3225f * y) + 0.4490f, 0.0f), Quaternion.identity, null);

        if (newPiece.TryGetComponent(out PieceBehavior PB))
        {
            switch (pieceIndex)
            {
                case 0:
                {
                    PB.SetValues(-2, x, y);
                }
                    break;

                case 1:
                {
                    PB.SetValues(-3, x, y);
                }
                    break;

                case 2:
                {
                    PB.SetValues(-4, x, y);
                }
                    break;
            }
        }
            
        return newPiece;
    }

    public bool isWithinBounds(int x, int y)
    {
        return ((x >= 0 && x <= xDimension) && (y >= 0 && y <= yDimension));
    }
    
    //set piece value on integer board to -1 then delete correspnding game objects
    public void Attack()
    {
        for (int i = 0; i < selected.Count; i++)    //for all selected piece
        {
            if(selected[i].GetComponent<PieceBehavior>().ID < 3 && selected[i].GetComponent<PieceBehavior>().ID >= 0)    //if normal piece
            {
                //delete normal piece
                //Debug.Log("Deleting");
                if (selected[i].GetComponent<PieceBehavior>().ID == 0)
                {
                    currentDamageCounter += basicDamage * 1.0f;
                }

                if (selected[i].GetComponent<PieceBehavior>().ID == 1)
                {
                    currentHealCounter += 0.01f;
                    currentDamageCounter += basicDamage * 0.45f;

                }

                if (selected[i].GetComponent<PieceBehavior>().ID == 2)
                {
                    if (!enemyStunned)
                    {
                        PuzzleUIManager.Instance.stunIndicator.SetActive(true);
                        PuzzleUIManager.Instance.stunIndicator.transform.rotation = Quaternion.identity;

                        enemyStunned = true;
                    }

                    enemyStunnedRounds = Math.Min(enemyStunnedRounds + enemyStunSetRounds, 5);
                    PuzzleUIManager.Instance.stunCounter.text = enemyStunnedRounds.ToString();

                    currentDamageCounter += basicDamage * 0.3f;
                }


                int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                if(nBoard[xIndex, yIndex] != -1)
                {
                    UpdateChargeBars(i);

                    nBoard[xIndex, yIndex] = -1;
                }

            }

            else if(selected[i].GetComponent<PieceBehavior>().ID < 0)   //if special piece
            {
                //perform special piece effect
                PerformNeutralPieceEffects(i);
            }

            else    //if special piece
            {
                //perform special piece effect
                PerformSpecialPieceEffects(i);
            }
        }
        
        selected.Clear();   //clear reference list of selected pieces
        powerups.Clear();   //clear reference list of selected powerups
        DestroyDamagedPieces(); //destroy game object pieces that have been set to -1 value
        RearrangePieces();
        isBoardInteractable = false;

        AnimationManager.Instance.PlayHitAnimation();
        enemyCurrentHp = Math.Max(enemyCurrentHp - (currentDamageCounter * currentDamageMultiplier), 0);

        c1CurrentHp = Math.Min(c1CurrentHp + (c1MaxHp * (currentHealCounter * currentHealMultiplier)), c1MaxHp);
        c2CurrentHp = Math.Min(c2CurrentHp + (c2MaxHp * (currentHealCounter * currentHealMultiplier)), c2MaxHp);
        c3CurrentHp = Math.Min(c3CurrentHp + (c3MaxHp * (currentHealCounter * currentHealMultiplier)), c3MaxHp);

        currentDamageCounter = 0;
        currentDamageMultiplier = 1;
        currentHealCounter = 0;
        currentHealMultiplier = 1;
        

        if (is2ndLastLevel)
        {
            Update2ndLastLevelDialogue();
            dialogueIndexFor2ndLast++;
        }
        

        if (isFinalLevel)
        {
            UpdateFinalLevelDialogue();
            dialogueIndexForFinal++;
        }
        
        Debug.Log("Attack");

        currentTurn++;
        if (currentTurn % 3 == 0)
        {
            for (int i = 0; i < Values.Puzzle.hexBlockerCount; i++)
            {

                int newBlockerXIndex = 0;
                int newBlockerYIndex = 0;

                do
                {
                    newBlockerXIndex = UnityEngine.Random.Range(0, xDimension);
                    newBlockerYIndex = UnityEngine.Random.Range(0, yDimension);
                    Debug.Log(newBlockerXIndex + " " + newBlockerYIndex);
                } while (nBoard[newBlockerXIndex, newBlockerYIndex] == -2);


                Destroy(gBoard[newBlockerXIndex, newBlockerYIndex]);

                nBoard[newBlockerXIndex, newBlockerYIndex] = -2;

                GameObject newhexBlockerObj = createNeutral(0, newBlockerXIndex, newBlockerYIndex);
                gBoard[newBlockerXIndex, newBlockerYIndex] = newhexBlockerObj;




                int oldBlockerXIndex = lockedHexes[i].GetComponent<PieceBehavior>().x;
                int oldBlockerYIndex = lockedHexes[i].GetComponent<PieceBehavior>().y;
                GameObject oldHexBlockerObj = gBoard[oldBlockerXIndex, oldBlockerYIndex];
                Destroy(oldHexBlockerObj);


                int n = UnityEngine.Random.Range(0, 3);
                GameObject newPiece = createPiece(n, oldBlockerXIndex, oldBlockerYIndex);

                nBoard[oldBlockerXIndex, oldBlockerYIndex] = n;
                gBoard[oldBlockerXIndex, oldBlockerYIndex] = newPiece;

                lockedHexes[i] = newhexBlockerObj;

            }
        }
    }

    

    private void PerformSpecialPieceEffects(int i)
    {
        GameObject specialPiece = selected[i];

        if(specialPiece.GetComponent<PieceBehavior>().ID == c1Index)
        {
            specialPiece1 = null;
            specialPiecesCount--;
            c1CurrentCharge = 0;
            UpdateChargeBars(i);

            if (!isTutorial)
            {
                PuzzleUIManager.Instance.charDialogue1.gameObject.SetActive(true);
                PuzzleUIManager.Instance.charDialogue2.gameObject.SetActive(false);
                PuzzleUIManager.Instance.charDialogue3.gameObject.SetActive(false);
            }

        }

        else if (specialPiece.GetComponent<PieceBehavior>().ID == c2Index)
        {
            specialPiece2 = null;
            specialPiecesCount--;
            c2CurrentCharge = 0;
            UpdateChargeBars(i);

            if (!isTutorial)
            {
                PuzzleUIManager.Instance.charDialogue2.gameObject.SetActive(true);
                PuzzleUIManager.Instance.charDialogue1.gameObject.SetActive(false);
                PuzzleUIManager.Instance.charDialogue3.gameObject.SetActive(false);
            }

        }

        else if (specialPiece.GetComponent<PieceBehavior>().ID == c3Index)
        {
            Debug.Log("delete 3");
            specialPiece3 = null;
            specialPiecesCount--;
            c3CurrentCharge = 0;
            UpdateChargeBars(i);

            if (!isTutorial)
            {
                PuzzleUIManager.Instance.charDialogue3.gameObject.SetActive(true);
                PuzzleUIManager.Instance.charDialogue2.gameObject.SetActive(false);
                PuzzleUIManager.Instance.charDialogue1.gameObject.SetActive(false);
            }

        }

        //else if (specialPiece.GetComponent<PieceBehavior>().ID == c3Index)
        //{
        //    specialPiece3 = null;
        //}

        switch (specialPiece.GetComponent<PieceBehavior>().ID)
        {
            case 3: //clear row of powerup piece
                {
                    //-----------patterned clearing-----------\\
                    //int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    //for (int j = 0; j < xDimension; j++)
                    //{
                    //    nBoard[j, yIndex] = -1;
                    //}
                    //--------------------------------------------\\



                    //-----------deal huge damage to boss-----------\\
                    Debug.Log("Dealing enhanced damage to enemy");
                    currentDamageMultiplier = enhancedDamageMultiplier;
                    int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    nBoard[xIndex, yIndex] = -1;
                    //--------------------------------------------\\
                }
                break;

            case 4: //clear pieces around powerup piece
                {
                    //-----------patterned clearing-----------\\
                    //int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    //int yIndex = selected[i].GetComponent<PieceBehavior>().y;

                    //nBoard[xIndex, yIndex] = -1;

                    //if(xIndex - 1 >= 0)
                    //{
                    //    nBoard[xIndex - 1, yIndex] = -1;

                    //    if (yIndex + 1 < yDimension)
                    //        nBoard[xIndex - 1, yIndex + 1] = -1;
                    //}

                    //if(xIndex + 1 < xDimension)
                    //{
                    //    nBoard[xIndex + 1, yIndex] = -1;

                    //    if (yIndex + 1 < yDimension)
                    //        nBoard[xIndex + 1, yIndex + 1] = -1;
                    //}
                    //--------------------------------------------\\


                    //nBoard[xIndex, yIndex] = -1;
                    //nBoard[xIndex, yIndex] = -1;
                    //nBoard[xIndex, yIndex] = -1;



                    //-----------Heal allies for %hp-----------\\

                    int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    nBoard[xIndex, yIndex] = -1;

                    currentHealMultiplier = 5;

                    Debug.Log("Healing allies");

                }
                break;

            case 5: //clear column of powerup piece
                {
                    //-----------patterned clearing-----------\\
                    //int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    //for (int j = 0; j < yDimension; j++)
                    //{
                    //    nBoard[xIndex, j] = -1;
                    //}
                    //--------------------------------------------\\


                    //-----------UnityEngine.Random piece rate boost-----------\\
                    //Debug.Log("Boost spawn rate effect");

                    //int pieceBoostRateIndex = UnityEngine.Random.Range(0, 3);

                    //if (pieceBoostRateIndex == 0)
                    //    rIncreaseRate = true;

                    //else if (pieceBoostRateIndex == 1)
                    //    gIncreaseRate = true;

                    //else if (pieceBoostRateIndex == 2)
                    //    bIncreaseRate = true;

                    //manipulatedSpawnRates = true;
                    //manipulatedSpawnRatesRounds = 5;

                    //int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    //int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    //nBoard[xIndex, yIndex] = -1;
                    //--------------------------------------------\\

                    //-----------stun boss-----------\\

                    Debug.Log("Disable next enemy attack (stunned)");

                    if (!enemyStunned)
                    {
                        PuzzleUIManager.Instance.stunIndicator.SetActive(true);
                        PuzzleUIManager.Instance.stunIndicator.transform.rotation = Quaternion.identity;
                        enemyStunned = true;
                    }
                        

                    enemyStunnedRounds = Math.Min(enemyStunnedRounds + enemyStunSetRounds + 3, 5);
                    currentDamageCounter += basicDamage * 1.0f;
                    PuzzleUIManager.Instance.stunCounter.text = enemyStunnedRounds.ToString();


                    int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    nBoard[xIndex, yIndex] = -1;
                    //--------------------------------------------\\



                }


                break;
            case 6:
                {
                    //-----------patterned clearing-----------\\
                    //int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    //int yIndex = selected[i].GetComponent<PieceBehavior>().y;

                    //nBoard[xIndex, yIndex] = -1;

                    //if (xIndex+1 <= xDimension - 1 && yIndex+1 <= yDimension - 1)
                    //{
                    //    nBoard[xIndex + 1, yIndex + 1] = -1;
                    //}

                    //if(xIndex + 2 <= xDimension - 1&& yIndex + 2 <= yDimension - 1)
                    //{
                    //    nBoard[xIndex + 2, yIndex + 2] = -1;
                    //}

                    //if (xIndex - 1 > 0 && yIndex - 1 > 0)
                    //{
                    //    nBoard[xIndex - 1, yIndex - 1] = -1;
                    //}

                    //if (xIndex - 2 > 0 && yIndex - 2 > 0)
                    //{
                    //    nBoard[xIndex - 2, yIndex -  2] = -1;
                    //}

                    //--------------------------------------------\\


                    //-----------Heal allies for %hp-----------\\

                    int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    nBoard[xIndex, yIndex] = -1;

                    if (c1CurrentHp > 0)
                    {
                        c1CurrentHp += (c1MaxHp * 0.075f);

                        if (c1CurrentHp > c1MaxHp)
                        {
                            c1CurrentHp = c1MaxHp;
                        }

                    }

                    if (c2CurrentHp > 0)
                    {
                        c2CurrentHp += (c2MaxHp * 0.075f);

                        if (c2CurrentHp > c2MaxHp)
                        {
                            c2CurrentHp = c2MaxHp;
                        }

                    }

                    if (c3CurrentHp > 0)
                    {
                        c3CurrentHp += (c3MaxHp * 0.075f);

                        if (c3CurrentHp > c3MaxHp)
                        {
                            c3CurrentHp = c3MaxHp;
                        }

                    }

                    Debug.Log("Healing allies");


                }
                break;
        }


    }

    private void PerformNeutralPieceEffects(int i)
    {
        GameObject neutralPiece = selected[i];

        switch (neutralPiece.GetComponent<PieceBehavior>().ID)
        {
            case -3:
            {
                int charToSelfHurt = -1;
                if (neutralPiece.GetComponent<PieceBehavior>().ID <= 0)
                {
                    charToSelfHurt = i % 3;
                    if (charToSelfHurt == 0)
                    {
                        c1CurrentHp = Mathf.Max(c1CurrentHp - 100, 0);
                    }

                    else if (charToSelfHurt == 1)
                    {
                        c2CurrentHp = Mathf.Max(c2CurrentHp - 100, 0);
                    }

                    else if (charToSelfHurt == 2)
                    {
                        c3CurrentHp = Mathf.Max(c3CurrentHp - 100, 0);
                    }

                    Debug.Log("chartohurt:" + charToSelfHurt);
                }
                int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                int yIndex = selected[i].GetComponent<PieceBehavior>().y;

                nBoard[xIndex, yIndex] = -1;
                }
                break;

            case -4:
            {
                ResetPainHexDamageCounter();
                int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                int yIndex = selected[i].GetComponent<PieceBehavior>().y;

                nBoard[xIndex, yIndex] = -1;
                }
                break;
        }

        
    }

    //destroy pieces from game object board where equivalent index in integer board is -1
    public void DestroyDamagedPieces()
    {
        for (int i = xDimension-1; i >= 0; i--)
        {
            for (int j = yDimension-1; j >= 0; j--)
            {
                
                if (nBoard[i, j] == -1)
                {
                    
                    Destroy(gBoard[i, j].gameObject);
                    gBoard[i, j] = null;

                    
                    
                    //int k = j;
                    //int l = j;

                    //while(nBoard[i, l] == -1 || nBoard[i, k] != -2)
                    //{
                    //    if (l - 1 == -1)
                    //    {
                    //        break;
                    //    }

                    //    l = l - 1;

                        
                            
                    //}

                    ////while(l >= 0)
                    ////{
                    //    //Debug.Log(k - (k - l));

                    //    if (nBoard[i, k - (k - l)] != -1 && nBoard[i, k - (k - l)] != -2)
                    //    {
                    //        nBoard[i, k] = nBoard[i, k - (k - l)];
                    //        gBoard[i, k] = gBoard[i, k - (k - l)];
                    //        gBoard[i, k].GetComponent<PieceBehavior>().y = k;

                    //        if (gBoard[i, k].GetComponent<PieceBehavior>().x % 2 == 0)
                    //    {
                    //        Vector3 currentPos = gBoard[i, k].transform.position;
                    //        Vector3 newPos = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.2875f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                    //        gBoard[i, k].GetComponent<PieceBehavior>().MoveUp(currentPos, newPos);
                    //    }
                    //            //gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.2875f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                    //        else
                    //    {
                    //        Vector3 currentPos = gBoard[i, k].transform.position;
                    //        Vector3 newPos = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.4490f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                    //        gBoard[i, k].GetComponent<PieceBehavior>().MoveUp(currentPos, newPos);
                    //    }
                    //    //gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.4490f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);

                    //        nBoard[i, k - (k - l)] = -1;
                    //        gBoard[i, k - (k - l)] = null;
                    //    }
                        
                        

                        //k--;
                        //l--;
                    //} 

                    //for(int m = (k-l); m >= 0; m--)
                    //{
                    //    Debug.Log(k - l);
                    //    nBoard[i, m] = -1;
                    //    gBoard[i, m] = null;
                    //}
                    
                }
            }
        }
    }

    public void RearrangePieces()
    {
        for (int i = xDimension - 1; i >= 0; i--)
        {
            for (int j = yDimension - 1; j >= 0; j--)
            {

                if (gBoard[i, j] == null && nBoard[i,j] != -2)
                {
                    int k = j;
                    while(k > 0)
                    {
                        k--;
                        if (gBoard[i, k] != null && gBoard[i, k].GetComponent<PieceBehavior>().ID != -2)
                        {
                            gBoard[i, j] = gBoard[i, k];
                            gBoard[i, k] = null;
                            nBoard[i, j] = nBoard[i, k];
                            nBoard[i, k] = -1;

                            gBoard[i, j].GetComponent<PieceBehavior>().y = j;

                            if (gBoard[i, j].GetComponent<PieceBehavior>().x % 2 == 0)
                            {
                                Vector3 currentPos = gBoard[i, j].transform.position;
                                Vector3 newPos = new Vector3(gBoard[i, j].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, j].GetComponent<PieceBehavior>().y) + 0.2875f, gBoard[i, j].GetComponent<PieceBehavior>().transform.position.z);
                                gBoard[i, j].GetComponent<PieceBehavior>().MoveUp(currentPos, newPos);
                            }
                            //gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.2875f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                            else
                            {
                                Vector3 currentPos = gBoard[i, j].transform.position;
                                Vector3 newPos = new Vector3(gBoard[i, j].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, j].GetComponent<PieceBehavior>().y) + 0.4490f, gBoard[i, j].GetComponent<PieceBehavior>().transform.position.z);
                                gBoard[i, j].GetComponent<PieceBehavior>().MoveUp(currentPos, newPos);
                            }
                            //gBoard[
                            break;
                        }
                    }
                    if (j > 0)
                    {
                        
                    }

                    

                }
            }
        }
    }

    //replace damaged pieces and undo transparency of pieces on board after a delay
    public IEnumerator DelayedRefreshBoard()
    {
        
        yield return new WaitForSeconds(0.5f);
        Debug.Log("in delay spawn func");
        List<GameObject> generatedPieces = new List<GameObject>();
        List<GameObject> toDelete = new List<GameObject>();

        for(int i = 0; i < xDimension; i++)
        {
            for(int j = 0; j < yDimension; j++)
            {
                if(nBoard[i, j] == -1)
                {
                    int n = UnityEngine.Random.Range(0, 7);

                    if (n > 0)
                    {
                        n = UnityEngine.Random.Range(0, 3);
                    }

                    else
                    {
                        n = UnityEngine.Random.Range(3, 5);
                    }

                    GameObject newPiece = createPiece(n, i, j);

                    nBoard[i, j] = n;
                    gBoard[i, j] = newPiece;

                    generatedPieces.Add(newPiece);
                }
            }
        }

        //if(tutorialPhase == 3)
        //{
        //    Destroy(gBoard[3, 0]);
        //    GameObject newPiece1 = createPiece(3, 3, 0);

        //    nBoard[3, 0] = 3;
        //    gBoard[3, 0] = newPiece1;


        //    generatedPieces.Add(newPiece1);


        //    Destroy(gBoard[2, 0]);
        //    GameObject newPiece2 = createPiece(1, 2, 0);

        //    nBoard[2, 0] = 1;
        //    gBoard[2, 0] = newPiece2;



        //    generatedPieces.Add(newPiece2);
        //}

        

        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in allPieces)
        {
            
            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
            currentColor.a = 1.0f;
            piece.GetComponent<SpriteRenderer>().color = currentColor;
            
        }

        List<GameObject> identicalPieces = new List<GameObject>();
        foreach (GameObject newPiece in generatedPieces)
        {
            identicalPieces.Add(newPiece);
            int xIndex = newPiece.GetComponent<PieceBehavior>().x;
            int yIndex = newPiece.GetComponent<PieceBehavior>().y;
            //bool withinBounds = (xIndex > 0 && xIndex < xDimension) && (yIndex > 0 && yIndex < yDimension);
            if (xIndex - 1 >= 0 )
            {
                if(yIndex - 1 >= 0)
                {
                    if(gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex - 1, yIndex - 1].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                    {
                        identicalPieces.Add(gBoard[xIndex - 1, yIndex - 1]);
                    }
                }

                if (yIndex + 1 < yDimension)
                {
                    if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex - 1, yIndex + 1].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                    {
                        identicalPieces.Add(gBoard[xIndex - 1, yIndex + 1]);
                    }
                }

                if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex - 1, yIndex].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                {
                    identicalPieces.Add(gBoard[xIndex - 1, yIndex]);
                }
            }

            if (xIndex + 1 < xDimension)
            {
                if (yIndex - 1 >= 0)
                {
                    if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex + 1, yIndex - 1].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                    {
                        identicalPieces.Add(gBoard[xIndex + 1, yIndex - 1]);
                    }
                }

                if (yIndex + 1 < yDimension)
                {
                    if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex + 1, yIndex + 1].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                    {
                        identicalPieces.Add(gBoard[xIndex + 1, yIndex + 1]);
                    }
                }

                if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex + 1, yIndex].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                {
                    identicalPieces.Add(gBoard[xIndex + 1, yIndex]);
                }
            }

            {
                if (yIndex - 1 >= 0)
                {
                    if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex, yIndex - 1].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                    {
                        identicalPieces.Add(gBoard[xIndex, yIndex - 1]);
                    }
                }

                if (yIndex + 1 < yDimension)
                {
                    if (gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID == gBoard[xIndex, yIndex + 1].GetComponent<PieceBehavior>().ID && gBoard[xIndex, yIndex].GetComponent<PieceBehavior>().ID < 3)
                    {
                        identicalPieces.Add(gBoard[xIndex, yIndex + 1]);
                    }
                }
            }

            if(identicalPieces.Count >= 4)
            {
                foreach(GameObject identical in identicalPieces)
                {
                    if(!(toDelete.Contains(identical)))
                    {
                        toDelete.Add(identical);
                    }
                }
            }

            identicalPieces.Clear();
        }

        

        //if(toDelete.Count == 0)
        //{
        //    Debug.Log("No chain");
        //    isBoardInteractable = true;
        //}

        //else
        //{
        //    Debug.Log("Chaining");
        //    foreach(GameObject delete in toDelete)
        //    {
        //        PieceBehavior pb = delete.GetComponent<PieceBehavior>();

        //        nBoard[pb.x, pb.y] = -1;
        //    }

        //    DestroyDamagedPieces();
        //    StartCoroutine(DelayedRefreshBoard());
        //}
        
        isBoardInteractable = true;
        //currentTurn++;

        //if (currentTurn > maxTurn)
        //    isBoardInteractable = false;

        
    }

    public IEnumerator delayRefresh(float secondsDelay)
    {
        yield return secondsDelay;
    }

    //replace damaged pieces and undo transparency of pieces on board instantly
    //create new pieces in game object board where equivalent index in integer board is -1
    public void InstantRefreshBoard()
    {

        StartCoroutine(DelaySpawnPieces(0.5f));
        
    }

    private IEnumerator DelaySpawnPieces(float delaySeconds)
    {
        yield return new WaitForSeconds( delaySeconds);
        int clearedPieces = 0;

        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                if (nBoard[i, j] == -1)
                {
                    clearedPieces++;
                    GameObject newPiece;
                    int n = UnityEngine.Random.Range(0, 7);

                    //----------manipulating spawning of new pieces in tutorial----------\\
                    if (isTutorial && tutorialPhase == 2)
                    {
                        if (tutorialPhase == 2 && i == 3 && j == 0)
                        {
                            n = 3;
                            newPiece = createPiece(n, i, j);
                            tutorialPhase = 3;
                        }

                        else
                        {
                            n = UnityEngine.Random.Range(0, 3);
                            newPiece = createPiece(n, i, j);
                        }

                        

                    }
                    //-------------------------------------------------------\\

                    else if (specialPiecesCount >= 3)
                    {
                        //Debug.Log("spawning normal piece");
                        if (!manipulatedSpawnRates)
                        {
                            int rng = UnityEngine.Random.Range(0, 5);
                            if (rng < 4)
                            {
                                n = UnityEngine.Random.Range(0, 3);
                                newPiece = createPiece(n, i, j);
                            }

                            else
                            {
                                if (PainHex == null && bossExtraAttackPerStack != 0)
                                {
                                    newPiece = createNeutral(2, i, j);
                                    PainHex = newPiece;
                                }

                                else if(selfHurtDamage != 0)
                                {
                                    newPiece = createNeutral(1, i, j);
                                }
                                
                                else
                                {
                                    n = UnityEngine.Random.Range(0, 3);
                                    newPiece = createPiece(n, i, j);
                                }
                            }

                            Debug.Log("rng: " + rng);
                        }


                        else if (rIncreaseRate)  //boosted spawn rate red piece
                        {
                            Debug.Log("Spawning pieces with increased spawn rates for red pieces for " + manipulatedSpawnRatesRounds + "rounds");
                            int m = UnityEngine.Random.Range(0, 10);

                            if (m == 1 || m == 2)
                                newPiece = createPiece(m, i, j);

                            else
                                newPiece = createPiece(0, i, j);


                        }

                        else if (gIncreaseRate)  //boosted spawn rate red piece
                        {
                            Debug.Log("Spawning pieces with increased spawn rates for green pieces for " + manipulatedSpawnRatesRounds + "rounds");
                            int m = UnityEngine.Random.Range(0, 10);

                            if (m == 0 || m == 2)
                                newPiece = createPiece(m, i, j);

                            else
                                newPiece = createPiece(1, i, j);
                        }

                        else if (bIncreaseRate)  //boosted spawn rate red piece
                        {
                            Debug.Log("Spawning pieces with increased spawn rates for blue pieces for " + manipulatedSpawnRatesRounds + "rounds");
                            int m = UnityEngine.Random.Range(0, 10);

                            if (m == 0 || m == 1)
                                newPiece = createPiece(m, i, j);

                            else
                                newPiece = createPiece(2, i, j);
                        }

                        else //same as default
                        {
                            int rng = UnityEngine.Random.Range(0, 5);
                            if (rng < 4)
                            {
                                n = UnityEngine.Random.Range(0, 3);
                                newPiece = createPiece(n, i, j);
                            }

                            else
                            {
                                if (PainHex == null && bossExtraAttackPerStack != 0)
                                {
                                    newPiece = createNeutral(2, i, j);
                                    PainHex = newPiece;
                                }

                                else if (selfHurtDamage != 0)
                                {
                                    newPiece = createNeutral(1, i, j);
                                }

                                else
                                {
                                    n = UnityEngine.Random.Range(0, 3);
                                    newPiece = createPiece(n, i, j);
                                }
                            }
                            
                            Debug.Log("rng: " + rng);
                        }


                    }

                    else
                    {

                        if (specialPiece1 == null && c1CurrentHp > 0 && c1CurrentCharge == c1MaxCharge)   //if no specialPiece1 on board & character 1 is alive
                        {
                            Debug.Log("creating special piece1");
                            n = c1Index;
                            newPiece = createPiece(n, i, j);
                            specialPiece1 = newPiece;
                            specialPiecesCount++;
                        }

                        else if (specialPiece2 == null && c2CurrentHp > 0 && c2CurrentCharge == c2MaxCharge)  //if no specialPiece2 on board & character 2 is alive
                        {
                            Debug.Log("creating special piece2");
                            n = c2Index;
                            newPiece = createPiece(n, i, j);
                            specialPiece2 = newPiece;
                            specialPiecesCount++;
                        }

                        else if (specialPiece3 == null && c3CurrentHp > 0 && c3CurrentCharge == c3MaxCharge) //if no specialPiece3 on board & character 3 is alive
                        {
                            Debug.Log("creating special piece3");
                            n = c3Index;
                            newPiece = createPiece(n, i, j);
                            specialPiece3 = newPiece;
                            specialPiecesCount++;
                        }


                        //int specialCharacterIndex = UnityEngine.Random.Range(0, 2); // 0-1

                        //switch(specialCharacterIndex)
                        //{
                        //    case 0: n = c1Index; break;
                        //    case 1: n = c2Index; break;
                        //}

                        //Debug.Log(specialCharacterIndex);

                        else
                        {
                            int rng = UnityEngine.Random.Range(0, 5);
                            if (rng < 4)
                            {
                                n = UnityEngine.Random.Range(0, 3);
                                newPiece = createPiece(n, i, j);
                            }

                            else
                            {
                                if (PainHex == null && bossExtraAttackPerStack != 0)
                                {
                                    newPiece = createNeutral(2, i, j);
                                    PainHex = newPiece;
                                }

                                else if (selfHurtDamage != 0)
                                {
                                    newPiece = createNeutral(1, i, j);
                                }

                                else
                                {
                                    n = UnityEngine.Random.Range(0, 3);
                                    newPiece = createPiece(n, i, j);
                                }
                            }

                            Debug.Log("rng: " + rng);
                        }
                    }




                    nBoard[i, j] = n;
                    gBoard[i, j] = newPiece;
                }
            }
        }

        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in allPieces)
        {

            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
            currentColor.a = 0.98039215686f;
            piece.GetComponent<SpriteRenderer>().color = currentColor;

        }

        if (clearedPieces >= 3)
        {
            if (manipulatedSpawnRates) //if manipulated spawn rates
            {
                manipulatedSpawnRatesRounds--;  //decrease rounds remaining for manipulated spawn rates

                if (manipulatedSpawnRatesRounds == 0)
                    manipulatedSpawnRates = false;
            }
        }

        if(!playerIsFrozen)
            isBoardInteractable = true;

        

        
        //if (currentTurn > maxTurn)
        //    isBoardInteractable = false;
    }

    public void ResetPainHexDamageCounter()
    {
        bossExtraAttackAccumulated = 0;
    }

    private void SetAsSpeakerPortrait(Image _speakerPortrait)
    {
        PuzzleUIManager.Instance.speakerPortrait.sprite = _speakerPortrait.sprite;
    }

    private void UpdateHelpDialogue()
    {
        if(tutorialPhase == 1 && !(PuzzleUIManager.Instance.helpDialogue1.activeInHierarchy))
        {
            PuzzleUIManager.Instance.helpDialogue1.SetActive(true);
            PuzzleUIManager.Instance.helpDialogue2.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue3.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue4.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue5.SetActive(false);

            PuzzleUIManager.Instance.mainArrow.SetActive(true);
            //arrowGroup1.SetActive(true);
            PuzzleUIManager.Instance.arrowGroup2.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup3.SetActive(false);

            AnimationManager.Instance.PlayTutorialAnimation1();
        }

        if (tutorialPhase == 2 && !(PuzzleUIManager.Instance.helpDialogue2.activeInHierarchy))
        {
            PuzzleUIManager.Instance.helpDialogue2.SetActive(true);
            PuzzleUIManager.Instance.helpDialogue3.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue4.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue5.SetActive(false);

            PuzzleUIManager.Instance.mainArrow.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup1.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup2.SetActive(true);
            PuzzleUIManager.Instance.arrowGroup3.SetActive(false);
            AnimationManager.Instance.StopTutorialAnimation1();
            PuzzleUIManager.Instance.helpDialogue1.SetActive(false);


        }

        if (tutorialPhase == 4 && !(PuzzleUIManager.Instance.helpDialogue3.activeInHierarchy))
        {
            PuzzleUIManager.Instance.helpDialogue1.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue2.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue3.SetActive(true);
            PuzzleUIManager.Instance.helpDialogue4.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue5.SetActive(false);


            PuzzleUIManager.Instance.arrowGroup1.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup2.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup3.SetActive(true);

        }

        if (tutorialPhase == 5 && !(PuzzleUIManager.Instance.helpDialogue4.activeInHierarchy))
        {
            PuzzleUIManager.Instance.helpDialogue1.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue2.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue3.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue4.SetActive(true);
            PuzzleUIManager.Instance.helpDialogue5.SetActive(false);

            PuzzleUIManager.Instance.mainArrow.SetActive(true);
            PuzzleUIManager.Instance.arrowGroup1.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup2.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup3.SetActive(false);
            //arrowGroup4.SetActive(true);
            PuzzleUIManager.Instance.arrowGroup5.SetActive(false);

            AnimationManager.Instance.PlayTutorialAnimation2();
            Debug.Log("Playing animtion 2");

        }

        if (tutorialPhase == 6 && !(PuzzleUIManager.Instance.helpDialogue5.activeInHierarchy))
        {
            PuzzleUIManager.Instance.helpDialogue1.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue2.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue3.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue4.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue5.SetActive(true);

            PuzzleUIManager.Instance.arrowGroup1.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup2.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup3.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup4.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup5.SetActive(true);

            AnimationManager.Instance.StopTutorialAnimation2();
            PuzzleUIManager.Instance.mainArrow.SetActive(false);



        }

        if (tutorialPhase == 7 && !(PuzzleUIManager.Instance.endText.activeInHierarchy))
        {
            PuzzleUIManager.Instance.helpDialogue1.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue2.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue3.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue4.SetActive(false);
            PuzzleUIManager.Instance.helpDialogue5.SetActive(false);


            PuzzleUIManager.Instance.arrowGroup1.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup2.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup3.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup4.SetActive(false);
            PuzzleUIManager.Instance.arrowGroup5.SetActive(false);


            PuzzleUIManager.Instance.endText.GetComponentInChildren<Text>().text = "Click anywhere to exit tutorial!";
            PuzzleUIManager.Instance.endText.SetActive(true);
        }


    }

    private void UpdateCatchphraseDialogue()
    {
        if (
            PuzzleUIManager.Instance.charDialogue1.gameObject.activeInHierarchy
            || 
            PuzzleUIManager.Instance.charDialogue2.gameObject.activeInHierarchy
            || 
            PuzzleUIManager.Instance.charDialogue3.gameObject.activeInHierarchy
            )

            catchPhraseTick += Time.deltaTime;

        if(catchPhraseTick > catchphraseDuration)
        {
            PuzzleUIManager.Instance.charDialogue1.gameObject.SetActive(false);
            PuzzleUIManager.Instance.charDialogue2.gameObject.SetActive(false);
            PuzzleUIManager.Instance.charDialogue3.gameObject.SetActive(false);
            catchPhraseTick = 0.0f;

        }
    }

    private void Update2ndLastLevelDialogue()
    {
        PuzzleUIManager.Instance.charDialogue2.gameObject.SetActive(false);
        PuzzleUIManager.Instance.charDialogue3.gameObject.SetActive(false);
        PuzzleUIManager.Instance.charDialogue1.gameObject.SetActive(true);

        if (dialogueIndexFor2ndLast < FindObjectOfType<StoryManager>()
                .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].sentences.Length)
        {
            PuzzleUIManager.Instance.charDialogue1.gameObject.GetComponentInChildren<Text>().text =
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue]
                    .sentences[dialogueIndexFor2ndLast];
        }
        else
        {
            SceneManager.LoadScene(Values.SceneNames.BedroomScene);
            FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
            FindObjectOfType<StoryManager>().currentDialogue++;
        }
    }

    private void UpdateFinalLevelDialogue()
    {

        if (dialogueIndexForFinal < FindObjectOfType<StoryManager>()
                .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].sentences.Length - 5)
        {
            
            PuzzleUIManager.Instance.Text.GetComponent<Text>().text =
                FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                    .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue]
                    .sentences[dialogueIndexForFinal];

            int RisingValue = FindObjectOfType<StoryManager>()
                .StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].sentences.Length / 2;

            float currentTextPosition = initTextPosY *5 ;
            float increaseValue = currentTextPosition / RisingValue;
            Vector3 increaseVector = new Vector3(0, increaseValue, 0);

            float currentAlpha = 1;
            float increaseAlphaValue = currentAlpha / RisingValue;
            Color increaseAlpha = new Color(0, 0, 0, increaseAlphaValue);


            if (dialogueIndexForFinal >= RisingValue)
            {
                PuzzleUIManager.Instance.Text.GetComponent<RectTransform>().position += increaseVector;
                PuzzleUIManager.Instance.FadeToBlackPanel.GetComponent<Image>().color += increaseAlpha;
            }
            
        }
        
    }

    private void OnWin()
    {
        //Values.Player.gold += 25;
        Debug.Log("Win");
        PuzzleUIManager.Instance.endText.GetComponentInChildren<Text>().text = "Victory!";
        PuzzleUIManager.Instance.endText.SetActive(true);
        //SceneManager.LoadScene("LevelSetupTest");
    }

    private void OnLose()
    {
       
        PuzzleUIManager.Instance.endText.SetActive(true);
        if (isRigged)
        {
            Debug.Log("Lose Rigged");
            PuzzleUIManager.Instance.endText.GetComponentInChildren<Text>().text = "Game Over!";
        }
        else
        {
            Debug.Log("Lose Not Rigged");
            PuzzleUIManager.Instance.endText.GetComponentInChildren<Text>().text = "Try Again!";
        }
        //SceneManager.LoadScene("LevelSetupTest");

    }

    IEnumerator WaitAnimation()
    {
        yield return new WaitForSeconds(3.0f);
        isPuzzleDone = true;
        Debug.Log("triggertoscene transfer");
    }


}
