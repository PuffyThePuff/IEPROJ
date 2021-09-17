using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
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
            for(int j = 0; j < yDimension; j++)
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

        if(x%2 == 0)
            newPiece = Instantiate(pieces[pieceIndex], new Vector3(0.2f * x, (0.2f * y) + 0.1f, 0.0f), Quaternion.identity, null);

        else
            newPiece = Instantiate(pieces[pieceIndex], new Vector3(0.2f * x, 0.2f * y, 0.0f), Quaternion.identity, null);

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
                                gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.2f * gBoard[i, k].GetComponent<PieceBehavior>().y) + 0.1f, gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);
                            else
                                gBoard[i, k].GetComponent<PieceBehavior>().transform.position = new Vector3(gBoard[i, k].GetComponent<PieceBehavior>().transform.position.x, (0.2f * gBoard[i, k].GetComponent<PieceBehavior>().y), gBoard[i, k].GetComponent<PieceBehavior>().transform.position.z);

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

        List<GameObject> generatedPieces = new List<GameObject>();
        List<GameObject> toDelete = new List<GameObject>();

        for(int i = 0; i < xDimension; i++)
        {
            for(int j = 0; j < yDimension; j++)
            {
                if(nBoard[i, j] == -1)
                {
                    int n = Random.Range(0, pieces.Count);

                    GameObject newPiece = createPiece(n, i, j);

                    nBoard[i, j] = n;
                    gBoard[i, j] = newPiece;

                    generatedPieces.Add(newPiece);
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

        if(toDelete.Count == 0)
        {
            Debug.Log("No chain");
            isBoardInteractable = true;
        }

        else
        {
            Debug.Log("Chaining");
            foreach(GameObject delete in toDelete)
            {
                PieceBehavior pb = delete.GetComponent<PieceBehavior>();

                nBoard[pb.x, pb.y] = -1;
            }

            DestroyDamagedPieces();
            StartCoroutine(DelayedRefreshBoard());
        }
        
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

            Color currentColor = piece.GetComponent<SpriteRenderer>().color;
            currentColor.a = 1.0f;
            piece.GetComponent<SpriteRenderer>().color = currentColor;

        }
    }
}
