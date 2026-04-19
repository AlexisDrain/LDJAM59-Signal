using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveOnClick : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Update() {
        if (GameManager.playerInputAction.UI.Click.WasPressedThisFrame()) {
            // Fixes the following bug on WebGL:
            // Unity Bug: if player clicks on splashscreen and mouse lands on a button in the main menu: button is triggered.
            StartCoroutine(DestoryObjectCountdown());
        }
    }
    private IEnumerator DestoryObjectCountdown() {
        yield return new WaitForSecondsRealtime(0.2f);
        DestroyObject();
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
