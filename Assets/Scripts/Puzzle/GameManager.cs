using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentTurn = 0;
    public int maxTurn = 99;

    public static GameManager Instance;
    public List<GameObject> pieces;
    public int[,] nBoard;
    public GameObject[,] gBoard;
    public bool isBoardInteractable = true;
    public List<GameObject> selected;
    public List<GameObject> powerups;
    public Image enemyHpBar;
    public Image c1HpBar;
    public Image c2HpBar;
    public Image c3HpBar;
    public GameObject helpDialogue1;
    public GameObject helpDialogue2;
    public GameObject helpDialogue3;
    public GameObject endText;
    public GameObject arrowGroup1;
    public GameObject arrowGroup2;
    public GameObject arrowGroup3;
    private float holdTime = 5.0f;
    private float holdTick = 0.0f;

    public int c1Index = 3;
    public int c2Index = 4;
    private float c1MaxHp = 0;
    private float c2MaxHp = 0;
    private float c3MaxHp = 0;

    private float c1CurrentHp = 0;
    private float c2CurrentHp = 0;
    private float c3CurrentHp = 0;

    public float enemyMaxHp = 100;
    private float enemyCurrentHp = 100;
    private float enemyDmg = 0;
    private float enemyAttackInterval = 1.5f;
    private float enemyAttackTick = 0.0f;

    private int xDimension = 7;
    private int yDimension = 5;

    public static bool isTutorial = false;
    public static int tutorialPhase = 0;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

    }
    void Start()
    {
        if(!isTutorial)
            nBoard = InitializeBoard();

        else
        {
            SetupTutorialBoard();
            tutorialPhase = 1;
        }

        c1MaxHp = Values.Characters.c1.hp;
        c1CurrentHp = c1MaxHp;
        c2MaxHp = Values.Characters.c2.hp;
        c2CurrentHp = c2MaxHp;
        c3MaxHp = Values.Characters.c3.hp;
        c3CurrentHp = c3MaxHp;
        enemyMaxHp = Values.Enemy.maxHP;
        enemyCurrentHp = enemyMaxHp;
        enemyDmg = Values.Enemy.dmg;
    }

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

        enemyAttackTick+= Time.deltaTime;
        if(enemyAttackTick >= enemyAttackInterval)
        {
            int charToAttack = Random.Range(0, 2);
            switch (charToAttack)
            {
                case 0: c1CurrentHp -= enemyDmg; break;
                case 1: c2CurrentHp -= enemyDmg; break;
                case 2: c3CurrentHp -= enemyDmg; break;
            }

            enemyAttackTick = 0.0f;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(!isTutorial)
            {
                if (GameManager.Instance.selected.Count >= 3)
                {
                    GameManager.Instance.Attack();
                    GameManager.Instance.InstantRefreshBoard();
                }

                else
                {
                    foreach (GameObject selectedPiece in GameManager.Instance.selected)
                    {
                        Debug.Log("before: " + GameManager.Instance.selected.Count);
                        Destroy(selectedPiece.transform.GetChild(0).gameObject);


                        if (selectedPiece.GetComponent<PieceBehavior>().ID >= 3)
                        {
                            GameManager.Instance.powerups.Remove(selectedPiece);
                        }



                        Debug.Log("after: " + GameManager.Instance.selected.Count);

                    }

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


                    GameManager.Instance.InstantRefreshBoard();
                }
            }

            else
            {
                Debug.Log("true2");
                if (GameManager.tutorialPhase == 1 && GameManager.Instance.selected.Count == 2)
                {
                    GameManager.tutorialPhase = 2;
                    GameManager.Instance.Attack();
                    GameManager.Instance.InstantRefreshBoard();
                    Debug.Log("true3");
                }

                else if (GameManager.tutorialPhase == 2 && GameManager.Instance.selected.Count >= 3)
                {
                    GameManager.tutorialPhase = 3;
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

        if (isTutorial)
        {
            UpdateHelpDialogue();
            if (tutorialPhase == 3 && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene("TransitionSample");
            }
        }

        

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
            c1HpBar.fillAmount = c1CurrentHp / c1MaxHp;
        }

        if (c3HpBar != null)
        {
            //currentHP -= 0.25f;   //testing
            c3HpBar.fillAmount = c3CurrentHp / c3MaxHp;
        }
    }

    int[,] InitializeBoard()
    {

        gBoard = new GameObject[xDimension, yDimension];

        int[,] newBoard = new int[xDimension, yDimension];

        for (int i = 0; i < xDimension; i++)
        {
            for(int j = 0; j < yDimension; j++)
            {
                int n = Random.Range(0, 7);

                if(n > 0)
                {
                    n = Random.Range(0, 3);
                }

                else
                {
                    int specialCharacterIndex = Random.Range(0, 2); // 0-1

                    switch(specialCharacterIndex)
                    {
                        case 0: n = c1Index; break;
                        case 1: n = c2Index; break;
                    }

                    Debug.Log(specialCharacterIndex);
                    
                }

                GameObject newPiece = createPiece(n, i, j);

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
            newPiece = Instantiate(pieces[pieceIndex], new Vector3((0.3f * x) -0.925f, (0.3f * y) + 0.15f, 0.0f), Quaternion.identity, null);

        else
            newPiece = Instantiate(pieces[pieceIndex], new Vector3((0.3f * x) - 0.925f, (0.3f * y) + 0.25f, 0.0f), Quaternion.identity, null);

        if(newPiece.TryGetComponent(out PieceBehavior PB))
            PB.SetValues(pieceIndex, x, y);

        return newPiece;
    }


    public bool isWithinBounds(int x, int y)
    {
        return ((x >= 0 && x <= xDimension) && (y >= 0 && y <= yDimension));
    }
    
    public void Attack()
    {
        for (int i = 0; i < selected.Count; i++)
        {
            if(selected[i].GetComponent<PieceBehavior>().ID < 3)
            {
                Debug.Log("Deleting");

                int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                int yIndex = selected[i].GetComponent<PieceBehavior>().y;

                nBoard[xIndex, yIndex] = -1;
            }
            

            else
            {
                PerformSpecialPieceEffects(i);
            }
        }

        selected.Clear();
        powerups.Clear();
        DestroyDamagedPieces();
        isBoardInteractable = false;
    }

    private void PerformSpecialPieceEffects(int i)
    {
        GameObject specialPiece = selected[i];

        switch(specialPiece.GetComponent<PieceBehavior>().ID)
        {
            case 3:
                {
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;
                    for (int j = 0; j < xDimension; j++)
                    {
                        nBoard[j, yIndex] = -1;
                    }
                }
                break;

            case 4:
                {
                    int xIndex = selected[i].GetComponent<PieceBehavior>().x;
                    int yIndex = selected[i].GetComponent<PieceBehavior>().y;

                    nBoard[xIndex, yIndex] = -1;

                    if(xIndex - 1 >= 0)
                    {
                        nBoard[xIndex - 1, yIndex] = -1;

                        if (yIndex + 1 < yDimension)
                            nBoard[xIndex - 1, yIndex + 1] = -1;
                    }
                    
                    if(xIndex + 1 < xDimension)
                    {
                        nBoard[xIndex + 1, yIndex] = -1;

                        if (yIndex + 1 < yDimension)
                            nBoard[xIndex + 1, yIndex + 1] = -1;
                    }
                    
                    //nBoard[xIndex, yIndex] = -1;
                    //nBoard[xIndex, yIndex] = -1;
                    //nBoard[xIndex, yIndex] = -1;

                }
                break;
        }
        
    }

    public void DestroyDamagedPieces()
    {
        for (int i = xDimension-1; i >= 0; i--)
        {
            for (int j = yDimension-1; j >= 0; j--)
            {
                
                if (nBoard[i, j] == -1)
                {
                    enemyCurrentHp -= 10;
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
                                gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.15f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                            else
                                gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.3f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.25f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);

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

        if(tutorialPhase == 2)
        {
            Destroy(gBoard[3, 0]);
            GameObject newPiece1 = createPiece(3, 3, 0);

            nBoard[3, 0] = 3;
            gBoard[3, 0] = newPiece1;


            generatedPieces.Add(newPiece1);


            Destroy(gBoard[2, 0]);
            GameObject newPiece2 = createPiece(1, 2, 0);

            nBoard[2, 0] = 1;
            gBoard[2, 0] = newPiece2;



            generatedPieces.Add(newPiece2);
        }

        

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
        currentTurn++;

        if (currentTurn > maxTurn)
            isBoardInteractable = false;

        
    }

    public void InstantRefreshBoard()
    {
        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                if (nBoard[i, j] == -1)
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
                }
            }
        }

        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in allPieces)
        {

            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
            currentColor.a = 1.0f;
            piece.GetComponent<SpriteRenderer>().color = currentColor;

        }

        isBoardInteractable = true;
        currentTurn++;

        if (currentTurn > maxTurn)
            isBoardInteractable = false;
    }

    private void UpdateHelpDialogue()
    {
        if(tutorialPhase == 1 && !(helpDialogue1.activeInHierarchy))
        {
            helpDialogue1.SetActive(true);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(false);

            arrowGroup1.SetActive(true);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(false);
        }

        if (tutorialPhase == 2 && !(helpDialogue2.activeInHierarchy))
        {
            helpDialogue1.SetActive(false);
            helpDialogue2.SetActive(true);
            helpDialogue3.SetActive(false);

            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(true);
            arrowGroup3.SetActive(false);
        }

        if (tutorialPhase == 3 && !(helpDialogue3.activeInHierarchy))
        {
            helpDialogue1.SetActive(false);
            helpDialogue2.SetActive(false);
            helpDialogue3.SetActive(true);

            arrowGroup1.SetActive(false);
            arrowGroup2.SetActive(false);
            arrowGroup3.SetActive(true);

            endText.SetActive(true);
        }
    }

    private void OnWin()
    {

    }

    private void OnLose()
    {

    }
}
