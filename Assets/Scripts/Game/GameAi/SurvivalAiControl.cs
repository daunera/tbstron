using UnityEngine;
using System.Collections;

public class SurvivalAiControl : IAiControl
{
    BoardManager boardManager;

    public SurvivalAiControl(BoardManager boardManager)
    {
        this.boardManager = boardManager;
    }

    public void Update()
    {
        
    }

    public Vector3 GetTargetVector(Transform transform)
    {
        Vector3 target = transform.position + transform.forward;
        int x = (int)target.x;
        int y = (int)target.y;

        if (boardManager.tiles[x, y].type != enTileType.Empty)
        {
            transform.Rotate(0.0f, -90F, 0.0f);
        }

        target = transform.position + transform.forward;
        x = (int)target.x;
        y = (int)target.y;

        if (boardManager.tiles[x, y].type != enTileType.Empty)
        {
            transform.Rotate(0.0f, 180F, 0.0f);
        }

        return transform.position + transform.forward;
    }
}
