using UnityEngine;
using UnityEditor;

public interface IAiControl
{
    void Update();

    Vector3 GetTargetVector(Transform transform);
}
