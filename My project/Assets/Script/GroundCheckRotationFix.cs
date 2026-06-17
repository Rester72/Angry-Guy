using UnityEngine;

public class GroundCheckRotationFix : MonoBehaviour
{
    void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}