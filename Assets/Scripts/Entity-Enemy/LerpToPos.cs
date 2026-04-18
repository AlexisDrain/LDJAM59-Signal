using UnityEngine;

public class LerpToPos : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Transform targetTrans;
    public float delta = 0.2f;
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetTrans.position, delta);
    }

}
