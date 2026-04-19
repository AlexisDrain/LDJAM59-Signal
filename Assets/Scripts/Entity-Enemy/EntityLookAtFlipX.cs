using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityLookAtFlipX : MonoBehaviour {

    public bool targetIsCamera = true;
    public bool targetIsPlayer = false;
    public Transform targetTransform;
    public Transform graphicalObject;

    public bool instantAnimation = true;
    private bool directionRight = true;
    private float targetScaleX = 1f;
    private float currentScaleX = 1f;

    void Start() {
        if(targetIsCamera) {
            targetTransform = Camera.main.transform;
        }
        if (targetIsPlayer == true) {
            targetTransform = GameManager.playerTrans;
        }
    }

    void LateUpdate() {

        if (directionRight == true && targetTransform.position.x < transform.position.x) {
            directionRight = false;
            targetScaleX = -1f;
        }
        if (directionRight == false && targetTransform.position.x > transform.position.x) {
            directionRight = true;
            targetScaleX = 1f;
        }
        if(instantAnimation == true) {
            graphicalObject.localScale = new Vector3(targetScaleX, 1f, 1f);
        } else {
            currentScaleX = Mathf.Lerp(currentScaleX, targetScaleX, 8f * Time.deltaTime);
            graphicalObject.localScale = new Vector3(currentScaleX, 1f, 1f);

        }
    }
}
