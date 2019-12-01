using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class PlayerManager : MonoBehaviour
{
    public Player Player;

    public static PlayerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void AchievementProgress(enAchievementType type)
    {
        foreach (Achievement achievement in Player.Achievements.Where(x => x.AchievementType == type))
        {
            achievement.Progress++;
        }
    }
}
