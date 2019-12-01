using UnityEngine;
using System.Collections;


public enum enTileType
{
    Empty,
    Wall
}

public class TileBehaviour : MonoBehaviour
{
    public enTileType type = enTileType.Empty;

    public void SetType(enTileType type, Color wallColor)
    {
        this.type = type;

        switch (type)
        {
            case enTileType.Empty:
                GetComponent<SpriteRenderer>().color = new Color(66, 127, 255);
                break;
            case enTileType.Wall:
                GetComponent<SpriteRenderer>().color = wallColor;
                break;
            default:
                break;
        }
    }
}
