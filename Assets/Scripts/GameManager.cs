using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public List<int> cards = new List<int>(52);
    public List<int> shuffledNumbers = new List<int>(52);

    GameObject[,] gridPosition = new GameObject[12, 12];

    public GameObject cardPrefab;

    void Start()
    {
        ShuffleCards();

        //when the game starts, draw 28 cards
        //line the cards up so there are 7 columns

        for (int x = 0; x < 7; x++)
        {                                    
            SetupCards(x, DrawCard());
        }                
    }

    private void SetupCards(int x, int card)
    {
        for (int i = 0; i <= x; i++)
        {
            for (int y = 0; y <= i; y++)
            {
                GameObject cardObj = Instantiate(cardPrefab, new Vector2(x, -y), Quaternion.identity);
                Cards cardObjScript = cardObj.GetComponent<Cards>();

                cardObjScript.SetXPosition(x);
                cardObjScript.SetYPosition(y);
                cardObjScript.SetSpacing(x, -y);

                cardObjScript.SetFace(card);

                gridPosition[x, y] = cardObj;
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

            print(shuffledNumbers[i]);
        }
    }

    int DrawCard()
    {
        int draw = shuffledNumbers[0];

        shuffledNumbers.RemoveAt(0);

        print(draw);        

        return draw;
    }
}
