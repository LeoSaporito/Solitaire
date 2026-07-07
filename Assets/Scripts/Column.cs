using UnityEngine;

public class Column : MonoBehaviour
{
    [SerializeField] int xPosition;

    public int GetXPosition()
    {
        return xPosition;
    }
}
