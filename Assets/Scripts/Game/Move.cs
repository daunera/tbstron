using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float rotation = 0;
    private PlayerInGame playerInGame;

    public static Dictionary<int, string> playerAxis = new Dictionary<int, string>
    {
        { 0, "Player1" },
        { 1, "Guest1" },
        { 2, "Guest2" },
        { 3, "Guest3" }
    };  
    public string axis = null;


    void Start()
    {
        InvokeRepeating(nameof(StepForward), 2.0f, 1f);
        playerInGame = gameObject.GetComponent<PlayerInGame>();
    }

    void Update()
    {
        if (!string.IsNullOrEmpty(axis))
        {
            if (Input.GetAxisRaw(axis) == 1)
            {
                rotation = 90F;
            }
            if (Input.GetAxisRaw(axis) == -1)
            {
                rotation = -90F;
            }
        }
    }

    void StepForward()
    {
        if (gameObject.GetComponent<PlayerInGame>().IsAlive)
        {
            if (rotation != 0)
            {
                transform.Rotate(0.0f, rotation, 0.0f);
                rotation = 0;
            }

            GameManager.instance.boardScript.UpdateTile(playerInGame, transform.position + transform.forward);
        }
        else
        {
            CancelInvoke(nameof(StepForward));
        }
    }
}
