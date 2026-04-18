using UnityEngine;
using UnityEngine.Events;

public class TriggerEnterGoal : MonoBehaviour
{
    /* Alexis Clay Drain */
    [Header("Automatically handles Ball events")]
    public UnityEvent goalEvent;
    public UnityEvent regularEvent;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Ball")) {
            other.GetComponent<BallStats>().BallHitNet();
            goalEvent.Invoke();
        } else {
            regularEvent.Invoke();
        }
    }
}
