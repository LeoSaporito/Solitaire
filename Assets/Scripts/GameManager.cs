using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public List<int> cards = new List<int>(52);
    public List<int> shuffledNumbers = new List<int>(52);
    public List<int> cardsInQueue = new List<int>();

    GameObject[,] gridPosition = new GameObject[7, 12];

    public GameObject cardPrefab;
    public GameObject drawPilePrefab;

    void Start()
    {
        ShuffleCards();
        SetupCards();
        DrawPile();
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

                SetupCardsOnStart(x, y, isRevealed);
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

        print(face);

        return cardDrawn;
    }

    void DrawPile()
    {
        GameObject drawPileObj = Instantiate(drawPilePrefab, new Vector2(-7.5f, 2.25f), Quaternion.identity);
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
                    DrawCard(-7.5f, 0f);
                }
                Cards cardObj = hit.collider.GetComponent<Cards>();
            }
        }
    }
}
