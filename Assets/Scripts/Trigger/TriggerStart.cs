using UnityEngine;
using UnityEngine.Events;

public class TriggerStart : MonoBehaviour
{
    /* Alexis Clay Drain */
    public UnityEvent onStart;
    public UnityEvent onEnable;
    void Start()
    {
        onStart.Invoke();
    }
    private void OnEnable() {
        onEnable.Invoke();
    }

}
