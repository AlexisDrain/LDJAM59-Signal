using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrajectoryController : MonoBehaviour
{
    /* Alexis Clay Drain */

    public float shootCountdown = 3f;
    [Header("Set once")]
    public GameObject signalTextPrefab;
    public BallStats ball;
    public Transform targetReticle;
    public SpriteRenderer humanSpriteRenderer;
    public Sprite shotSprite;
    private GameObject textObj;
    public bool _hasShot = false;
    public bool _shotEnded = false;
    void Start() {
        textObj = GameObject.Instantiate(signalTextPrefab, GameManager.canvasWorld);
        textObj.GetComponent<FadeSignalText>().targetTrans = targetReticle;
        textObj.GetComponent<FollowObject>().targetTrans = ball.transform;
        ball.fadeSignalText = textObj;

        int randomIndex = Random.Range(0, GameManager.myGameManager.possibleGoalTargets.Count);
        Vector3 randomizedOffset = new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), 0f);
        Transform square = GameManager.myGameManager.possibleGoalTargets[randomIndex];

        targetReticle.position = square.position + randomizedOffset;

        textObj.GetComponent<TextMeshProUGUI>().text = square.GetComponent<SquareProperties>().squareName;
        Color newColor = square.GetComponent<SquareProperties>().squareColor;
        newColor.a = 1f;
        textObj.GetComponent<TextMeshProUGUI>().color = newColor;

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
                ball.ShootBall();
            }

        }
    }
}
