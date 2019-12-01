using UnityEngine;
using System.Collections;

public class DummyAiControl : IAiControl
{
    public void Update()
    {
        
    }

    public Vector3 GetTargetVector(Transform transform)
    {
        return transform.position + transform.forward;
    }
}
