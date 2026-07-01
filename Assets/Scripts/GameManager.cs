using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<int> cards = new List<int>(52);
    public List<int> shuffledNumbers = new List<int>(52);

    GameObject[,] gridPosition = new GameObject[7, 12];

    public GameObject cardPrefab;

    void Start()
    {
        ShuffleCards();

        //when the game starts, draw 28 cards
        //line the cards up so there are 7 columns

        SetupCards();
                     
    }

    void SetupCards()
    {
        for (int x = 0; x < 7; x++)
        {
            for (int y = 11; y >= 11 - x; y--)
            {
                bool isRevealed = y == 11 - x;

                DrawCard(x, y, isRevealed);
            }
        }
    }
    
    void ShuffleCards()
    {
        //Have a list of numbers in a list
        //Put a number in each index in the list
        
        for (int i = 0; i < 52; i++)
        {            
            cards.Add(i);
        }

        //Pick a random number from that list
        //Add the number in that index to the shuffled list
        //Remove that index from the cards list
        //Repeat until there is no more cards in the cards list

        for (int i = 0; i < 52; i++)
        {
            int randomNumber = Random.Range(0, cards.Count);

            shuffledNumbers.Add(cards[randomNumber]);

            cards.RemoveAt(randomNumber);

            //print(shuffledNumbers[i]);
        }
    }

    GameObject DrawCard(int x, int y, bool isRevealed)
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
}
