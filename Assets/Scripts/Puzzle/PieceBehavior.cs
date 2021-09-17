using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceBehavior : MonoBehaviour
{
    public int ID { get; private set; } = -1;
    public int x = -1;
    public int y = -1;

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
    public void OnMouseUp()
    {
        Debug.Log("Piece clicked");

        if (gameObject == null)
            return;

        if (!GameManager.Instance.isBoardInteractable)
            return;

        if (GameManager.Instance.selected.Count == 0)
        {
            if (this.GetComponent<PieceBehavior>().ID >= 3)
                return;

            GameObject[] allPieces = GameObject.FindGameObjectsWithTag("Piece");
            foreach (GameObject piece in allPieces)
            {
                if (piece.GetComponent<PieceBehavior>().ID != this.ID  && piece.GetComponent<PieceBehavior>().ID < 3)
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

                if(this.GetComponent<PieceBehavior>().ID >= 3)
                {
                    GameManager.Instance.powerups.Remove(this.gameObject);
                }

                if(GameManager.Instance.selected.Count == 0)
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
}
