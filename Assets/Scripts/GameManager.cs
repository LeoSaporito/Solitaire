using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    List<int> cards = new List<int>(52);
    public List<int> shuffledNumbers = new List<int>(52);
    public List<int> cardsInQueue = new List<int>();

    List<int> hearts = new List<int>(13);
    List<int> diamonds = new List<int>(13);
    List<int> clubs = new List<int>(13);
    List<int> spades = new List<int>(13);

    GameObject[,] gridPosition = new GameObject[7, 12];

    public GameObject cardPrefab;
    public GameObject drawPilePrefab;

    public GameObject heartsPlacementPrefab;
    public GameObject diamondsPlacementPrefab;
    public GameObject clubsPlacementPrefab;
    public GameObject spadesPlacementPrefab;

    public List<GameObject> cardsSelected = new List<GameObject>(2);

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
        for (int x = 0; x < 7; x++)
        {
            for (int y = 11; y >= 11 - x; y--)
            {
                bool isRevealed = y == 11 - x;

                GameObject cardsDrawnOnSetup = SetupCardsOnStart(x, y, isRevealed);
            }
        }
    }

    GameObject SetupCardsOnStart(int x, int y, bool isRevealed)
    {
        GameObject cardDrawn = Instantiate(cardPrefab, new Vector2(x, y), Quaternion.identity);
        Cards cardDrawnObj = cardDrawn.GetComponent<Cards>();

        cardDrawnObj.SetSpacing(x, y);
        cardDrawnObj.SetXPosition(x);
        cardDrawnObj.SetYPosition(y);
        gridPosition[x, y] = cardDrawn;

        cardDrawnObj.GetComponent<SpriteRenderer>().sortingOrder = -y;

        int face = shuffledNumbers[0];
        shuffledNumbers.RemoveAt(0);

        if (!isRevealed)
        {
            cardDrawnObj.Hide();
        }
        else if(isRevealed)
        {
            cardDrawnObj.Reveal(face);
        }

        return cardDrawn;
    }

    void DrawPile()
    {
        GameObject drawPileObj = Instantiate(drawPilePrefab, new Vector2(-8, 3), Quaternion.identity);
    }
    GameObject DrawCard(float x, float y)
    {
        GameObject cardDrawn = Instantiate(cardPrefab, new Vector2(x, y), Quaternion.identity);
        Cards cardDrawnScript = cardDrawn.GetComponent<Cards>();
        
        int face = shuffledNumbers[0];
        cardsInQueue.Add(face);
        shuffledNumbers.Remove(face);

        cardDrawn.GetComponent<SpriteRenderer>().sortingOrder = cardsInQueue.Count;

        cardDrawnScript.Reveal(face);

        return cardDrawn;
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
                if (hit.collider.CompareTag("Draw Pile"))
                {
                    DrawCard(-8, 1);
                }
                if (hit.collider.CompareTag("Card Drawn"))
                {
                    Cards cardObjScript = hit.collider.GetComponent<Cards>();

                    cardObjScript.isSelected = true;

                    cardsSelected.Add(hit.collider.gameObject);
                }
            }
        }
    }
    void CardCheck()
    {
        if (cardsSelected.Count == 2)
        {
            Cards cardOneScript = cardsSelected[0].GetComponent<Cards>();
            Cards cardTwoScript = cardsSelected[1].GetComponent<Cards>();

            if (cardOneScript.colour != cardTwoScript.colour && cardOneScript.value == cardTwoScript.value - 1)
            {
                cardOneScript.transform.position = new Vector2(cardTwoScript.transform.position.x, cardTwoScript.transform.position.y - 1);
                gridPosition[cardTwoScript.GetXPosition(),cardTwoScript.GetYPosition()] = cardsSelected[0];
                cardsSelected[0].GetComponent<SpriteRenderer>().sortingOrder = cardsSelected[1].GetComponent<SpriteRenderer>().sortingOrder + 1;

                cardOneScript.isSelected = false;
                cardTwoScript.isSelected = false;
            }
            
            cardsSelected.RemoveAt(1);
            cardsSelected.RemoveAt(0);
        }
    }
    private void Update()
    {
        CardCheck();
    }
}
