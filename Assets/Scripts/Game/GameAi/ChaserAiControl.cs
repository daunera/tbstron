using UnityEngine;
using System.Collections;
using Assets.Scripts.Pathfinding;

public class ChaserAiControl : IAiControl
{
    private BoardManager boardManager;
    private SurvivalAiControl backupControl;

    public void Update()
    {

    }

    public ChaserAiControl(BoardManager boardManager)
    {
        this.boardManager = boardManager;
        backupControl = new SurvivalAiControl(boardManager);
    }

    public Vector3 GetTargetVector(Transform transform)
    {
        Vector3? target = PathFinder.FindPath(boardManager.tiles,
            transform.position, boardManager.players[0].transform.position,
            transform.position + transform.forward * -1);

        if (!target.HasValue)
        {
            target = backupControl.GetTargetVector(transform);
        }

        transform.forward = target.Value - transform.position;

        return transform.position + transform.forward;
    }
}
