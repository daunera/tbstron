using UnityEngine;
using System.Collections;

public class ManualAiControl : IAiControl
{
    private float rotation = 0;
    private string axis;

    public ManualAiControl(string axis)
    {
        this.axis = axis;
    }

    public void Update()
    {
        if (Input.GetAxisRaw(axis) == 1)
        {
            rotation = 90F;
        }
        else if (Input.GetAxisRaw(axis) == -1)
        {
            rotation = -90F;
        }
    }

    public Vector3 GetTargetVector(Transform transform)
    {
        if (rotation != 0)
        {
            transform.Rotate(0.0f, rotation, 0.0f);
            rotation = 0;
        }

        return transform.position + transform.forward;
    }
}
