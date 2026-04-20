using UnityEngine;

public class CameraController : MonoBehaviour
{
    /* Alexis Clay Drain */
    public Transform cameraDestination;
    void Start()
    {
        
    }
    public void SetNewCameraDestination(Transform newTrans) {
        cameraDestination = newTrans;
    }
    private void LateUpdate() {
        if(Vector3.Distance(transform.position, cameraDestination.position) > 0.01f) {
            transform.position = Vector3.Lerp(transform.position, cameraDestination.position, 20f * Time.deltaTime);
        }
    }
}
