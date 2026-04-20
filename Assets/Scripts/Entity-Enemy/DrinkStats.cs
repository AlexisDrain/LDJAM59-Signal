using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class DrinkStats : MonoBehaviour
{
    /* Alexis Clay Drain */
    public List<AudioClip> shootSFX = new List<AudioClip>();
    public AudioClip drinkSFX;
    public AudioClip goalPostSFX;
    public float shootImpulse;
    public Transform target;
    public float backupKillGameObjectTimer = 15f;

    public SpriteRenderer mySprite;
    private bool scored = false;
    private SphereCollider mySphereCollider;
    private Rigidbody myRigidbody;
    private AudioSource myAudioSource;
    void Start() {
        mySphereCollider = GetComponent<SphereCollider>();
        myRigidbody = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
    }

    public void ShootDrink() {
        mySphereCollider.enabled = true;
        Vector3 direction = (target.position - transform.position).normalized;
        myRigidbody.AddForce(direction * shootImpulse, ForceMode.Impulse);
        //GameObject missile = GameManager.pool_EnemyMissile.Spawn(bulletStart.position);
        // missile.GetComponent<Rigidbody>().AddForce(direction * shootImpulse, ForceMode.Impulse);
        //missile.GetComponent<BulletStats>().direction = direction;

        int idx = Random.Range(0, shootSFX.Count);
        myAudioSource.clip = shootSFX[idx];
        myAudioSource.pitch = Random.Range(0.9f, 1.2f);
        myAudioSource.PlayWebGL();
    }
    public void DrinkHitNet() {

        //mySphereCollider.enabled = false;
        //myRigidbody.linearVelocity = Vector3.zero;
        //myRigidbody.AddForce(GameManager.playerTrans.forward, ForceMode.Impulse);
        scored = true;
        StartCoroutine(DisapearCountdown());

        transform.parent.GetComponent<TrajectoryController>().EndShot();

        mySprite.sortingOrder = -3;

        //myAudioSource.clip = failSFX;
        //myAudioSource.pitch = Random.Range(0.9f, 1.2f);
        //myAudioSource.PlayWebGL();
    }
    public void DrinkHitGoalPost() {
        if (scored) {
            return;
        }
        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);

        transform.parent.GetComponent<TrajectoryController>().EndShot();

        StartCoroutine(DisapearCountdown());

        GameManager.SpawnLoudAudio(goalPostSFX, new Vector2(0.9f, 1.2f));
        // myAudioSource.clip = deflectSFX;
        // myAudioSource.pitch = Random.Range(1.2f, 1.5f);
        // myAudioSource.PlayWebGL();
    }
    public void DrinkCaughtByPlayer() {
        if(scored) {
            return;
        }
        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);

        transform.parent.GetComponent<TrajectoryController>().EndShot();
        GameManager.SpawnLoudAudio(drinkSFX, new Vector2(0.9f, 1.2f));

        GameManager.myGameManager.ToggleDrinkPowerUp(true);

        Destroy(gameObject);

        // myAudioSource.clip = deflectSFX;
        // myAudioSource.pitch = Random.Range(1.2f, 1.5f);
        // myAudioSource.PlayWebGL();
    }

    private bool backupKillGameObjectTimerActive = true;
    private void FixedUpdate() {
        if(backupKillGameObjectTimer > 0f && backupKillGameObjectTimerActive == true) {
            backupKillGameObjectTimer -= Time.deltaTime;
        } else {
            backupKillGameObjectTimerActive = false;
            StartCoroutine(DisapearCountdown());
        }
    }
    private IEnumerator DisapearCountdown() {

        for (int i = 0; i < 10; i++) {
            mySprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            mySprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        // yield return new WaitForSeconds(1f);
        Destroy(transform.parent.gameObject);
        //transform.parent.gameObject.SetActive(false);
    }
}
