using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class BallStats : MonoBehaviour
{
    /* Alexis Clay Drain */
    public List<AudioClip> shootSFX = new List<AudioClip>();
    public AudioClip deflectSFX;
    public AudioClip goalPostSFX;
    public AudioClip failSFX;
    public float shootImpulse;
    public Transform target;
    public GameObject fadeSignalText;

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

    public void ShootBall() {
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
    public void BallHitNet() {

        //mySphereCollider.enabled = false;
        //myRigidbody.linearVelocity = Vector3.zero;
        //myRigidbody.AddForce(GameManager.playerTrans.forward, ForceMode.Impulse);
        scored = true;
        Destroy(fadeSignalText);
        StartCoroutine(DisapearCountdown());

        transform.parent.GetComponent<TrajectoryController>().EndShot();

        mySprite.sortingOrder = -100;
        GameManager.SpawnLoudAudio(failSFX, new Vector2(0.9f, 1.2f));
        //myAudioSource.clip = failSFX;
        //myAudioSource.pitch = Random.Range(0.9f, 1.2f);
        //myAudioSource.PlayWebGL();
    }
    public void BallHitGoalPost() {
        if (scored) {
            return;
        }
        Destroy(fadeSignalText);
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
    public void BallCaughtByPlayer() {
        if(scored) {
            return;
        }
        Destroy(fadeSignalText);
        mySphereCollider.enabled = false;
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.AddForce(-Vector3.forward * 4f, ForceMode.Impulse);

        transform.parent.GetComponent<TrajectoryController>().EndShot();

        StartCoroutine(DisapearCountdown());

        GameManager.SpawnLoudAudio(deflectSFX, new Vector2(0.9f, 1.2f));
        // myAudioSource.clip = deflectSFX;
        // myAudioSource.pitch = Random.Range(1.2f, 1.5f);
        // myAudioSource.PlayWebGL();
    }
    private IEnumerator DisapearCountdown() {

        for (int i = 0; i < 10; i++) {
            mySprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            mySprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        // yield return new WaitForSeconds(1f);
        if(fadeSignalText) {
            Destroy(fadeSignalText);
        }
        Destroy(transform.parent.gameObject);
        //transform.parent.gameObject.SetActive(false);
    }
}
