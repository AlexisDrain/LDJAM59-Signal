using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerTouch : MonoBehaviour
{
    /* Alexis Clay Drain */
    public UnityEvent onPlayerTouch;
    public bool canBeReTriggered = false;

    private bool hasBeenTriggered = false;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        if(canBeReTriggered == false && hasBeenTriggered == true) {
            return;
        }
        if(collision.collider.CompareTag("Player")) {
            hasBeenTriggered = true;
            onPlayerTouch.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other) {

        if (canBeReTriggered == false && hasBeenTriggered == true) {
            return;
        }
        if (other.CompareTag("Player")) {
            hasBeenTriggered = true;
            onPlayerTouch.Invoke();
        }
    }
}
