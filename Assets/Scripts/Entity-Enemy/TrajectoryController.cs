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
    public bool redText = false;
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

        int maxPossibleGoalTarget = 10;
        if(GameManager.tallLevel) {
            maxPossibleGoalTarget = 15;
        }

        int randomIndex = Random.Range(0, maxPossibleGoalTarget); // max is 10 or 15
        if(onlyBottomRow) {
            if(GameManager.tallLevel) {
                randomIndex = Random.Range(10, maxPossibleGoalTarget);
            } else {
                randomIndex = Random.Range(5, maxPossibleGoalTarget);
            }
        }
        if(indexToShootAt != -1) {
            randomIndex = indexToShootAt;
        }
        Vector3 randomizedOffset = new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), 0f);
        if(onlyBottomRow) { // make fans shoot a little closer to the ground by decreasing Y offset range
            randomizedOffset = new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.1f), 0f);
        }
        Transform square = GameManager.myGameManager.possibleGoalTargets[randomIndex];

        targetReticle.position = square.position + randomizedOffset;

        if (reverseShots == true) { // reverse shown shot.
            square = GameManager.myGameManager.possibleGoalTargets[maxPossibleGoalTarget - randomIndex - 1];
            //List<Transform> reversedList = new List<Transform>(GameManager.myGameManager.possibleGoalTargets);
            //reversedList.Reverse();
            //square = reversedList[randomIndex];
        }


        if(textObj) {
            textObj.GetComponent<TextMeshProUGUI>().text = square.GetComponent<SquareProperties>().squareName;
            if(reverseShots == true) {
                textObj.GetComponent<TextMeshProUGUI>().text = "!" + textObj.GetComponent<TextMeshProUGUI>().text;
            }
            Color newColor = square.GetComponent<SquareProperties>().squareColor;
            newColor.a = 1f;
            if(redText == true) {
                textObj.GetComponent<TextMeshProUGUI>().color = Color.black;
            } else {
                textObj.GetComponent<TextMeshProUGUI>().color = newColor;
            }
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
