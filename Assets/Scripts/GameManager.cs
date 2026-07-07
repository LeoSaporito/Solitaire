using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using System.Net;

public class GameManager : MonoBehaviour
{
    public List<int> cards = new List<int>(52);
    public List<int> shuffledNumbers = new List<int>(52);
    public List<int> cardsInQueue = new List<int>();
    public List<GameObject> cardsInDrawPile = new List<GameObject>();

    public List<GameObject> hearts = new List<GameObject>(12);
    public List<GameObject> diamonds = new List<GameObject>(12);
    public List<GameObject> clubs = new List<GameObject>(12);
    public List<GameObject> spades = new List<GameObject>(12);

    GameObject[,] gridPosition = new GameObject[9, 19];

    public GameObject cardPrefab;
    public GameObject drawPilePrefab;
    public GameObject reloadDrawDeckPrefab;

    public GameObject heartsPlacementPrefab;
    public GameObject diamondsPlacementPrefab;
    public GameObject clubsPlacementPrefab;
    public GameObject spadesPlacementPrefab;

    public List<GameObject> cardsSelected = new List<GameObject>(2);

    GameObject drawPileObj;

    public GameObject column1;
    public GameObject column2;
    public GameObject column3;
    public GameObject column4;
    public GameObject column5;
    public GameObject column6;
    public GameObject column7;

    public GameObject suitPlacementSpot;

    void Start()
    {
        ShuffleCards();
        SetupCards();
        DrawPile();
        DrawAndSuitSections();

        suitPlacementSpot.SetActive(false);
        column1.SetActive(false);
        column2.SetActive(false);
        column3.SetActive(false);
        column4.SetActive(false);
        column5.SetActive(false);
        column6.SetActive(false);
        column7.SetActive(false);
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
            for (int y = 18; y > 18 - x; y--)
            {
                GameObject cardObj = Card(x, y);
                Cards cardObjScript = cardObj.GetComponent<Cards>();

                cardObj.GetComponent<SpriteRenderer>().sortingOrder = -y;
                cardObjScript.isInDeck = false;
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
        drawPileObj = Instantiate(drawPilePrefab, new Vector2(-8, 2), Quaternion.identity);
        drawPileObj.GetComponent<SpriteRenderer>().sortingOrder = 1;
    }
    void DrawAndSuitSections()
    {
        GameObject reloadDrawDeckObj = Instantiate(reloadDrawDeckPrefab, new Vector3(-8, 2, 1), Quaternion.identity);
        reloadDrawDeckObj.GetComponent<SpriteRenderer>().sortingOrder = 0;

        Instantiate(heartsPlacementPrefab, new Vector2(8, 2), Quaternion.identity);
        Instantiate(diamondsPlacementPrefab, new Vector2(8, 0), Quaternion.identity);
        Instantiate(clubsPlacementPrefab, new Vector2(8, -2), Quaternion.identity);
        Instantiate(spadesPlacementPrefab, new Vector2(8, -4), Quaternion.identity);        
    }    
    public void OnClick(InputValue value)
    {
        if (value.isPressed)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                GameObject obj = hit.collider.gameObject;
                Cards cardObjScript = hit.collider.GetComponent<Cards>();

                print(obj);

                if (obj.CompareTag("Draw Pile") && shuffledNumbers.Count != 0)
                {
                    ClickedDrawPile();
                }
                else if (obj.CompareTag("Reload Draw Deck") && shuffledNumbers.Count == 0)
                {
                    ClickedReloadDeck();
                }

                if (obj.CompareTag("Card Drawn") && cardsSelected.Count < 2)
                {
                    if (cardObjScript.isCardRevealed == false)
                    {
                        return;
                    }
                    else
                    {
                        cardObjScript.isSelected = true;
                        obj.transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                        cardsSelected.Add(obj);
                    }
                }

                if (obj.CompareTag("Suit Placement Spot") || obj.CompareTag("Column"))
                {
                    cardsSelected.Add(obj);
                }
            }
        }
    }
    void ClickedDrawPile()
    {
        GameObject drawnCardObj = Card(0, 15);
        Cards drawnCardObjScript = drawnCardObj.GetComponent<Cards>();

        drawnCardObjScript.Reveal(drawnCardObjScript.faceCard);

        drawnCardObjScript.SetStateOfCard("Revealed");
        drawnCardObjScript.isCardRevealed = true;
        drawnCardObjScript.isInDeck = true;

        cardsInQueue.Add(drawnCardObjScript.faceCard);
        cardsInDrawPile.Add(drawnCardObj);

        drawnCardObj.GetComponent<SpriteRenderer>().sortingOrder = cardsInQueue.IndexOf(drawnCardObjScript.faceCard);

        if (shuffledNumbers.Count == 0)
        {
            Destroy(drawPileObj);
        }
    }
    void ClickedReloadDeck()
    {
        DrawPile();

        int numberOfCardsRemainingInDeck = cardsInQueue.Count;

        for (int i = 0; i < numberOfCardsRemainingInDeck; i++)
        {
            GameObject cardsObj = cardsInDrawPile[i];
            Cards cardsObjScript = cardsInDrawPile[i].GetComponent<Cards>();

            shuffledNumbers.Add(cardsInQueue[0]);
            cardsInQueue.RemoveAt(0);

            if (cardsObjScript.isInDeck == true)
            {
                Destroy(cardsObj);
            }
        }
        for (int i = 0; i < numberOfCardsRemainingInDeck; i++)
        {
            cardsInDrawPile.RemoveAt(0);
        }
    }
    public void OnRightClick(InputValue value)
    {
        if (value.isPressed)
        {
            for (int i = 0; i < cardsSelected.Count; i++)
            {
                if (cardsSelected[i] != null)
                {
                    cardsSelected[i].transform.localScale = new Vector3(1f, 1f, 1f);

                    cardsSelected.RemoveAt(0);
                }
            }
        }
    }
    void CompareCards()
    {
        if (cardsSelected[0].CompareTag("Card Drawn") && cardsSelected[1].CompareTag("Card Drawn"))
        {
            GameObject cardOneObj = cardsSelected[0];
            GameObject cardTwoObj = cardsSelected[1];
            Cards cardOneScript = cardsSelected[0].GetComponent<Cards>();
            Cards cardTwoScript = cardsSelected[1].GetComponent<Cards>();
       
            if (cardOneScript.colour != cardTwoScript.colour && cardOneScript.value == cardTwoScript.value - 1 && cardOneScript.isCardRevealed == true)
            { 
                if (cardOneScript.isInDeck)
                {
                    gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition()] = null;
                    
                    gridPosition[cardTwoScript.GetXPosition(), cardTwoScript.GetYPosition() - 1] = cardOneObj;

                    cardOneScript.SetXPosition(cardTwoScript.GetXPosition());
                    cardOneScript.SetYPosition(cardTwoScript.GetYPosition() - 1);

                    cardOneObj.transform.position = new Vector2(cardTwoObj.transform.position.x, (cardTwoObj.transform.position.y - 0.5f));
                }
                else if (gridPosition[cardOneScript.GetXPosition(), cardOneScript.GetYPosition() - 1] != null)
                {
                    MovePile(cardOneObj, cardTwoObj);
                }
                
                cardOneScript.isSelected = false;
                cardTwoScript.isSelected = false;                
            }                        
        }
        else if (cardsSelected[0].CompareTag("Card Drawn") && cardsSelected[1].CompareTag("Suit Placement Spot"))
        {
            GameObject cardOneObj = cardsSelected[0];
            GameObject cardTwoObj = cardsSelected[1];

            ScoredCards(cardOneObj);
        }
        if (cardsSelected[1].CompareTag("Column"))
        {
            GameObject firstObj = cardsSelected[0];
            GameObject secondObj = cardsSelected[1];
            Cards firstObjScript = firstObj.GetComponent<Cards>();
            Column secondObjScript = secondObj.GetComponent<Column>();

            if (gridPosition[secondObjScript.GetXPosition(), 18] == null && firstObjScript.value == 13)
            {
                gridPosition[firstObjScript.GetXPosition(), firstObjScript.GetYPosition()] = null;

                firstObjScript.SetXPosition(secondObjScript.GetXPosition());
                firstObjScript.SetYPosition(18);

                gridPosition[firstObjScript.GetXPosition(), firstObjScript.GetYPosition()] = firstObj;

                firstObj.transform.position = new Vector2(secondObjScript.transform.position.x, 2);
            }            
        }

        cardsSelected[1].transform.localScale = new Vector3(1f, 1f, 1f);
        cardsSelected[0].transform.localScale = new Vector3(1f, 1f, 1f);
            
        cardsSelected.RemoveAt(1);
        cardsSelected.RemoveAt(0);        
    }
    void MovePile(GameObject movingCardObj, GameObject targetCardObj)
    {
        Cards movingCardScript = movingCardObj.GetComponent<Cards>();
        Cards targetCardScript = targetCardObj.GetComponent<Cards>();

        int oldX = movingCardScript.GetXPosition();
        int oldY = movingCardScript.GetYPosition();

        int newX = targetCardScript.GetXPosition();
        int newY = targetCardScript.GetYPosition() - 1;

        int pileCount = 0;

        for (int y = oldY; oldY >= 0 && gridPosition[oldX, y] != null; y--)
        {
            pileCount++;
        }

        for (int i = 0; i < pileCount; i++)
        {
            GameObject cardObj = gridPosition[oldX, oldY - i];
            Cards cardObjScript = cardObj.GetComponent<Cards>();

            gridPosition[oldX, oldY - i] = null;

            gridPosition[newX, newY - i] = cardObj;

            cardObjScript.SetXPosition(newX);
            cardObjScript.SetYPosition(newY - i);

            cardObj.transform.position = new Vector2(targetCardObj.transform.position.x, (targetCardObj.transform.position.y - 0.5f - i));

            cardObj.GetComponent<SpriteRenderer>().sortingOrder = targetCardObj.GetComponent<SpriteRenderer>().sortingOrder + 1 + i;
        }
    }
    void ScoredCards(GameObject movingCardObj)
    {
        Cards movingCardObjScript = movingCardObj.GetComponent<Cards>();

        int value = movingCardObjScript.value;
        string suit = movingCardObjScript.suit;

        int heartsCount = hearts.Count;
        int diamondsCount = diamonds.Count;
        int clubsCount = clubs.Count;
        int spadesCount = spades.Count;

        movingCardObjScript.isInDeck = false;

        if (suit == "hearts" && heartsCount - value + 1 == 0)
        {
            MoveCardToScore(hearts, movingCardObj, 8, 3, "hearts");
        }
        else if (suit == "diamonds" && diamondsCount - value + 1 == 0)
        {
            MoveCardToScore(diamonds, movingCardObj, 8, 2, "diamonds");
        }
        else if (suit == "clubs" && clubsCount - value + 1 == 0)
        {
            MoveCardToScore(clubs, movingCardObj, 8, 1, "clubs");
        }
        else if (suit == "spades" && spadesCount - value + 1 == 0)
        {
            MoveCardToScore(spades, movingCardObj, 8, 0, "spades");
        }
    }
    void MoveCardToScore(List<GameObject> suit, GameObject cardObj, int x, int y, string suitString)
    {
        suit.Add(cardObj);

        Cards cardObjScript = cardObj.GetComponent<Cards>();

        gridPosition[cardObjScript.GetXPosition(), cardObjScript.GetYPosition()] = null;
        gridPosition[x, y] = cardObj;

        cardObjScript.SetXPosition(x);
        cardObjScript.SetYPosition(y);

        cardObjScript.SetStateOfCard("InSuitPile");

        switch(suitString)
        {
            case "hearts": cardObj.transform.position = new Vector3(x, 2, 1); break;
            case "diamonds": cardObj.transform.position = new Vector3(x, 0, 1); break;
            case "clubs": cardObj.transform.position = new Vector3(x, -2, 1); break;
            case "spades": cardObj.transform.position = new Vector3(x, -4, 1); break;
        };        

        cardObj.GetComponent<SpriteRenderer>().sortingOrder = suit.Count;
    }
    private void Update()
    {
        for (int x = 1; x < 8; x++)
        {
            for (int y = 18; gridPosition[x, y] != null; y--)
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
                cardObj.transform.position = new Vector3(cardObj.transform.position.x, cardObj.transform.position.y, (float) y / 10);
                cardObjScript.isInDeck = false;
            }
        }

        if (cardsSelected.Count == 0)
        {
            suitPlacementSpot.SetActive(false);
            column1.SetActive(false);
            column2.SetActive(false);
            column3.SetActive(false);
            column4.SetActive(false);
            column5.SetActive(false);
            column6.SetActive(false);
            column7.SetActive(false);
        }
        else if (cardsSelected.Count == 1)
        {
            suitPlacementSpot.SetActive(true);
            column1.SetActive(true);
            column2.SetActive(true);
            column3.SetActive(true);
            column4.SetActive(true);
            column5.SetActive(true);
            column6.SetActive(true);
            column7.SetActive(true);
        }
        else if (cardsSelected.Count == 2)
        {
            CompareCards();
        }
    }
}