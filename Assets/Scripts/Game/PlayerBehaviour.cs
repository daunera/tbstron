using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public static Dictionary<int, string> playerAxis = new Dictionary<int, string>
    {
        { 0, "Player1" },
        { 1, "Guest1" },
        { 2, "Guest2" },
        { 3, "Guest3" }
    };

    public bool IsAlive;
    public bool IsEnemy;
    private IAiControl AiControl;

    void Update()
    {
        if (IsAlive)
        {
            AiControl.Update();
        }
    }

    internal Vector3 GetTargetVector()
    {
        return AiControl.GetTargetVector(transform);
    }

    internal void Initialize(bool isEnemy, string axis, int aiControlIdId, BoardManager boardManager)
    {
        IsAlive = true;
        IsEnemy = isEnemy;

        LoadAi(axis, aiControlIdId, boardManager);
    }

    private void LoadAi(string axis, int aiControlIdId, BoardManager boardManager)
    {
        switch (aiControlIdId)
        {
            case 0:
                AiControl = new ManualAiControl(axis);
                break;

            case 1:
                AiControl = new DummyAiControl();
                break;

            case 2:
                AiControl = new SurvivalAiControl(boardManager);
                break;

            case 3:
                AiControl = new ChaserAiControl(boardManager);
                break;

            default:
                AiControl = new DummyAiControl();
                break;
        }
    }
}
