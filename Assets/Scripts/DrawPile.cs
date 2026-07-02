using UnityEngine;

public class DrawPile : MonoBehaviour
{
    public int xPosition;
    public int yPosition;
    public int offsetX;
    public int offsetY;
    
    public void SetSpacing()
    {
        int x = xPosition;
        int y = yPosition;
        
        x += offsetX;
        y += offsetY;

        transform.position = new Vector2(x, y);
    }
}
