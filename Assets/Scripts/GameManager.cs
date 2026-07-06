using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    List<int> cards = new List<int>(52);
    public List<int> shuffledNumbers = new List<int>(52);
    public List<int> cardsInQueue = new List<int>();

    List<int> hearts = new List<int>(13);
    List<int> diamonds = new List<int>(13);
    List<int> clubs = new List<int>(13);
    List<int> spades = new List<int>(13);

    GameObject[,] gridPosition = new GameObject[8, 13];

    public GameObject cardPrefab;
    public GameObject drawPilePrefab;

    public GameObject heartsPlacementPrefab;
    public GameObject diamondsPlacementPrefab;
    public GameObject clubsPlacementPrefab;
    public GameObject spadesPlacementPrefab;

    public List<GameObject> cardsSelected = new List<GameObject>(2);

    bool isRevealed;

    void Start()
    {
        ShuffleCards();
        SetupCards();
        DrawPile();
        SuitSections();
    }
    
    void ShuffleCards()
    {
        for (int i = 0; i < 52; i++)
        {            
            cards.Add(i);
        }

        for (int i = 0; i < 52; i++)
        {
            int randomNumber = Random.Range(0, cards.Count);

            shuffledNumbers.Add(cards[randomNumber]);

            cards.RemoveAt(randomNumber);
        }
    }
    void SetupCards()
    {
        
        for (int x = 1; x < 8; x++)
        {
            for (int y = 12; y >= 12 - x; y--)
            {
                GameObject cardObj = Card(x, y);
                cardObj.GetComponent<SpriteRenderer>().sortingOrder = -y;
            }
        }
    }

    GameObject Card(int x, int y)
    {
        GameObject cardObj = Instantiate(cardPrefab, new Vector2(x, y), Quaternion.identity);
        Cards cardObjScript = cardObj.GetComponent<Cards>();

        cardObjScript.SetSpacing(x, y);

        cardObjScript.SetXPosition(x);
        cardObjScript.SetYPosition(y);

        gridPosition[cardObjScript.GetXPosition(), cardObjScript.GetYPosition()] = cardObj;

        print(gridPosition[x, y]);
        print(cardObjScript.GetXPosition() + "," + cardObjScript.GetYPosition());
        print(shuffledNumbers[0]);

        cardObjScript.faceCard = shuffledNumbers[0];
        shuffledNumbers.RemoveAt(0);

        return cardObj;
    }


    void DrawPile()
    {
        GameObject drawPileObj = Instantiate(drawPilePrefab, new Vector2(-8, 2), Quaternion.identity);
    }
    void SuitSections()
    {
        Instantiate(heartsPlacementPrefab, new Vector2(8, 3), Quaternion.identity);
        Instantiate(diamondsPlacementPrefab, new Vector2(8, 1), Quaternion.identity);
        Instantiate(clubsPlacementPrefab, new Vector2(8, -1), Quaternion.identity);
        Instantiate(spadesPlacementPrefab, new Vector2(8, -3), Quaternion.identity);
    }
    
    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject cardObj = hit.collider.gameObject;
                Cards cardObjScript = hit.collider.GetComponent<Cards>();

                if (cardObj.CompareTag("Draw Pile"))
                {
                    GameObject drawnCardObj = Card(0, 8);
                    Cards drawnCardObjScript = drawnCardObj.GetComponent<Cards>();

                    drawnCardObjScript.Reveal(drawnCardObjScript.faceCard);

                    cardsInQueue.Add(drawnCardObjScript.faceCard);

                    drawnCardObj.GetComponent<SpriteRenderer>().sortingOrder = cardsInQueue.IndexOf(drawnCardObjScript.faceCard);
                }
                if (cardObj.CompareTag("Card Drawn"))
                {
                    cardObjScript.isSelected = true;
                    cardObj.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                    cardsSelected.Add(cardObj);

                    CardCheck();
                }
            }
        }
    }
    void CardCheck()
    {
        if (cardsSelected.Count == 2)
        {
            GameObject cardOneObj = cardsSelected[0];
            GameObject cardTwoObj = cardsSelected[1];
            Cards cardOneScript = cardsSelected[0].GetComponent<Cards>();
            Cards cardTwoScript = cardsSelected[1].GetComponent<Cards>();
            
            cardsSelected[1].transform.localScale = new Vector3(1f, 1f, 1f);
            cardsSelected[0].transform.localScale = new Vector3(1f, 1f, 1f);
       
            if (cardOneScript.colour != cardTwoScript.colour && cardOneScript.value == cardTwoScript.value - 1)
            {
                if (gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition() - 1] == null)
                {
                    int sortingOrder = cardTwoObj.GetComponent<SpriteRenderer>().sortingOrder;
                    cardOneObj.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 1;

                    cardOneScript.transform.position = new Vector2(cardTwoScript.transform.position.x, cardTwoScript.transform.position.y - 1);
                    gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition()] = null;

                    cardOneScript.SetXPosition(cardTwoScript.GetXPosition());
                    cardOneScript.SetYPosition(cardTwoScript.GetYPosition() - 1);

                    gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition()] = cardOneObj;

                    print(cardOneScript.GetXPosition() + "," + cardOneScript.GetYPosition());
                    print(cardTwoScript.GetXPosition() + "," + cardTwoScript.GetYPosition());
                    print(gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition()]);
                }
                else if (gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition()] != null)
                {
                    for (int y = 0; gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition() - y] != null; y++)
                    {
                        GameObject childCard = gridPosition[cardTwoScript.GetXPosition(), cardTwoScript.GetYPosition() - 1 - y];
                        Cards childCardScript = childCard.GetComponent<Cards>();
                        
                        int sortingOrder = childCard.GetComponent<SpriteRenderer>().sortingOrder;
                        childCard.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder + 1 + y;

                        childCard.transform.position = new Vector2(cardTwoScript.transform.position.x, cardTwoScript.transform.position.y - 1 - y);

                        gridPosition[childCardScript.GetXPosition(), childCardScript.GetYPosition() - y] = null;

                        childCardScript.SetXPosition(childCardScript.GetXPosition());
                        childCardScript.SetYPosition(childCardScript.GetYPosition() - 1 - y);

                        gridPosition[childCardScript.GetXPosition(), childCardScript.GetYPosition() - 1 - y] = cardOneObj;

                        //print(childCardScript.GetXPosition() + "," + childCardScript.GetYPosition());
                        //print(childCardScript.GetXPosition() + "," + childCardScript.GetYPosition());
                        //print(gridPosition[childCardScript.GetXPosition(), childCardScript.GetYPosition()]);
                    }
                }
                
                cardOneScript.isSelected = false;
                cardTwoScript.isSelected = false;                               
            }            
            
            cardsSelected.RemoveAt(1);
            cardsSelected.RemoveAt(0);
        }
    }

    private void Update()
    {
        for (int x = 1; x < 8; x++)
        {
            for (int y = 12; gridPosition[x, y] != null; y--)
            {
                GameObject cardObj = gridPosition[x, y];
                Cards cardObjScript = cardObj.GetComponent<Cards>();

                if (gridPosition[x, y - 1] != null && cardObjScript.isCardRevealed == false)
                {
                    cardObjScript.Hide();
                    cardObjScript.SetStateOfCard("Unrevealed");
                }
                else if (gridPosition[x, y - 1] == null)
                {
                    cardObjScript.Reveal(cardObjScript.faceCard);
                    cardObjScript.SetStateOfCard("Revealed");
                }
            }
        }
    }
}
