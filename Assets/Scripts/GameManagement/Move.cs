using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Dictionary<int, string> playerAxis = new Dictionary<int, string>
    {
        { 1, "Player1" },
        { 2, "Guest1" },
        { 3, "Guest2" },
        { 4, "Guest3" }
    };

    public string axis = null;
    private float rotation = 0;

    void Start()
    {
        InvokeRepeating(nameof(StepForward), 2.0f, 1f);
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

            GameManager.instance.boardScript.UpdateTile(gameObject, transform.position + transform.forward);
        }
        else
        {
            CancelInvoke(nameof(StepForward));
        }
    }
}
