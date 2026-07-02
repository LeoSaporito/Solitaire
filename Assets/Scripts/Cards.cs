using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Cards : MonoBehaviour
{
    public enum StateOfCard
    {
        unrevealedInDrawPile, //0
        revealedInDrawPile, //1
        unrevealedOnBoard, //2
        revealedOnBoard, //3
        inSuitPile //4
    }

    public int xPosition;
    public int yPosition;

    public float xOffset;
    public float yOffset;

    public float xSpacing;
    public float ySpacing;

    public string colour;
    public string suit;
    public int value;

    public bool isSelected;

    public StateOfCard stateOfCard;
    public int stateOfCardInt;
    public string unrevealedInDrawPileString = "UnrevealedInDrawPile",
                  revealedInDrawPileString = "RevealedInDrawPile",
                  unrevealedOnBoardString = "UnrevealedOnBoard",
                  revealedOnBoardString = "RevealedOnBoard",
                  inSuitPuleString = "InSuitPile";

    public string currentStateOfCard;

    public Sprite back;
    public Sprite heartAce, heartTwo, heartThree, heartFour, heartFive, heartSix, heartSeven, heartEight, heartNine, heartTen, heartJack, heartQueen, heartKing, 
                  clubAce, clubTwo, clubThree, clubFour, clubFive, clubSix, clubSeven, clubEight, clubNine, clubTen, clubJack, clubQueen, clubKing,
                  diamondAce, diamondTwo, diamondThree, diamondFour, diamondFive, diamondSix, diamondSeven, diamondEight, diamondNine, diamondTen, diamondJack, diamondQueen, diamondKing,
                  spadeAce, spadeTwo, spadeThree, spadeFour, spadeFive, spadeSix, spadeSeven, spadeEight, spadeNine, spadeTen, spadeJack, spadeQueen, spadeKing;
    public void SetSpacing(float x, float y)
    {
        x += xOffset;
        y += yOffset;

        x *= xSpacing;
        y *= ySpacing;

        this.transform.position = new Vector2(x, y);        
    }
    public void SetXPosition(float x)
    {
        x = xPosition;
    }
    public void SetYPosition(float y)
    {
        y = yPosition;
    }
    public int GetXPosition()
    {
        return xPosition;
    }
    public int GetYPosition()
    {
        return yPosition;
    }
    public void Reveal(int card)
    {
        SetFace(card);
    }
    public void Hide()
    {
        this.GetComponent<SpriteRenderer>().sprite = back;
    }
    void SetFace(int number)
    {
        switch (number)
        {
            //Hearts
            case 0: this.GetComponent<SpriteRenderer>().sprite = heartAce; this.colour = "red"; this.suit = "hearts"; this.value = 1; break;
            case 1: this.GetComponent<SpriteRenderer>().sprite = heartTwo; this.colour = "red"; this.suit = "hearts"; this.value = 2; break;
            case 2: this.GetComponent<SpriteRenderer>().sprite = heartThree; this.colour = "red"; this.suit = "hearts"; this.value = 3; break;
            case 3: this.GetComponent<SpriteRenderer>().sprite = heartFour; this.colour = "red"; this.suit = "hearts"; this.value = 4; break;
            case 4: this.GetComponent<SpriteRenderer>().sprite = heartFive; this.colour = "red"; this.suit = "hearts"; this.value = 5; break;
            case 5: this.GetComponent<SpriteRenderer>().sprite = heartSix; this.colour = "red"; this.suit = "hearts"; this.value = 6; break;
            case 6: this.GetComponent<SpriteRenderer>().sprite = heartSeven; this.colour = "red"; this.suit = "hearts"; this.value = 7; break;
            case 7: this.GetComponent<SpriteRenderer>().sprite = heartEight; this.colour = "red"; this.suit = "hearts"; this.value = 8; break;
            case 8: this.GetComponent<SpriteRenderer>().sprite = heartNine; this.colour = "red"; this.suit = "hearts"; this.value = 9; break;
            case 9: this.GetComponent<SpriteRenderer>().sprite = heartTen; this.colour = "red"; this.suit = "hearts"; this.value = 10; break;
            case 10: this.GetComponent<SpriteRenderer>().sprite = heartJack; this.colour = "red"; this.suit = "hearts"; this.value = 11; break;
            case 11: this.GetComponent<SpriteRenderer>().sprite = heartQueen; this.colour = "red"; this.suit = "hearts"; this.value = 12; break;
            case 12: this.GetComponent<SpriteRenderer>().sprite = heartKing; this.colour = "red"; this.suit = "hearts"; this.value = 13; break;
            //Clubs
            case 13: this.GetComponent<SpriteRenderer>().sprite = clubAce; this.colour = "black"; this.suit = "clubs"; this.value = 1; break;
            case 14: this.GetComponent<SpriteRenderer>().sprite = clubTwo; this.colour = "black"; this.suit = "clubs"; this.value = 2; break;
            case 15: this.GetComponent<SpriteRenderer>().sprite = clubThree; this.colour = "black"; this.suit = "clubs"; this.value = 3; break;
            case 16: this.GetComponent<SpriteRenderer>().sprite = clubFour; this.colour = "black"; this.suit = "clubs"; this.value = 4; break;
            case 17: this.GetComponent<SpriteRenderer>().sprite = clubFive; this.colour = "black"; this.suit = "clubs"; this.value = 5; break;
            case 18: this.GetComponent<SpriteRenderer>().sprite = clubSix; this.colour = "black"; this.suit = "clubs"; this.value = 6; break;
            case 19: this.GetComponent<SpriteRenderer>().sprite = clubSeven; this.colour = "black"; this.suit = "clubs"; this.value = 7; break;
            case 20: this.GetComponent<SpriteRenderer>().sprite = clubEight; this.colour = "black"; this.suit = "clubs"; this.value = 8; break;
            case 21: this.GetComponent<SpriteRenderer>().sprite = clubNine; this.colour = "black"; this.suit = "clubs"; this.value = 9; break;
            case 22: this.GetComponent<SpriteRenderer>().sprite = clubTen; this.colour = "black"; this.suit = "clubs"; this.value = 10; break;
            case 23: this.GetComponent<SpriteRenderer>().sprite = clubJack; this.colour = "black"; this.suit = "clubs"; this.value = 11; break;
            case 24: this.GetComponent<SpriteRenderer>().sprite = clubQueen; this.colour = "black"; this.suit = "clubs"; this.value = 12; break;
            case 25: this.GetComponent<SpriteRenderer>().sprite = clubKing; this.colour = "black"; this.suit = "clubs"; this.value = 13; break;
            //Diamonds
            case 26: this.GetComponent<SpriteRenderer>().sprite = diamondAce; this.colour = "red"; this.suit = "diamonds"; this.value = 1; break;
            case 27: this.GetComponent<SpriteRenderer>().sprite = diamondTwo; this.colour = "red"; this.suit = "diamonds"; this.value = 2; break;
            case 28: this.GetComponent<SpriteRenderer>().sprite = diamondThree; this.colour = "red"; this.suit = "diamonds"; this.value = 3; break;
            case 29: this.GetComponent<SpriteRenderer>().sprite = diamondFour; this.colour = "red"; this.suit = "diamonds"; this.value = 4; break;
            case 30: this.GetComponent<SpriteRenderer>().sprite = diamondFive; this.colour = "red"; this.suit = "diamonds"; this.value = 5; break;
            case 31: this.GetComponent<SpriteRenderer>().sprite = diamondSix; this.colour = "red"; this.suit = "diamonds"; this.value = 6; break;
            case 32: this.GetComponent<SpriteRenderer>().sprite = diamondSeven; this.colour = "red"; this.suit = "diamonds"; this.value = 7; break;
            case 33: this.GetComponent<SpriteRenderer>().sprite = diamondEight; this.colour = "red"; this.suit = "diamonds"; this.value = 8; break;
            case 34: this.GetComponent<SpriteRenderer>().sprite = diamondNine; this.colour = "red"; this.suit = "diamonds"; this.value = 9; break;
            case 35: this.GetComponent<SpriteRenderer>().sprite = diamondTen; this.colour = "red"; this.suit = "diamonds"; this.value = 10; break;
            case 36: this.GetComponent<SpriteRenderer>().sprite = diamondJack; this.colour = "red"; this.suit = "diamonds"; this.value = 11; break;
            case 37: this.GetComponent<SpriteRenderer>().sprite = diamondQueen; this.colour = "red"; this.suit = "diamonds"; this.value = 12; break;
            case 38: this.GetComponent<SpriteRenderer>().sprite = diamondKing; this.colour = "red"; this.suit = "diamonds"; this.value = 13; break;
            //Spades
            case 39: this.GetComponent<SpriteRenderer>().sprite = spadeAce; this.colour = "black"; this.suit = "spades"; this.value = 1; break;
            case 40: this.GetComponent<SpriteRenderer>().sprite = spadeTwo; this.colour = "black"; this.suit = "spades"; this.value = 2; break;
            case 41: this.GetComponent<SpriteRenderer>().sprite = spadeThree; this.colour = "black"; this.suit = "spades"; this.value = 3; break;
            case 42: this.GetComponent<SpriteRenderer>().sprite = spadeFour; this.colour = "black"; this.suit = "spades"; this.value = 4; break;
            case 43: this.GetComponent<SpriteRenderer>().sprite = spadeFive; this.colour = "black"; this.suit = "spades"; this.value = 5; break;
            case 44: this.GetComponent<SpriteRenderer>().sprite = spadeSix; this.colour = "black"; this.suit = "spades"; this.value = 6; break;
            case 45: this.GetComponent<SpriteRenderer>().sprite = spadeSeven; this.colour = "black"; this.suit = "spades"; this.value = 7; break;
            case 46: this.GetComponent<SpriteRenderer>().sprite = spadeEight; this.colour = "black"; this.suit = "spades"; this.value = 8; break;
            case 47: this.GetComponent<SpriteRenderer>().sprite = spadeNine; this.colour = "black"; this.suit = "spades"; this.value = 9; break;
            case 48: this.GetComponent<SpriteRenderer>().sprite = spadeTen; this.colour = "black"; this.suit = "spades"; this.value = 10; break;
            case 49: this.GetComponent<SpriteRenderer>().sprite = spadeJack; this.colour = "black"; this.suit = "spades"; this.value = 11; break;
            case 50: this.GetComponent<SpriteRenderer>().sprite = spadeQueen; this.colour = "black"; this.suit = "spades"; this.value = 12; break;
            case 51: this.GetComponent<SpriteRenderer>().sprite = spadeKing; this.colour = "black"; this.suit = "spades"; this.value = 13; break;
        }
    }

    public void SetStateOfCard(string stateOfCardString)
    {
        currentStateOfCard = stateOfCardString;
        switch (stateOfCardString)
        {
            case "UnrevealedInDrawPile": this.stateOfCard = StateOfCard.unrevealedInDrawPile; break;
            case "RevealedInDrawPile": this.stateOfCard = StateOfCard.revealedInDrawPile; break;
            case "UnrevealedOnBoard": this.stateOfCard = StateOfCard.unrevealedOnBoard; break;
            case "RevealedOnBoard": this.stateOfCard = StateOfCard.revealedOnBoard; break;
            case "InSuitPile": this.stateOfCard = StateOfCard.inSuitPile; break;
        }
    }
}