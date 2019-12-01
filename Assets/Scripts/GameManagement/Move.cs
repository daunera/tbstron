using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    public KeyCode rightKey;
    public KeyCode leftKey;
    public Vector3 directionVector = new Vector3(0,0,0);

    void Start()
    {
        InvokeRepeating("StepForward", 2.0f, 0.2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(leftKey))
        {
            // TODO
        }
        else if (Input.GetKeyDown(rightKey))
        {
            // TODO
        }
    }

    void StepForward()
    {
        if (gameObject.GetComponent<PlayerInGame>().IsAlive)
        {
            if (!directionVector.Equals(new Vector3(0, 0, 0)))
                GameManager.instance.boardScript.UpdateTile(gameObject, directionVector);
        }
    }
}
