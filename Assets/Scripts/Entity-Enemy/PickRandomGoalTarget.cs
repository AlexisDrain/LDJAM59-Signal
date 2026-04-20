using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickRandomGoalTarget : MonoBehaviour
{
    /* Alexis Clay Drain */

    public float distanceColorOffset = 3f;
    private TrajectoryController trajectoryController;
    private HecklerController hecklerController;
    public Transform trackedObject;
    private SpriteRenderer mySprite;
    private float myAlpha = 1f;
    private bool alwaysShowAlpha = false;
    void Start()
    {
        trajectoryController = transform.parent.GetComponent<TrajectoryController>();
        hecklerController = transform.parent.GetComponent<HecklerController>();
        if(trajectoryController) {
            if(trajectoryController.ball) {
                trackedObject = trajectoryController.ball.transform;
            } else if(trajectoryController.drink) {
                alwaysShowAlpha = true;
                trackedObject = trajectoryController.drink.transform;
            } else {
                print("Unknown thrown object type");
            }
        }
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);


    }

    private float distance;
    private bool _shotEnded;
    private bool _hasShot;
    private void FixedUpdate() {
        if(trajectoryController) {
            _shotEnded = trajectoryController._shotEnded;
            _hasShot = trajectoryController._hasShot;
        } else {
            _shotEnded = hecklerController._shotEnded;
            _hasShot = hecklerController._hasShot;
        }
        if(_shotEnded == false) {
            if (_hasShot == true) {
                if (GameManager.visionPowerUp == true || trackedObject == null || // tackedObject is null in case of Bullet/Heckler
                    alwaysShowAlpha == true) {
                    mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 1f);
                } else {
                    distance = Vector3.Distance(transform.position, trackedObject.position) - distanceColorOffset;
                    if(distance <= 0f) {
                        distance = 0.001f;
                    }
                    myAlpha = Mathf.Clamp(1f / distance, 0f, 1f);
                    mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, myAlpha);

                }
            }
        }
        else if(_shotEnded == true) {
            mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);
        }
    }

}
