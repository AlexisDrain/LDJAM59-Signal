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
            if(other.GetComponent<BallStats>()) {
                other.GetComponent<BallStats>().BallHitNet();
            }
            // if (other.GetComponent<BulletStats>()) {
            //     other.GetComponent<BulletStats>().BulletHitNet();
            // }
            goalEvent.Invoke();
        } else {
            regularEvent.Invoke();
        }
    }
}
