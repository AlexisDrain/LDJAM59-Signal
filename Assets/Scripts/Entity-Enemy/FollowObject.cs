using UnityEngine;

public class FollowObject : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Vector3 offset = Vector3.zero;
    public Transform targetTrans;
    void LateUpdate()
    {
        transform.position = targetTrans.position + offset;
    }

}
