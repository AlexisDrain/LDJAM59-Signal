using UnityEngine;
using UnityEngine.Events;

public class DetectLevelEnd : MonoBehaviour
{
    /* Alexis Clay Drain */

    void Update()
    {
        if(transform.childCount == 0) {
            GameManager.myGameManager.FinishedLevel();
        }
    }

}
