using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    public static UIGameManager Instance;
    public List<GameObject> pieces;
    public int[,] nBoard;
    public GameObject[,] gBoard;
    public bool isBoardInteractable = true;
    public List<GameObject> selected;
    public List<GameObject> powerups;
    private int xDimension = 7;
    private int yDimension = 5;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    void Start()
    {
        nBoard = InitializeBoard();


    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    int[,] InitializeBoard()
    {

        gBoard = new GameObject[xDimension, yDimension];

        int[,] newBoard = new int[xDimension, yDimension];

        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                int n = Random.Range(0, pieces.Count);

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

        if (x % 2 == 0)
            newPiece = Instantiate(pieces[pieceIndex], this.transform.position + new Vector3(80 * x, (60 * y) + 30, 0.0f) - (Vector3)(this.gameObject.GetComponent<RectTransform>().sizeDelta / 4), Quaternion.identity, this.transform);

        else
            newPiece = Instantiate(pieces[pieceIndex], this.transform.position + new Vector3(80 * x, 60 * y, 0.0f) - (Vector3)(this.gameObject.GetComponent<RectTransform>().sizeDelta / 4), Quaternion.identity, this.transform);

        if (newPiece.TryGetComponent(out PieceBehavior PB))
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
            if (selected[i].GetComponent<PieceBehavior>().ID < 3)
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

        switch (specialPiece.GetComponent<PieceBehavior>().ID)
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

                    if (xIndex - 1 >= 0)
                    {
                        nBoard[xIndex - 1, yIndex] = -1;

                        if (yIndex + 1 < yDimension)
                            nBoard[xIndex - 1, yIndex + 1] = -1;
                    }

                    if (xIndex + 1 < xDimension)
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
        for (int i = xDimension - 1; i >= 0; i--)
        {
            for (int j = yDimension - 1; j >= 0; j--)
            {

                if (nBoard[i, j] == -1)
                {
                    Destroy(gBoard[i, j]);

                    int k = j;
                    int l = j;

                    while (nBoard[i, l] == -1)
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
                            gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, this.transform.position.y + (60f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 30f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z) - new Vector3(0f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y / 4, 0f);
                        else
                            gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, this.transform.position.y + (60f * gBoard[i, k].GetComponent<PieceBehavior>().y), gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z) - new Vector3(0f, this.gameObject.GetComponent<RectTransform>().sizeDelta.y/4, 0f);

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
        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                if (nBoard[i, j] == -1)
                {
                    int n = Random.Range(0, pieces.Count);

                    GameObject newPiece = createPiece(n, i, j);

                    nBoard[i, j] = n;
                    gBoard[i, j] = newPiece;
                }
            }
        }

        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in allPieces)
        {
            if (piece.TryGetComponent<Image>(out Image img))
            {
                Color currentColor = piece.GetComponent<Image>().color;
                currentColor.a = 1.0f;
                piece.GetComponent<Image>().color = currentColor;
            }
            

        }

        isBoardInteractable = true;
    }

    public void InstantRefreshBoard()
    {
        for (int i = 0; i < xDimension; i++)
        {
            for (int j = 0; j < yDimension; j++)
            {
                if (nBoard[i, j] == -1)
                {
                    int n = Random.Range(0, pieces.Count);

                    GameObject newPiece = createPiece(n, i, j);

                    nBoard[i, j] = n;
                    gBoard[i, j] = newPiece;
                }
            }
        }

        GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject piece in allPieces)
        {

            Color currentColor = piece.GetComponent<Image>().color;
            currentColor.a = 1.0f;
            piece.GetComponent<Image>().color = currentColor;

        }
    }
}
