using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script assigned to every individual piece
public class PieceBehavior : MonoBehaviour
{
    
    public int ID { get; private set; } = -1;
    public int x = -1;  //x index on game board
    public int y = -1;  //y index on game board

    //hold properties
    private float holdTime = 5.0f;
    private float holdTick = 0.0f;
    private LineRenderer lineRenderer;
    
    [SerializeField] GameObject Border; //object instantiated when piece is clicked


    private bool isSelected = false;    //is piece currenly part of selected list
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

        if (GameManager.Instance.selected.Count == 0)   //if no other selected piece
        {
            if (this.GetComponent<PieceBehavior>().ID >= 3)
                return;
            if(!GameManager.isTutorial)
            {
                //make dissimilar pieces semi transparent
                GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
                foreach (GameObject piece in allPieces)
                {
                    if (piece.GetComponent<PieceBehavior>().ID != this.ID && piece.GetComponent<PieceBehavior>().ID < 3)
                    {
                        Color currentColor = piece.GetComponent<SpriteRenderer>().color;
                        currentColor.a = 0.5f;
                        piece.GetComponent<SpriteRenderer>().color = currentColor;
                    }
                }


                this.gameObject.transform.localScale *= 1.10f;
                GameManager.Instance.selected.Add(gameObject); //add selected object to selected objects list
                Instantiate(Border, this.transform);    //create border around piece for visual aid
                lineRenderer.positionCount = 9;

                lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));


                //lineRenderer.SetPosition(3, gameObject.)
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
                        lineRenderer.positionCount = 9;

                        lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                    }
                }

                if (GameManager.tutorialPhase == 4)
                {
                    if ((this.x == 2 && this.y == 3) || ((this.x == 2 && this.y == 2)) || (this.x == 3 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 4 && this.y == 1) || (this.x == 3 && this.y == 0))
                    {
                        this.gameObject.transform.localScale *= 1.10f;

                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                        lineRenderer.positionCount = 9;

                        lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                        lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
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
            if (!(GameManager.isTutorial))
            {
                if (GameManager.Instance.selected.Count == 0)
                {
                    if (this.GetComponent<PieceBehavior>().ID >= 3)
                        return;

                    GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
                    foreach (GameObject piece in allPieces)
                    {
                        if (piece.GetComponent<PieceBehavior>().ID != this.ID && piece.GetComponent<PieceBehavior>().ID < 3)
                        {
                            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
                            currentColor.a = 0.5f;
                            piece.GetComponent<SpriteRenderer>().color = currentColor;
                        }
                    }

                    this.gameObject.transform.localScale *= 1.10f;

                    GameManager.Instance.selected.Add(gameObject);
                    Instantiate(Border, this.transform);
                    lineRenderer.positionCount = 9;

                    lineRenderer.SetPosition(0, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(1, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(2, new Vector3(gameObject.transform.position.x + 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(3, new Vector3(gameObject.transform.position.x + 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(4, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.05f, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(5, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y - 0.025f, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(6, new Vector3(gameObject.transform.position.x - 0.05f, gameObject.transform.position.y, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(7, new Vector3(gameObject.transform.position.x - 0.025f, gameObject.transform.position.y + 0.025f, gameObject.transform.position.z - 0.0001f));
                    lineRenderer.SetPosition(8, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 0.05f, gameObject.transform.position.z - 0.0001f));

                }

                else if (this.ID == GameManager.Instance.selected[0].GetComponent<PieceBehavior>().ID || this.GetComponent<PieceBehavior>().ID >= 3)
                {

                    GameObject latestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 1];
                    if (this.gameObject == latestSelected)
                    {
                        //unselect
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



                    else
                    {
                        //select if neighboring to last selected piece
                        bool neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;
                        bool neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1;

                        if (neighborInX && neighborInY && !(GameManager.Instance.selected.Contains(this.gameObject)))
                        {
                            if (this.GetComponent<PieceBehavior>().ID >= 3)
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
                            Debug.Log("drawing line");
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

                    if (((((this.x == 2 && this.y == 2) && (GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[3, 1]) || GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[1, 1])))
                        || ((this.x == 3 && this.y == 1) && (GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[2, 2]) || GameManager.Instance.selected.Count == 0))
                        || ((this.x == 1 && this.y == 1) && (GameManager.Instance.selected.Contains(GameManager.Instance.gBoard[2, 2]) || GameManager.Instance.selected.Count == 0))))
                        && !(GameManager.Instance.selected.Contains(this.gameObject)))
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

                else if (GameManager.tutorialPhase == 4)
                {
                    if ((this.x == 2 && this.y == 3)
                        ||((this.x == 2 && this.y == 2))
                        || (this.x == 3 && this.y == 2)
                        || (this.x == 3 && this.y == 1)
                        || (this.x == 4 && this.y == 1)
                        || (this.x == 3 && this.y == 0))
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
    //public void OnMouseOver()
    //{
    //    Ray ray;
    //    RaycastHit hit;

    //    ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //    if (Physics.Raycast(ray, out hit))
    //    {

    //        if(Input.GetMouseButton(0))
    //        {

    //            if (gameObject == null)
    //                return;

    //            if (!GameManager.Instance.isBoardInteractable)
    //                return;
    //            if (!(GameManager.isTutorial))
    //            {
    //                if (GameManager.Instance.selected.Count == 0)
    //                {
    //                    if (this.GetComponent<PieceBehavior>().ID >= 3)
    //                        return;

    //                    GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
    //                    foreach (GameObject piece in allPieces)
    //                    {
    //                        if (piece.GetComponent<PieceBehavior>().ID != this.ID && piece.GetComponent<PieceBehavior>().ID < 3)
    //                        {
    //                            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
    //                            currentColor.a = 0.5f;
    //                            piece.GetComponent<SpriteRenderer>().color = currentColor;
    //                        }
    //                    }

    //                    GameManager.Instance.selected.Add(gameObject);
    //                    Instantiate(Border, this.transform);
    //                }

    //                else if (this.ID == GameManager.Instance.selected[0].GetComponent<PieceBehavior>().ID || this.GetComponent<PieceBehavior>().ID >= 3)
    //                {

    //                    GameObject latestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 1];
    //                    if (this.gameObject == latestSelected)
    //                    {

    //                    }

    //                    else if(GameManager.Instance.selected.Contains(this.gameObject))
    //                    {

    //                    }

    //                    else
    //                    {
    //                        bool neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;
    //                        bool neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1;

    //                        if (neighborInX && neighborInY)
    //                        {
    //                            if (this.GetComponent<PieceBehavior>().ID >= 3)
    //                            {
    //                                Debug.Log(GameManager.Instance.powerups.Count);
    //                                if (GameManager.Instance.powerups.Count < 2)
    //                                {
    //                                    GameManager.Instance.powerups.Add(gameObject);
    //                                }

    //                                else
    //                                {
    //                                    return;
    //                                }

    //                            }
    //                            GameManager.Instance.selected.Add(gameObject);
    //                            Instantiate(Border, this.transform);
    //                        }
    //                    }


    //                }
    //            }


    //            else
    //            {
    //                if (GameManager.tutorialPhase == 1)
    //                {
    //                    if ((this.x == 2 && this.y == 2) || ((this.x == 3 && this.y == 1)))
    //                    {
    //                        GameManager.Instance.selected.Add(gameObject);
    //                        Instantiate(Border, this.transform);
    //                    }
    //                }

    //                if (GameManager.tutorialPhase == 2)
    //                {
    //                    if ((this.x == 2 && this.y == 3) || ((this.x == 2 && this.y == 2)) || (this.x == 3 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 4 && this.y == 1) || (this.x == 3 && this.y == 0))
    //                    {
    //                        GameManager.Instance.selected.Add(gameObject);
    //                        Instantiate(Border, this.transform);
    //                    }
    //                }
    //            }
    //        }

    //    }
    //}

    //public static void DrawCircle(this GameObject container, float radius, float lineWidth)
    //{
    //    var segments = 360;
    //    var line = container.AddComponent<LineRenderer>();
    //    line.useWorldSpace = false;
    //    line.startWidth = lineWidth;
    //    line.endWidth = lineWidth;
    //    line.positionCount = segments + 1;

    //    var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
    //    var points = new Vector3[pointCount];

    //    for (int i = 0; i < pointCount; i++)
    //    {
    //        var rad = Mathf.Deg2Rad * (i * 360f / segments);
    //        points[i] = new Vector3(Mathf.Sin(rad) * radius, 0, Mathf.Cos(rad) * radius);
    //    }

    //    line.SetPositions(points);
    //}
}
