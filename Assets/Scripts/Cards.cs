using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class Cards : MonoBehaviour
{
    public float xPosition;
    public float yPosition;

    public float xOffset;
    public float yOffset;

    public float xSpacing;
    public float ySpacing;

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

        xPosition = x;
        yPosition = y;

        this.transform.position = new Vector2(xPosition, yPosition);        
    }

    public void SetXPosition(float x)
    {
        x = xPosition;
    }
    public void SetYPosition(float y)
    {
        y = yPosition;
    }

    public void Reveal(int card)
    {
        SetFace(card);
    }
    public void Hide()
    {
        this.GetComponent<SpriteRenderer>().sprite = back;
    }
    public void SetFace(int number)
    {
        switch (number)
        {
            //Hearts
            case 0: this.GetComponent<SpriteRenderer>().sprite = heartAce; break;
            case 1: this.GetComponent<SpriteRenderer>().sprite = heartTwo; break;
            case 2: this.GetComponent<SpriteRenderer>().sprite = heartThree; break;
            case 3: this.GetComponent<SpriteRenderer>().sprite = heartFour; break;
            case 4: this.GetComponent<SpriteRenderer>().sprite = heartFive; break;
            case 5: this.GetComponent<SpriteRenderer>().sprite = heartSix; break;
            case 6: this.GetComponent<SpriteRenderer>().sprite = heartSeven; break;
            case 7: this.GetComponent<SpriteRenderer>().sprite = heartEight; break;
            case 8: this.GetComponent<SpriteRenderer>().sprite = heartNine; break;
            case 9: this.GetComponent<SpriteRenderer>().sprite = heartTen; break;
            case 10: this.GetComponent<SpriteRenderer>().sprite = heartJack; break;
            case 11: this.GetComponent<SpriteRenderer>().sprite = heartQueen; break;
            case 12: this.GetComponent<SpriteRenderer>().sprite = heartKing; break;
            //Clubs
            case 13: this.GetComponent<SpriteRenderer>().sprite = clubAce; break;
            case 14: this.GetComponent<SpriteRenderer>().sprite = clubTwo; break;
            case 15: this.GetComponent<SpriteRenderer>().sprite = clubThree; break;
            case 16: this.GetComponent<SpriteRenderer>().sprite = clubFour; break;
            case 17: this.GetComponent<SpriteRenderer>().sprite = clubFive; break;
            case 18: this.GetComponent<SpriteRenderer>().sprite = clubSix; break;
            case 19: this.GetComponent<SpriteRenderer>().sprite = clubSeven; break;
            case 20: this.GetComponent<SpriteRenderer>().sprite = clubEight; break;
            case 21: this.GetComponent<SpriteRenderer>().sprite = clubNine; break;
            case 22: this.GetComponent<SpriteRenderer>().sprite = clubTen; break;
            case 23: this.GetComponent<SpriteRenderer>().sprite = clubJack; break;
            case 24: this.GetComponent<SpriteRenderer>().sprite = clubQueen; break;
            case 25: this.GetComponent<SpriteRenderer>().sprite = clubKing; break;
            //Diamonds
            case 26: this.GetComponent<SpriteRenderer>().sprite = diamondAce; break;
            case 27: this.GetComponent<SpriteRenderer>().sprite = diamondTwo; break;
            case 28: this.GetComponent<SpriteRenderer>().sprite = diamondThree; break;
            case 29: this.GetComponent<SpriteRenderer>().sprite = diamondFour; break;
            case 30: this.GetComponent<SpriteRenderer>().sprite = diamondFive; break;
            case 31: this.GetComponent<SpriteRenderer>().sprite = diamondSix; break;
            case 32: this.GetComponent<SpriteRenderer>().sprite = diamondSeven; break;
            case 33: this.GetComponent<SpriteRenderer>().sprite = diamondEight; break;
            case 34: this.GetComponent<SpriteRenderer>().sprite = diamondNine; break;
            case 35: this.GetComponent<SpriteRenderer>().sprite = diamondTen; break;
            case 36: this.GetComponent<SpriteRenderer>().sprite = diamondJack; break;
            case 37: this.GetComponent<SpriteRenderer>().sprite = diamondQueen; break;
            case 38: this.GetComponent<SpriteRenderer>().sprite = diamondKing; break;
            //Spades
            case 39: this.GetComponent<SpriteRenderer>().sprite = spadeAce; break;
            case 40: this.GetComponent<SpriteRenderer>().sprite = spadeTwo; break;
            case 41: this.GetComponent<SpriteRenderer>().sprite = spadeThree; break;
            case 42: this.GetComponent<SpriteRenderer>().sprite = spadeFour; break;
            case 43: this.GetComponent<SpriteRenderer>().sprite = spadeFive; break;
            case 44: this.GetComponent<SpriteRenderer>().sprite = spadeSix; break;
            case 45: this.GetComponent<SpriteRenderer>().sprite = spadeSeven; break;
            case 46: this.GetComponent<SpriteRenderer>().sprite = spadeEight; break;
            case 47: this.GetComponent<SpriteRenderer>().sprite = spadeNine; break;
            case 48: this.GetComponent<SpriteRenderer>().sprite = spadeTen; break;
            case 49: this.GetComponent<SpriteRenderer>().sprite = spadeJack; break;
            case 50: this.GetComponent<SpriteRenderer>().sprite = spadeQueen; break;
            case 51: this.GetComponent<SpriteRenderer>().sprite = spadeKing; break;
        }
    }
}
