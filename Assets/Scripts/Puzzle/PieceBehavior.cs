using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour
{
    
    public int ID { get; private set; } = -1;
    public int x = -1;
    public int y = -1;

    private float holdTime = 5.0f;
    private float holdTick = 0.0f;
    [SerializeField] GameObject Border;

    private bool isSelected = false;
    // Start is called before the first frame update
    void Start()
    {
        
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

        if (GameManager.Instance.selected.Count == 0)
        {
            if (this.GetComponent<PieceBehavior>().ID >= 3)
                return;
            if(!GameManager.isTutorial)
            {
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

                GameManager.Instance.selected.Add(gameObject);
                Instantiate(Border, this.transform);
            }

            else
            {
                if (GameManager.tutorialPhase == 1)
                {
                    if ((this.x == 2 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 1 && this.y == 1))
                    {
                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                    }
                }

                if (GameManager.tutorialPhase == 2)
                {
                    if ((this.x == 2 && this.y == 3) || ((this.x == 2 && this.y == 2)) || (this.x == 3 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 4 && this.y == 1) || (this.x == 3 && this.y == 0))
                    {
                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
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

                    GameManager.Instance.selected.Add(gameObject);
                    Instantiate(Border, this.transform);
                }

                else if (this.ID == GameManager.Instance.selected[0].GetComponent<PieceBehavior>().ID || this.GetComponent<PieceBehavior>().ID >= 3)
                {

                    GameObject latestSelected = GameManager.Instance.selected[GameManager.Instance.selected.Count - 1];
                    if (this.gameObject == latestSelected)
                    {
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
                    }



                    else
                    {
                        bool neighborInX = this.x <= latestSelected.GetComponent<PieceBehavior>().x + 1 && this.x >= latestSelected.GetComponent<PieceBehavior>().x - 1;
                        bool neighborInY = this.y <= latestSelected.GetComponent<PieceBehavior>().y + 1 && this.y >= latestSelected.GetComponent<PieceBehavior>().y - 1;

                        if (neighborInX && neighborInY)
                        {
                            if (this.GetComponent<PieceBehavior>().ID >= 3)
                            {
                                Debug.Log(GameManager.Instance.powerups.Count);
                                if (GameManager.Instance.powerups.Count < 2)
                                {
                                    GameManager.Instance.powerups.Add(gameObject);
                                }

                                else
                                {
                                    return;
                                }

                            }
                            GameManager.Instance.selected.Add(gameObject);
                            Instantiate(Border, this.transform);
                        }
                    }


                }
            }


            else
            {
                if (GameManager.tutorialPhase == 1)
                {
                    if ((this.x == 2 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 1 && this.y == 1))
                    {
                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                        Debug.Log("true");
                    }
                }

                if (GameManager.tutorialPhase == 2)
                {
                    if ((this.x == 2 && this.y == 3) || ((this.x == 2 && this.y == 2)) || (this.x == 3 && this.y == 2) || (this.x == 3 && this.y == 1) || (this.x == 4 && this.y == 1) || (this.x == 3 && this.y == 0))
                    {
                        GameManager.Instance.selected.Add(gameObject);
                        Instantiate(Border, this.transform);
                    }
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
}
