using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerTouch : MonoBehaviour
{
    /* Alexis Clay Drain */
    public UnityEvent onPlayerTouch;
    public UnityEvent onPlayerTriggerTouch;
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
        if (collision.collider.CompareTag("PlayerTrigger")) {
            hasBeenTriggered = true;
            onPlayerTriggerTouch.Invoke();
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
        if (other.CompareTag("PlayerTrigger")) {
            hasBeenTriggered = true;
            onPlayerTriggerTouch.Invoke();
        }
    }
}
