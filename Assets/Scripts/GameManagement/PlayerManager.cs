using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

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
}
