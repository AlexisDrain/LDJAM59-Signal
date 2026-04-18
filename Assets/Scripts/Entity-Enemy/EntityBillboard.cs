using UnityEngine;

public class EntityBillboard : MonoBehaviour
{
    /* Alexis Clay Drain */
    void LateUpdate() {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }

}
