using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PickRandomGoalTarget : MonoBehaviour
{
    /* Alexis Clay Drain */

    public float distanceColorOffset = 3f;
    private TrajectoryController trajectoryController;
    private SpriteRenderer mySprite;
    private float myAlpha = 1f;
    void Start()
    {
        trajectoryController = transform.parent.GetComponent<TrajectoryController>();
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);


    }

    private float distance;
    private void FixedUpdate() {
        if(trajectoryController._shotEnded == false) {
            if(GameManager.visionPowerUp == true) {
                mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 1f);
            }
            else if (trajectoryController._hasShot == true) {
                distance = Vector3.Distance(transform.position, trajectoryController.ball.transform.position) - distanceColorOffset;
                if(distance <= 0f) {
                    distance = 0.001f;
                }
                myAlpha = Mathf.Clamp(1f / distance, 0f, 1f);
                mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, myAlpha);
            }
        }
        else if(trajectoryController._shotEnded == true) {
            mySprite.color = new Color(mySprite.color.r, mySprite.color.g, mySprite.color.b, 0f);
        }
    }

}
