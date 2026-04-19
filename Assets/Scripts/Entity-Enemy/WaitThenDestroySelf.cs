using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class WaitThenDestroySelf : MonoBehaviour
{
    /* Alexis Clay Drain */
    public float timeToWait = 3f;
    public GameObject spawnGameObject;
    public UnityEvent onTimeEnd;

    void Start() {
    }
    private void OnEnable() {
        StartCoroutine("Countdown");
    }

    public IEnumerator Countdown() {
        yield return new WaitForSeconds(timeToWait);
        spawnGameObject.transform.parent = transform.parent;
        spawnGameObject.gameObject.SetActive(true);
        onTimeEnd.Invoke();
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }

}
