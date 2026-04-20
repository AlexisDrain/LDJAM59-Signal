using UnityEngine;

public class ShadowPosition : MonoBehaviour
{
    /* Alexis Clay Drain */
    public float maxSize = 1.0f;
    void Start()
    {
        
    }
    private float distance = 0f;
    float scaleAxis = 1f;
    private void LateUpdate() {

        RaycastHit hit;
        Physics.Linecast(transform.position, transform.position + new Vector3(0f, -15f, 0f), out hit, (1 << LayerMask.NameToLayer("Floor")));
        if (hit.collider) {
            if (hit.collider.CompareTag("Floor")) {
                transform.position = hit.point + new Vector3(0f, 0.1f, 0f);
                distance = Mathf.Abs(transform.parent.position.y - transform.position.y) + 0.4f;
                scaleAxis = Mathf.Clamp(1f / distance, 0.1f, maxSize);
                transform.localScale = new Vector3(scaleAxis, scaleAxis, scaleAxis);
            }
        }
        
    }
}
