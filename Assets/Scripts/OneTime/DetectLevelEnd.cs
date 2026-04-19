
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectLevelEnd : MonoBehaviour
{
    /* Alexis Clay Drain */
    public List<AudioClip> levelEndSFX = new List<AudioClip>();

    private bool levelHasEnded = false;
    void Update()
    {
        if(transform.childCount == 0 && levelHasEnded == false) {
            levelHasEnded = true;
            int idx = Random.Range(0, levelEndSFX.Count);
            GameManager.SpawnLoudAudio(levelEndSFX[idx]);
            GameManager.myGameManager.FinishedLevel();
        }
    }

}
