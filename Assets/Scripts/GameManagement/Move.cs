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
    private Vector3 pos;
    private int power = 1;

    // Start is called before the first frame update
    void Start()
    {
        pos = gameObject.transform.position;
        InvokeRepeating("StepForward", 2.0f, 0.2f);
    }

    void Update()
    {
        if (Input.GetKeyDown(upKey))
        {
            directionVector = new Vector3(0, 1 * power, 0);
        }
        else if (Input.GetKeyDown(downKey))
        {
            directionVector = new Vector3(0, -1 * power, 0);
        }
        else if (Input.GetKeyDown(rightKey))
        {
            directionVector = new Vector3(1 * power, 0, 0);
        }
        else if (Input.GetKeyDown(leftKey))
        {
            directionVector = new Vector3(-1* power, 0, 0);
        }
    }

    void StepForward()
    {
        gameObject.transform.position += directionVector;
        if (!pos.Equals(gameObject.transform.position)) {
            GameManager.instance.boardScript.ChangeTile(gameObject, directionVector);
        }
        // todo: collusion detect
    }

    private void OnTriggerEnter2D(Collider2D co)
    {
            Debug.Log("Collision! "+co.name+"("+co.transform.position.x+","+co.transform.position.y
                +") Player("+gameObject.transform.position.x+","+gameObject.transform.position.x +")" );
    }
}
