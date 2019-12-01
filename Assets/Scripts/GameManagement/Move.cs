using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;

    private Vector3 directionVector = new Vector3(0,0,0);

    void Start()
    {
        InvokeRepeating("StepForward", 2.0f, 0.2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            directionVector = new Vector3(0, 1, 0);
        }
        else if (Input.GetKeyDown(downKey))
        {
            directionVector = new Vector3(0, -1, 0);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            directionVector = new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(leftKey))
        {
            directionVector = new Vector3(-1, 0, 0);
        }
    }

    void StepForward()
    {
        if(!directionVector.Equals(new Vector3(0,0,0)))
            GameManager.instance.boardScript.UpdateTile(gameObject, directionVector);
    }
}
