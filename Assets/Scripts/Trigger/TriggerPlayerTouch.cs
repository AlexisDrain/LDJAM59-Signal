using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerTouch : MonoBehaviour
{
    /* Alexis Clay Drain */
    public UnityEvent onPlayerTouch;
    public UnityEvent onPlayerTriggerTouch;
    public UnityEvent onGoalPostTouch;
    public UnityEvent onGoalTouch;
    public UnityEvent onTouchOther;
    // public bool canBeReTriggered = false;

    // private bool hasBeenTriggered = false;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision) {
        // if(canBeReTriggered == false && hasBeenTriggered == true) {
        //     return;
        // }
        if(collision.collider) {
            if(collision.collider.CompareTag("Player")) {
                //hasBeenTriggered = true;
                onPlayerTouch.Invoke();
            }
            else if (collision.collider.CompareTag("PlayerTrigger")) {
                //hasBeenTriggered = true;
                onPlayerTriggerTouch.Invoke();
            }

            else if (collision.collider.CompareTag("GoalPost")) {
                //hasBeenTriggered = true;
                onGoalPostTouch.Invoke();
            } else if (collision.collider.CompareTag("Goal")) {
                //hasBeenTriggered = true;
                onGoalTouch.Invoke();
            } else {
                //hasBeenTriggered = true;
                onTouchOther.Invoke();
            }

        }
    }
    private void OnTriggerEnter(Collider other) {

        // if (canBeReTriggered == false && hasBeenTriggered == true) {
        //     return;
        // }
        if(other) {
            if (other.CompareTag("Player")) {
                //hasBeenTriggered = true;
                onPlayerTouch.Invoke();
            }
            else if (other.CompareTag("PlayerTrigger")) {
                //hasBeenTriggered = true;
                onPlayerTriggerTouch.Invoke();
            } else if (other.CompareTag("GoalPost")) {
                //hasBeenTriggered = true;
                onGoalPostTouch.Invoke();
            } else if (other.CompareTag("Goal")) {
                //hasBeenTriggered = true;
                onGoalTouch.Invoke();
            } else {

                onTouchOther.Invoke();
            }

        }
    }
}
