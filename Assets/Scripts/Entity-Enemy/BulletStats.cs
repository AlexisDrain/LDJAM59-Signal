using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class BulletStats : MonoBehaviour
{
    /* Alexis Clay Drain */
    public AudioClip goalPostSFX;
    public AudioClip hitOtherSFX;
    public float shootImpulse;
    public Transform target;
    public float backupKillGameObjectTimer = 5f;

    public GameObject graphics;
    private bool scored = false;
    public SphereCollider mySphereCollider;
    public Rigidbody myRigidbody;
    void Start() {
        mySphereCollider = GetComponent<SphereCollider>();
        myRigidbody = GetComponent<Rigidbody>();

        GameManager.levelEndEvent.AddListener(() => Destroy(gameObject));
    }

    public void ShootBullet() {
        mySphereCollider.enabled = true;
        Vector3 direction = (target.position - transform.position).normalized; // Quaternion.Euler(new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), 0f)) *

        myRigidbody.AddForce(direction * shootImpulse, ForceMode.Impulse);
        //GameObject missile = GameManager.pool_EnemyMissile.Spawn(bulletStart.position);
        // missile.GetComponent<Rigidbody>().AddForce(direction * shootImpulse, ForceMode.Impulse);
        //missile.GetComponent<BulletStats>().direction = direction;

    }
    public void BulletHitNet() {

        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(GameManager.playerTrans.forward, ForceMode.Impulse);
        scored = true;

        Destroy(gameObject);
        //StartCoroutine(DisapearCountdown());

        // mySprite.sortingOrder = -3;
        //myAudioSource.clip = failSFX;
        //myAudioSource.pitch = Random.Range(0.9f, 1.2f);
        //myAudioSource.PlayWebGL();
    }
    public void BallHitGoalPost() {
        if (scored) {
            return;
        }
        scored = true;
        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);

        // Destroy(gameObject);
        StartCoroutine(DisapearCountdown());

        GameManager.SpawnLoudAudio(goalPostSFX, new Vector2(0.9f, 1.2f));
        // myAudioSource.clip = deflectSFX;
        // myAudioSource.pitch = Random.Range(1.2f, 1.5f);
        // myAudioSource.PlayWebGL();
    }
    public void BulletHitOther() {

        scored = true;
        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;

        Destroy(gameObject);
        //StartCoroutine(DisapearCountdown());

        GameManager.SpawnLoudAudio(hitOtherSFX, new Vector2(0.9f, 1.2f));
    }
    public void BulletHitPlayer() {
        if(scored) {
            return;
        }
        scored = true;
        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);

        // transform.parent.GetComponent<HecklerController>().EndShot();
        GameManager.myPlayerHealth.PlayerLoseHealth();

        Destroy(gameObject);
        //StartCoroutine(DisapearCountdown());

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
            // StartCoroutine(DisapearCountdown());
            Destroy(gameObject); // destroy immedietly.
        }
    }
    private IEnumerator DisapearCountdown() {


        for (int i = 0; i < 5; i++) {
            graphics.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            graphics.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
        // yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        //transform.parent.gameObject.SetActive(false);
    }
}
