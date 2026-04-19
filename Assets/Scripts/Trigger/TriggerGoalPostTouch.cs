using UnityEngine;
using UnityEngine.Events;

public class TriggerGoalPostTouch : MonoBehaviour
{
    /* Alexis Clay Drain */
    public UnityEvent onGoalPostTouch;
    public bool canBeReTriggered = false;

    private bool hasBeenTriggered = false;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        if(canBeReTriggered == false && hasBeenTriggered == true) {
            return;
        }
        if(collision.collider.CompareTag("GoalPost")) {
            hasBeenTriggered = true;
            onGoalPostTouch.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other) {

        if (canBeReTriggered == false && hasBeenTriggered == true) {
            return;
        }
        if (other.CompareTag("GoalPost")) {
            hasBeenTriggered = true;
            onGoalPostTouch.Invoke();
        }
    }
}
