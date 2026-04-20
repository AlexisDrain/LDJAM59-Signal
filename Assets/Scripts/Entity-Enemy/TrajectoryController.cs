using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    /* Alexis Clay Drain */

    public float shootCountdown = 3f;
    public int indexToShootAt = -1;
    [Header("Set once")]
    public GameObject signalTextPrefab;
    public BallStats ball;
    public DrinkStats drink;
    public Transform targetReticle;
    public SpriteRenderer humanSpriteRenderer;
    public Sprite shotSprite;
    private GameObject textObj;
    public bool reverseShots = false;
    public bool showText = true; // green enemy doesn't showText
    public bool onlyBottomRow = false;
    [Header("read only")]
    public bool _hasShot = false;
    public bool _shotEnded = false;
    void Start() {
        if(ball) {
            textObj = GameObject.Instantiate(signalTextPrefab, GameManager.canvasWorld);
            textObj.GetComponent<FadeSignalText>().targetTrans = targetReticle;
            textObj.GetComponent<FollowObject>().targetTrans = ball.transform;
            ball.fadeSignalText = textObj;
        }

        int randomIndex = Random.Range(0, GameManager.myGameManager.possibleGoalTargets.Count);
        if(onlyBottomRow) {
            randomIndex = Random.Range(5, GameManager.myGameManager.possibleGoalTargets.Count);
        }
        if(indexToShootAt != -1) {
            randomIndex = indexToShootAt;
        }
        Vector3 randomizedOffset = new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), 0f);
        Transform square = GameManager.myGameManager.possibleGoalTargets[randomIndex];

        if(reverseShots == true) {
            List<Transform> reversedList = new List<Transform>(GameManager.myGameManager.possibleGoalTargets);
            reversedList.Reverse();
            square = reversedList[randomIndex];
        }

        targetReticle.position = square.position + randomizedOffset;

        if(textObj) {
            textObj.GetComponent<TextMeshProUGUI>().text = square.GetComponent<SquareProperties>().squareName;
            Color newColor = square.GetComponent<SquareProperties>().squareColor;
            newColor.a = 1f;
            textObj.GetComponent<TextMeshProUGUI>().color = newColor;
            if(showText == false) {
                textObj.GetComponent<TextMeshProUGUI>().text = "";
            }
        }

        _hasShot = false;
    }
    public void EndShot() {
        _shotEnded = true;
    }
    private void FixedUpdate() {
        if (_hasShot == false) {
            if (shootCountdown >= 0) {
                shootCountdown -= Time.deltaTime;
            } else {
                _hasShot = true;
                humanSpriteRenderer.sprite = shotSprite;
                if(ball) {
                    ball.ShootBall();
                }
                else if (drink) {
                    drink.ShootDrink();
                }
            }

        }
    }
}
