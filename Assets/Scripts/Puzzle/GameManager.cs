using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentTurn = 0;
    public int maxTurn = 99;

    public static GameManager Instance; //instance that can be called anywhere
    public List<GameObject> pieces; //list of all piece gameobjects where index 0-2 are plain pieces
    public int[,] nBoard;   //integer representation of board
    public GameObject[,] gBoard;    //gameobject board with reference to each piece
    public bool isBoardInteractable = true; // pieces can be selected if true
    public List<GameObject> selected;   //list of selected pieces including powerups
    public List<GameObject> powerups;   //list of selected powerups

    //enemy hp bar ui
    public Image enemyHpBar;    

    //characters hp bar ui
    public Image c1HpBar;
    public Image c2HpBar;
    public Image c3HpBar;

    public Image c1ChargeBar;
    public Image c2ChargeBar;
    public Image c3ChargeBar;

    public Image c1Sprite;
    public Image c2Sprite;
    public Image c3Sprite;

    public Image SpeakerPortrait;
    //tutorial help dialogues ui
    public GameObject helpDialogue1;
    public GameObject helpDialogue2;
    public GameObject helpDialogue3;
    public GameObject helpDialogue4;
    public GameObject helpDialogue5;
    public GameObject endText;
    public GameObject endTextWin;
    public GameObject endTextLose;

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

    //hold timer
    private float holdTime = 5.0f;
    private float holdTick = 0.0f;

    //special piece count and references
    public int specialPiecesCount = 0;
    public GameObject specialPiece1 = null;
    public GameObject specialPiece2 = null;
    public GameObject specialPiece3 = null;
   

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
    private float basicDamage = 10;
    private float enhancedDamage = 60;

    //player stats

    //status effects
    private bool enemyStunned = false;
    private int enemyStunnedRounds = 0;

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

    public int gameState = 0;
    public bool hasEnded = false;
    //board rows and columns
    private int xDimension = 7;
    private int yDimension = 4;

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
        //debug
        Debug.Log("c1 index: " + Values.Player.equippedChar1.index + ", c2 Index: " + Values.Player.equippedChar2.index + ", c3 Index: " + Values.Player.equippedChar3.index);

        //setting up stats accoroding to values.cs
        isTutorial = Values.Puzzle.isTutorial;


        if(!isTutorial)
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
            charDialogue1.text = Values.Player.equippedChar1.catchPhrase;
            charDialogue2.text = Values.Player.equippedChar2.catchPhrase;
            charDialogue3.text = Values.Player.equippedChar3.catchPhrase;
            enemyMaxHp = Values.Enemy.maxHP;
            enemyCurrentHp = enemyMaxHp;
            enemyDmg = Values.Enemy.dmg;
            basicDamage = Values.Player.basicDamage;
            enhancedDamage = Values.Player.enhancedDmaage;
            enemyAttackInterval = Values.Enemy.attackInterval;
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
        Debug.Log(tutorialPhase);

        if (isTutorial)
        {
            UpdateHelpDialogue();

            if (tutorialPhase == 2)
            {
                if (c1CurrentHp == c1MaxHp)
                    c1CurrentHp -= enemyDmg;


                if (Input.GetMouseButtonUp(0))
                {
                    tutorialPhase = 3;
                }

            }

            else if (tutorialPhase == 2 && Input.GetMouseButtonUp(0))
            {
                tutorialPhase = 3;
            }

            else if (tutorialPhase == 3 && Input.GetMouseButtonUp(0))
            {
                tutorialPhase = 4;

            }

            else if (tutorialPhase == 5 && Input.GetMouseButtonUp(0))
            {
                tutorialPhase = 6;
                c1CurrentHp -= enemyDmg;
            }

            else if (tutorialPhase == 6 && Input.GetMouseButtonUp(0))
            {
                if (FindObjectOfType<StoryManager>().currentDialogue == 3)
                {
                    SceneManager.LoadScene("TransitionSample");
                    FindObjectOfType<StoryManager>().StoryChapters[FindObjectOfType<StoryManager>().currentChapter]
                        .ChapterDialogues[FindObjectOfType<StoryManager>().currentDialogue].isDone = true;
                    FindObjectOfType<StoryManager>().currentDialogue++;
                }
                else
                {
                    SceneManager.LoadScene("LevelSetupTest");
                }
            }
        }

        else
        {
            UpdateCatchphraseDialogue();
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
                    if (Input.GetMouseButtonUp(0))
                    {
                        SceneManager.LoadScene("LevelSetupTest");
                        hasEnded = true;
                    }
                }
                else if (gameState == -1)
                {
                    OnLose();
                    if (Input.GetMouseButtonUp(0))
                    {
                        SceneManager.LoadScene("LevelSetupTest");
                        hasEnded = true;
                    }
                }

            }
        }


        if (!enemyStunned)
        {
            enemyAttackTick += Time.deltaTime;
            if (enemyAttackTick >= enemyAttackInterval)
            {
                if (!isTutorial)
                {
                    if(!enemyStunned)
                    {
                        int charToAttack = Random.Range(0, 2);
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

                        if(Values.Enemy.skill == Values.Enemy.SkillType.Burst)
                        {
                            int charToBurst = Random.Range(0, 2);
                            switch (charToBurst)
                            {

                                case 0: // prioritize attack on char1
                                    {
                                        if (c1CurrentHp > 0)
                                            c1CurrentHp -= enemyDmg*3;

                                        else if (c2CurrentHp > 0)
                                            c2CurrentHp -= enemyDmg*3;

                                        else if (c3CurrentHp > 0)
                                            c3CurrentHp -= enemyDmg*3;
                                    }
                                    break;
                                case 1: // prioritize attack on char2
                                    {
                                        if (c2CurrentHp > 0)
                                            c2CurrentHp -= enemyDmg*3;

                                        else if (c3CurrentHp > 0)
                                            c3CurrentHp -= enemyDmg*3;

                                        else if (c1CurrentHp > 0)
                                            c1CurrentHp -= enemyDmg*3;
                                    }
                                    break;
                                case 2: // prioritize attack on char3
                                    {
                                        if (c3CurrentHp > 0)
                                            c3CurrentHp -= enemyDmg*3;

                                        else if (c1CurrentHp > 0)
                                            c1CurrentHp -= enemyDmg*3;

                                        else if (c2CurrentHp > 0)
                                            c2CurrentHp -= enemyDmg*3;
                                    }
                                    break;
                            }
                        }

                        else if(Values.Enemy.skill == Values.Enemy.SkillType.Freeze)
                        {
                            if(!playerIsFrozen)
                            {
                                playerIsFrozen = true;
                                isBoardInteractable = false;
                                Debug.Log("Freeze player");
                            }
                                
                        }
                    }

                    else
                    {
                        Debug.Log("Enemy is stunned and cannot attack");
                        enemyStunnedRounds--;

                        if (enemyStunnedRounds == 0)
                            enemyStunned = false;
                    }
                    

                    enemyAttackTick = 0.0f;
                }

               

            }
        }

        else
        {
            

        }
        

        if(Input.GetMouseButtonUp(0))   //if left mouse button released
        {
            if(!isTutorial)
            {
                if (GameManager.Instance.selected.Count >= 3)   //if more than 3 pieces selected
                {
                    GameManager.Instance.Attack();  //delete normal pieces and trigger special pieces effects
                    GameManager.Instance.InstantRefreshBoard(); //replace deleted pieces

                    
                }

                else
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

                        if(selectedPiece.GetComponent<LineRenderer>() != null)
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
                    c2ChargeBar.fillAmount = c2MaxCharge / c2CurrentCharge;
                    Debug.Log("true3");
                }

                else if (GameManager.tutorialPhase == 4 && GameManager.Instance.selected.Count >= 4)
                {
                    GameManager.tutorialPhase = 5;
                    GameManager.Instance.Attack();
                    GameManager.Instance.InstantRefreshBoard();
                }
            }
            
        }

        //if(selected.Count > 0)
        //{
        //    holdTick += Time.deltaTime;

        //    if (holdTick >= holdTime)
        //    {
        //        holdTick = 0.0f;

        //        while (selected.Count > 0)
        //        {
        //            Debug.Log("inside");
        //            Destroy(selected[0].transform.GetChild(0).gameObject);
        //            selected.Remove(selected[0]);

        //            if (selected[0].GetComponent<PieceBehavior>().ID >= 3)
        //            {
        //                powerups.Remove(selected[0]);
        //            }

        //            if (selected.Count == 0)
        //            {
        //                InstantRefreshBoard();
                        
        //            }


        //        }
        //    }

            
        //}

        //else
        //{
        //    holdTick = 0.0f;
        //}

        

        

    }

    private void UpdateHpBars()
    {
        if (enemyHpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            enemyHpBar.fillAmount = enemyCurrentHp / enemyMaxHp;
        }

        if (c1HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c1HpBar.fillAmount = c1CurrentHp / c1MaxHp;
        }

        if (c2HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c2HpBar.fillAmount = c2CurrentHp / c2MaxHp;
        }

        if (c3HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c3HpBar.fillAmount = c3CurrentHp / c3MaxHp;
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


        if (c1ChargeBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c1ChargeBar.fillAmount = c1CurrentCharge / c1MaxCharge;
        }

        if (c2ChargeBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c2ChargeBar.fillAmount = c2CurrentCharge / c2MaxCharge;
        }

        if (c3ChargeBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c3ChargeBar.fillAmount = c3CurrentCharge / c3MaxCharge;
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
                int n = Random.Range(0, 7); //2nd parameter dictates special piece rng: higher = more rare

                if(n > 0 || specialPiecesCount >= 3)
                {
                    //create normal piece
                    n = Random.Range(0, 3); //randomize between normal pieces index (0-2)
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

                    //int specialCharacterIndex = Random.Range(0, 2); // 0-1

                    //switch(specialCharacterIndex)
                    //{
                    //    case 0: n = c1Index; break;
                    //    case 1: n = c2Index; break;
                    //}

                    //Debug.Log(specialCharacterIndex);

                    else
                    {
                        //Debug.Log("creating special piece4");
                        n = Random.Range(0, 3);
                        newPiece = createPiece(n, i, j);
                    }
                }

                

                newBoard[i, j] = n;
                gBoard[i, j] = newPiece;
            }
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

        if(newPiece.TryGetComponent(out PieceBehavior PB))
            PB.SetValues(pieceIndex, x, y);

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
            if(selected[i].GetComponent<PieceBehavior>().ID < 3)    //if normal piece
            {
                //delete normal piece
                Debug.Log("Deleting");

                int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                if(nBoard[xIndex, yIndex] != -1)
                {
                    UpdateChargeBars(i);

                    nBoard[xIndex, yIndex] = -1;
                }

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
        isBoardInteractable = false;

        AnimationManager.Instance.PlayHitAnimation();
        Debug.Log("Attack");
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

            if(!isTutorial)
                charDialogue1.gameObject.SetActive(true);

           
        }

        else if (specialPiece.GetComponent<PieceBehavior>().ID == c2Index)
        {
            specialPiece2 = null;
            specialPiecesCount--;
            c2CurrentCharge = 0;
            UpdateChargeBars(i);

            if (!isTutorial)
                charDialogue2.gameObject.SetActive(true);

            

        }

        else if (specialPiece.GetComponent<PieceBehavior>().ID == c3Index)
        {
            Debug.Log("delete 3");
            specialPiece3 = null;
            specialPiecesCount--;
            c3CurrentCharge = 0;
            UpdateChargeBars(i);

            if (!isTutorial)
                charDialogue3.gameObject.SetActive(true);

            

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
                    enemyCurrentHp -= enhancedDamage;
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


                    //-----------stun boss-----------\\
                    Debug.Log("Disable next enemy attack (stunned)");

                    enemyStunned = true;
                    enemyStunnedRounds = 1;

                    int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    nBoard[xIndex, yIndex] = -1;
                    //--------------------------------------------\\
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


                    //-----------random piece rate boost-----------\\
                    Debug.Log("Boost spawn rate effect");

                    int pieceBoostRateIndex = Random.Range(0, 3);

                    if (pieceBoostRateIndex == 0)
                        rIncreaseRate = true;

                    else if (pieceBoostRateIndex == 1)
                        gIncreaseRate = true;

                    else if (pieceBoostRateIndex == 2)
                        bIncreaseRate = true;

                    manipulatedSpawnRates = true;
                    manipulatedSpawnRatesRounds = 5;

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

                    if(c1CurrentHp > 0)
                    {
                        c1CurrentHp += (c1MaxHp * 0.075f);

                        if(c1CurrentHp > c1MaxHp)
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

    //destroy pieces from game object board where equivalent index in integer board is -1
    public void DestroyDamagedPieces()
    {
        for (int i = xDimension-1; i >= 0; i--)
        {
            for (int j = yDimension-1; j >= 0; j--)
            {
                
                if (nBoard[i, j] == -1)
                {
                    
                    enemyCurrentHp -= basicDamage;
                    Destroy(gBoard[i, j]);

                    int k = j;
                    int l = j;

                    while(nBoard[i, l] == -1 )
                    {
                        if (l - 1 == -1)
                        {
                            break;
                        }

                        l = l - 1;

                        
                            
                    }

                    //while(l >= 0)
                    //{
                        //Debug.Log(k - (k - l));

                        if (nBoard[i, k - (k - l)] != -1)
                        {
                            nBoard[i, k] = nBoard[i, k - (k - l)];
                            gBoard[i, k] = gBoard[i, k - (k - l)];
                            gBoard[i, k].GetComponent<PieceBehavior>().y = k;

                            if (gBoard[i, k].GetComponent<PieceBehavior>().x % 2 == 0)
                                gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.2875f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                            else
                                gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3225f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.4490f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);

                            nBoard[i, k - (k - l)] = -1;
                            gBoard[i, k - (k - l)] = null;
                        }
                        
                        

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
                    int n = Random.Range(0, 7);

                    if (n > 0)
                    {
                        n = Random.Range(0, 3);
                    }

                    else
                    {
                        n = Random.Range(3, 5);
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

    //replace damaged pieces and undo transparency of pieces on board instantly
    //create new pieces in game object board where equivalent index in integer board is -1
    public void InstantRefreshBoard()
    {
        int clearedPieces = 0;

        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                if (nBoard[i, j] == -1)
                {
                    clearedPieces++;
                    GameObject newPiece;
                    int n = Random.Range(0, 7);

                    //----------manipulating spawning of new pieces in tutorial----------\\
                    if (isTutorial && tutorialPhase == 2)
                    {
                        if(tutorialPhase == 2 && i == 3 && j == 0)
                        {
                            n = 4;
                            newPiece = createPiece(n, i, j);
                        }
                        
                        else
                        {
                            n = Random.Range(0, 3);
                            newPiece = createPiece(n, i, j);
                        }
                    }
                    //-------------------------------------------------------\\

                    else if (specialPiecesCount >= 3)
                    {
                            //Debug.Log("spawning normal piece");
                        if(!manipulatedSpawnRates)
                        {
                            n = Random.Range(0, 3);
                            newPiece = createPiece(n, i, j);
                        }


                        else if(rIncreaseRate)  //boosted spawn rate red piece
                        {
                            Debug.Log("Spawning pieces with increased spawn rates for red pieces for " + manipulatedSpawnRatesRounds + "rounds");
                            int m = Random.Range(0, 10);

                            if(m == 1 || m==2)
                                newPiece = createPiece(m, i, j);

                            else
                                newPiece = createPiece(0, i, j);

                            
                        }

                        else if (gIncreaseRate)  //boosted spawn rate red piece
                        {
                            Debug.Log("Spawning pieces with increased spawn rates for green pieces for " + manipulatedSpawnRatesRounds + "rounds");
                            int m = Random.Range(0, 10);

                            if (m == 0 || m == 2)
                                newPiece = createPiece(m, i, j);

                            else
                                newPiece = createPiece(1, i, j);                            
                        }

                        else if (bIncreaseRate)  //boosted spawn rate red piece
                        {
                            Debug.Log("Spawning pieces with increased spawn rates for blue pieces for " + manipulatedSpawnRatesRounds + "rounds");
                            int m = Random.Range(0, 10);

                            if (m == 0 || m == 1)
                                newPiece = createPiece(m, i, j);

                            else
                                newPiece = createPiece(2, i, j);
                        }

                        else //same as default
                        {
                            n = Random.Range(0, 3);
                            newPiece = createPiece(n, i, j);
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


                        //int specialCharacterIndex = Random.Range(0, 2); // 0-1

                        //switch(specialCharacterIndex)
                        //{
                        //    case 0: n = c1Index; break;
                        //    case 1: n = c2Index; break;
                        //}

                        //Debug.Log(specialCharacterIndex);

                        else
                        {
                            n = Random.Range(0, 3);
                            newPiece = createPiece(n, i, j);
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

        if(clearedPieces >= 3)
        {
            if (manipulatedSpawnRates) //if manipulated spawn rates
            {
                manipulatedSpawnRatesRounds--;  //decrease rounds remaining for manipulated spawn rates

                if (manipulatedSpawnRatesRounds == 0)
                    manipulatedSpawnRates = false;
            }
        }
        isBoardInteractable = true;
        currentTurn++;

        //if (currentTurn > maxTurn)
        //    isBoardInteractable = false;
    }

    private void UpdateHelpDialogue()
    {
        if(tutorialPhase == 1 && !(helpDialogue1.activeInHierarchy))
        {
            helpDialogue1.SetActive(true);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(false);
            helpDialogue4.SetActive(false);
            helpDialogue5.SetActive(false);

            mainArrow.SetActive(true);
            //arrowGroup1.SetActive(true);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(false);

            AnimationManager.Instance.PlayTutorialAnimation1();
        }

        if (tutorialPhase == 2 && !(helpDialogue2.activeInHierarchy))
        {
            helpDialogue2.SetActive(true);
            helpDialogue3.SetActive(false);
            helpDialogue4.SetActive(false);
            helpDialogue5.SetActive(false);

            mainArrow.SetActive(false);
            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(true);
            arrowGroup3.SetActive(false);
            AnimationManager.Instance.StopTutorialAnimation1();
            helpDialogue1.SetActive(false);


        }

        if (tutorialPhase == 3 && !(helpDialogue3.activeInHierarchy))
        {
            helpDialogue1.SetActive(false);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(true);
            helpDialogue4.SetActive(false);
            helpDialogue5.SetActive(false);


            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(true);

        }

        if (tutorialPhase == 4 && !(helpDialogue4.activeInHierarchy))
        {
            helpDialogue1.SetActive(false);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(false);
            helpDialogue4.SetActive(true);
            helpDialogue5.SetActive(false);

            mainArrow.SetActive(true);
            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(false);
            //arrowGroup4.SetActive(true);
            arrowGroup5.SetActive(false);

            AnimationManager.Instance.PlayTutorialAnimation2();
            Debug.Log("Playing animtion 2");

        }

        if (tutorialPhase == 5 && !(helpDialogue5.activeInHierarchy))
        {
            helpDialogue1.SetActive(false);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(false);
            helpDialogue4.SetActive(false);
            helpDialogue5.SetActive(true);

            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(false);
            arrowGroup4.SetActive(false);
            arrowGroup5.SetActive(true);

            AnimationManager.Instance.StopTutorialAnimation2();
            mainArrow.SetActive(false);



        }

        if (tutorialPhase == 6 && !(endText.activeInHierarchy))
        {
            helpDialogue1.SetActive(false);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(false);
            helpDialogue4.SetActive(false);
            helpDialogue5.SetActive(false);


            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(false);
            arrowGroup4.SetActive(false);
            arrowGroup5.SetActive(false);



            endText.SetActive(true);
        }


    }

    private void UpdateCatchphraseDialogue()
    {
        if (charDialogue1.gameObject.activeInHierarchy || charDialogue2.gameObject.activeInHierarchy || charDialogue3.gameObject.activeInHierarchy)
            catchPhraseTick += Time.deltaTime;

        if(catchPhraseTick > catchphraseDuration)
        {
            charDialogue1.gameObject.SetActive(false);
            charDialogue2.gameObject.SetActive(false);
            charDialogue3.gameObject.SetActive(false);
            catchPhraseTick = 0.0f;

        }
    }

    private void OnWin()
    {
        Values.Player.gold += 25;
        Debug.Log("Win");
        endTextWin.SetActive(true);
    }

    private void OnLose()
    {
        Debug.Log("Lose");
        endTextLose.SetActive(true);
    }
}
