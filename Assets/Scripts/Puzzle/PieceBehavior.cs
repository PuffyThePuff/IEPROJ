using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script assigned to every individual piece
public class PieceBehavior : MonoBehaviour
{
    
    [SerializeField] public int ID /*{ get; private set; }*/ = -1;
    /*
     * 0,1,2 = normal pieces
     * 3,4,5 = special piecs
     * -2,-3,-4 = neutral pieces
     */

    public int x = -1;  //x index on game board
    public int y = -1;  //y index on game board

    //hold properties
    private float holdTime = 5.0f;
    private float holdTick = 0.0f;
    private LineRenderer lineRenderer;
    
    [SerializeField] GameObject Border; //object instantiated when piece is clicked


    private bool isSelected = false;    //is piece currenly part of selected list

    private bool isMovingUp = false;
    private float lerpCurrentTime = 0.0f;
    private const float lerpEndTime = 1.0f;

    Vector3 lerpOldPos = Vector3.zero;
    Vector3 lerpNewPos = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = gameObject.GetComponent<LineRenderer>();

        if(lineRenderer == null)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.03f;
        lineRenderer.endWidth = 0.03f;
        lineRenderer.positionCount = 0;
        lineRenderer.startColor = Color.black;
        lineRenderer.endColor = Color.black;

    }

    // Update is called once per frame
    void Update()
    {
        if(isMovingUp)
        {
            lerpCurrentTime += Time.deltaTime;
            if(lerpCurrentTime >= lerpEndTime)
            {
                lerpCurrentTime = lerpEndTime;
                isMovingUp = false;
                
            }
            gameObject.transform.position = Vector3.Lerp(lerpOldPos, lerpNewPos, (lerpEndTime -(lerpEndTime - lerpCurrentTime)) / lerpEndTime);
            
        }
    }

    public void SetValues(int ID, int x, int y)
    {
        this.ID = ID;
        this.x = x;
        this.y = y;
    }

    public void OnMouseDown()
    {

    //    Debug.Log("Piece clicked");
        if (gameObject == null)
            return;

        if (!GameManager.Instance.isBoardInteractable)
            return;
        //    if (!(GameManager.isTutorial))
        //    {

        if (this.ID == -2)
            return;

        if (GameManager.Instance.selected.Count == 0)   //if no other selected piece
        {
            if (this.GetComponent<PieceBehavior>().ID >= 3 || this.ID < 0)
                return;
            if(!GameManager.isTutorial)
            {
                //make dissimilar pieces semi transparent
                GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
                foreach (GameObject piece in allPieces)
                {
                    if (piece.GetComponent<PieceBehavior>().ID != this.ID && piece.GetComponent<PieceBehavior>().ID != this.ID + 3 && piece.GetComponent<PieceBehavior>().ID != -3)
                    {
                        Color currentColor = piece.GetComponent<SpriteRenderer>().color;
                        currentColor.a = 0.5f;
                        piece.GetComponent<SpriteRenderer>().color = currentColor;
                    }
                }


                this.gameObject.transform.localScale *= 1.10f;
                GameManager.Instance.selected.Add(gameObject); //add selected object to selected objects list
                Instantiate(Border, this.transform);    //create border around piece for visual aid
                FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                FindObjectOfType<AudioManager>().Play("Ding1SFX", "sfx", false);
                Debug.Log("Ding1");
                //lineRenderer.positionCount = 9;

                //lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                //lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));


                //lineRenderer.SetPosition(3, gameObject.)

                DrawCircleInsidePiece();

            }

            else
            {
                if (GameManager.tutorialPhase == 1)
                {
                    if (((this.x == 3 && this.y == 1) || (this.x == 1 && this.y == 1)) && !GameManager.Instance.selected.Contains(this.gameObject))
                    {
                        this.gameObject.transform.localScale *= 1.10f;

                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                        DrawCircleInsidePiece();
                    }
                }

                if (GameManager.tutorialPhase == 5)
                {
                    if ((this.x == 2 && this.y == 3))
                    {
                        this.gameObject.transform.localScale *= 1.10f;

                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                        //lineRenderer.positionCount = 9;

                        //lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                        //lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));

                        DrawCircleInsidePiece();

                    }
                }
            }
            
        }

        //        else if (this.ID == GameManager.Instance.selected[0].GetComponent<PieceBehavior>().ID || this.GetComponent<PieceBehavior>().ID >= 3)
        //        {

        //            GameObject latestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 1];
        //            if (this.gameObject == latestSelected)
        //            {
        //                Destroy(this.transform.GetChild(0).gameObject);
        //                GameManager.Instance.selected.Remove(this.gameObject);

        //                if (this.GetComponent<PieceBehavior>().ID >= 3)
        //                {
        //                    GameManager.Instance.powerups.Remove(this.gameObject);
        //                }

        //                if (GameManager.Instance.selected.Count == 0)
        //                {
        //                    GameManager.Instance.InstantRefreshBoard();
        //                }
        //            }



        //            else
        //            {
        //                bool neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;
        //                bool neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1;

        //                if (neighborInX && neighborInY)
        //                {
        //                    if (this.GetComponent<PieceBehavior>().ID >= 3)
        //                    {
        //                        Debug.Log(GameManager.Instance.powerups.Count);
        //                        if (GameManager.Instance.powerups.Count < 2)
        //                        {
        //                            GameManager.Instance.powerups.Add(gameObject);
        //                        }

        //                        else
        //                        {
        //                            return;
        //                        }

        //                    }
        //                    GameManager.Instance.selected.Add(gameObject);
        //                    Instantiate(Border, this.transform);
        //                }
        //            }


        //        }
        //    }


        //    else
        //    {
        //        if (GameManager.tutorialPhase == 1)
        //        {
        //            if ((this.x == 2 && this.y == 2) || ((this.x == 3 && this.y == 1)))
        //            {
        //                GameManager.Instance.selected.Add(gameObject);
        //                Instantiate(Border, this.transform);
        //            }
        //        }

        //        if (GameManager.tutorialPhase == 2)
        //        {
        //            if ((this.x == 2 && this.y == 3) || ((this.x == 2 && this.y == 2)) || (this.x == 3 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 4 && this.y == 1) || (this.x == 3 && this.y == 0))
        //            {
        //                GameManager.Instance.selected.Add(gameObject);
        //                Instantiate(Border, this.transform);
        //            }
        //        }
        //    }

    }

    private void DrawCircleInsidePiece()
    {
        lineRenderer.positionCount = 9;

        lineRenderer.SetPosition(0,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(1,
            new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(2,
            new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(3,
            new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(4,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(5,
            new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(6,
            new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(7,
            new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f,
                gameObject.transform.position.z - 0.0001f));
        lineRenderer.SetPosition(8,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f,
                gameObject.transform.position.z - 0.0001f));
    }

    public void OnMouseUp()
    {
        
    }

    //when cursor enters collision box of piece
    public void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
        {
            if (gameObject == null)
                return;

            if (!GameManager.Instance.isBoardInteractable)
                return;

            if (this.ID == -2)
                return;

            if (!(GameManager.isTutorial))
            {
                //select first piece
                if (GameManager.Instance.selected.Count == 0)
                {
                    if (this.GetComponent<PieceBehavior>().ID >= 3)
                        return;

                    GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
                    foreach (GameObject piece in allPieces)
                    {
                        if (piece.GetComponent<PieceBehavior>().ID != this.ID && piece.GetComponent<PieceBehavior>().ID < 3 && piece.GetComponent<PieceBehavior>().ID != -3)
                        {
                            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
                            currentColor.a = 0.5f;
                            piece.GetComponent<SpriteRenderer>().color = currentColor;
                        }
                    }

                    this.gameObject.transform.localScale *= 1.10f;

                    GameManager.Instance.selected.Add(gameObject);
                    Instantiate(Border, this.transform);
                    //lineRenderer.positionCount = 9;

                    //lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                    //lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                    DrawCircleInsidePiece();


                }

                //
                else if (this.ID == GameManager.Instance.selected[0].GetComponent<PieceBehavior>().ID || this.GetComponent<PieceBehavior>().ID >= 3 ||
                    (this.GetComponent<PieceBehavior>().ID == -3 && GameManager.Instance.selected.Count >= 1) ||
                    (this.GetComponent<PieceBehavior>().ID == -4 && GameManager.Instance.selected.Count >= 1))
                {

                    GameObject latestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 1];
                    GameObject secondLatestSelected = null; if (GameManager.Instance.selected.Count >= 2) secondLatestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 2];
                    if (this.gameObject == secondLatestSelected)
                    {
                        //unselect
                        Destroy(latestSelected.transform.GetChild(0).gameObject);
                        GameManager.Instance.selected.Remove(latestSelected);

                        if (this.GetComponent<PieceBehavior>().ID >= 3)
                        {
                            GameManager.Instance.powerups.Remove(latestSelected);
                        }

                        if (GameManager.Instance.selected.Count == 0)
                        {
                            GameManager.Instance.InstantRefreshBoard();
                        }

                        latestSelected.GetComponent<LineRenderer>().positionCount = 0;
                        latestSelected.transform.localScale /= 1.10f;

                        if (GameManager.Instance.selected.Count >= 5)
                        {
                            FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                            FindObjectOfType<AudioManager>().Play("DIng5SFX", "sfx", false);
                            Debug.Log("Ding5");

                        }

                        else if (GameManager.Instance.selected.Count == 4)
                        {
                            FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                            FindObjectOfType<AudioManager>().Play("Ding4SFX", "sfx", false);
                            Debug.Log("Ding4");

                        }

                        else if (GameManager.Instance.selected.Count == 3)
                        {
                            FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                            FindObjectOfType<AudioManager>().Play("Ding3SFX", "sfx", false);
                            Debug.Log("Ding3");

                        }

                        else if (GameManager.Instance.selected.Count == 2)
                        {
                            FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                            FindObjectOfType<AudioManager>().Play("Ding2SFX", "sfx", false);
                            Debug.Log("Ding2");

                        }

                        else if (GameManager.Instance.selected.Count == 1)
                        {
                            FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                            FindObjectOfType<AudioManager>().Stop("DIing5SFX", "sfx");

                            FindObjectOfType<AudioManager>().Play("Ding1SFX", "sfx", false);
                            Debug.Log("Ding1");
                        }

                    }

                    else
                    {
                        //select if neighboring to last selected piece
                        bool neighborInX = false;
                        bool neighborInY = false;

                        neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;

                        if(this.x != latestSelected.GetComponent<PieceBehavior>().x)
                        {
                            if (latestSelected.GetComponent<PieceBehavior>().x % 2 == 0)
                                neighborInY = this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1 && this.y <= latestSelected.GetComponent<PieceBehavior>().y;

                            else if (latestSelected.GetComponent<PieceBehavior>().x % 2 == 1)
                                neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y;
                        }

                        else
                        {
                            neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y-1;
                        }
                        

                        if (neighborInX && neighborInY && !(GameManager.Instance.selected.Contains(this.gameObject)))
                        {
                            if (this.GetComponent<PieceBehavior>().ID >= 3) //if not basic piece
                            {
                                Debug.Log(GameManager.Instance.powerups.Count);
                                if (this.GetComponent<PieceBehavior>().ID == Values.Characters.c1.index && latestSelected.GetComponent<PieceBehavior>().ID == 0)
                                {

                                    GameManager.Instance.powerups.Add(gameObject);
                                }

                                else if (this.GetComponent<PieceBehavior>().ID == Values.Characters.c2.index && latestSelected.GetComponent<PieceBehavior>().ID == 1)
                                {

                                    GameManager.Instance.powerups.Add(gameObject);
                                }

                                else if (this.GetComponent<PieceBehavior>().ID == Values.Characters.c3.index && latestSelected.GetComponent<PieceBehavior>().ID == 2)
                                {

                                    GameManager.Instance.powerups.Add(gameObject);
                                }

                                else
                                {
                                    return;
                                }

                            }

                            this.gameObject.transform.localScale *= 1.10f;

                            GameManager.Instance.selected.Add(gameObject);
                            Instantiate(Border, this.transform);

                            lineRenderer.positionCount = 2;
                            lineRenderer.SetPosition(0, new Vector3( latestSelected.transform.position.x, latestSelected.transform.position.y, latestSelected.transform.position.z - 0.0001f));
                            lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));

                            if (GameManager.Instance.selected.Count >= 5)
                            {
                                FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                                FindObjectOfType<AudioManager>().Play("DIng5SFX", "sfx", false);
                                Debug.Log("Ding5");

                            }

                            else if (GameManager.Instance.selected.Count == 4)
                            {
                                FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                                FindObjectOfType<AudioManager>().Play("Ding4SFX", "sfx", false);
                                Debug.Log("Ding4");

                            }

                            else if (GameManager.Instance.selected.Count == 3)
                            {
                                FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                                FindObjectOfType<AudioManager>().Play("Ding3SFX", "sfx", false);
                                Debug.Log("Ding3");

                            }

                            else if (GameManager.Instance.selected.Count == 2)
                            {
                                FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("DIng5SFX", "sfx");

                                FindObjectOfType<AudioManager>().Play("Ding2SFX", "sfx", false);
                                Debug.Log("Ding2");

                            }

                            else if (GameManager.Instance.selected.Count == 1)
                            {
                                FindObjectOfType<AudioManager>().Stop("Ding1SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding2SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding3SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("Ding4SFX", "sfx");
                                FindObjectOfType<AudioManager>().Stop("DIing5SFX", "sfx");

                                FindObjectOfType<AudioManager>().Play("Ding1SFX", "sfx", false);
                                Debug.Log("Ding1");
                            }
                            //Debug.Log("drawing line");
                        }
                    }


                }
            }


            else
            {
                GameObject latestSelected;
                if (GameManager.Instance.selected.Count >= 1) latestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 1];
                else latestSelected = null;
                //unselect
                if (GameManager.Instance.selected.Count > 0 && this.gameObject == latestSelected)
                {
                    
                    
                    Debug.Log("Unselecting in tutorial");
                    Destroy(this.transform.GetChild(0).gameObject);
                    GameManager.Instance.selected.Remove(this.gameObject);

                    if (this.GetComponent<PieceBehavior>().ID >= 3)
                    {
                        GameManager.Instance.powerups.Remove(this.gameObject);
                    }

                    if (GameManager.Instance.selected.Count == 0)
                    {
                        GameManager.Instance.InstantRefreshBoard();
                    }

                    lineRenderer.positionCount = 0;
                    this.gameObject.transform.localScale /= 1.10f;

                }

                else if (GameManager.tutorialPhase == 1)
                {
                    bool neighborInX = false;
                    bool neighborInY = false;

                    neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;

                    if (this.x != latestSelected.GetComponent<PieceBehavior>().x)
                    {
                        if (latestSelected.GetComponent<PieceBehavior>().x % 2 == 0)
                            neighborInY = this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1 && this.y <= latestSelected.GetComponent<PieceBehavior>().y;

                        else if (latestSelected.GetComponent<PieceBehavior>().x % 2 == 1)
                            neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y;
                    }

                    else
                    {
                        neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1;
                    }
                    if (((((this.x == 2 && this.y == 2)))
                        || ((this.x == 3 && this.y == 1))
                        || ((this.x == 1 && this.y == 1)))
                        && ((!(GameManager.Instance.selected.Contains(this.gameObject))) && (neighborInX && neighborInY)) )
                    {
                        this.gameObject.transform.localScale *= 1.10f;

                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);

                        if(latestSelected != null)
                        {
                            lineRenderer.positionCount = 2;
                            lineRenderer.SetPosition(0, new Vector3(latestSelected.transform.position.x, latestSelected.transform.position.y, latestSelected.transform.position.z - 0.0001f));
                            lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                            //Debug.Log("true101");
                        }


                    }

                    Debug.Log("phase1 " + this.x + this.y);
                    Debug.Log(GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[3, 1]));
                    Debug.Log(GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[1, 1]));
                    Debug.Log(((this.x == 2 && this.y == 2) && (GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[3, 1]) || GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[1, 1]))));
                }

                else if (GameManager.tutorialPhase ==5)
                {
                    bool neighborInX = false;
                    bool neighborInY = false;

                    neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;

                    if (this.x != latestSelected.GetComponent<PieceBehavior>().x)
                    {
                        if (latestSelected.GetComponent<PieceBehavior>().x % 2 == 0)
                            neighborInY = this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1 && this.y <= latestSelected.GetComponent<PieceBehavior>().y;

                        else if (latestSelected.GetComponent<PieceBehavior>().x % 2 == 1)
                            neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y;
                    }

                    else
                    {
                        neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1;
                    }

                    if (((this.x == 2 && this.y == 3)
                        ||((this.x == 2 && this.y == 2))
                        || (this.x == 3 && this.y == 2)
                        || (this.x == 3 && this.y == 1)
                        || (this.x == 3 && this.y == 0)) 
                        && (neighborInX && neighborInY)
                        && !GameManager.Instance.selected.Contains(this.gameObject))
                    {
                        this.gameObject.transform.localScale *= 1.10f;

                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                        if (latestSelected != null)
                        {
                            lineRenderer.positionCount = 2;
                            lineRenderer.SetPosition(0, new Vector3(latestSelected.transform.position.x, latestSelected.transform.position.y, latestSelected.transform.position.z - 0.0001f));
                            lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                            //Debug.Log("true101");
                        }
                    }
                    Debug.Log("phase4 " + this.x + this.y);
                }

                
                
            }
        }
    }

    public void MoveUp(Vector3 oldPos, Vector3 newPos)
    {
        lerpOldPos = oldPos;
        lerpNewPos = newPos;
        isMovingUp = true;
        lerpCurrentTime = 0.0f;
    }
    
}
