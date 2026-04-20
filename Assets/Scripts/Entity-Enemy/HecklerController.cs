using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HecklerController : MonoBehaviour
{
    /* Alexis Clay Drain */

    public float shootCountdown = 2f;
    [Header("Set once")]
    public List<AudioClip> shootSFX = new List<AudioClip>();
    // public GameObject signalTextPrefab;
    public GameObject bulletPrefab;
    public Transform targetReticle;
    public SpriteRenderer humanSpriteRenderer; // do we need this?
    public Transform bulletStartTrans;
    // public Sprite shotSprite;
    private GameObject textObj;
    public bool _hasShot = false;
    public bool _shotEnded = false;
    private AudioSource myAudioSource;
    void Start() {

        //int randomIndex = Random.Range(0, GameManager.myGameManager.possibleGoalTargets.Count);
        //Vector3 randomizedOffset = new Vector3(Random.Range(-0.9f, 0.9f), Random.Range(-0.9f, 0.9f), 0f);
        //Transform square = GameManager.myGameManager.possibleGoalTargets[randomIndex];

        // targetReticle.position = square.position + randomizedOffset;
        myAudioSource = GetComponent<AudioSource>();
        targetReticle.position = GameManager.playerTrans.position + new Vector3(0f, 1f, 0f);

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
                // humanSpriteRenderer.sprite = shotSprite;
                StartCoroutine(OneShootAction());
            }

        }
    }
    private IEnumerator ThreeShootAction() {
        yield return new WaitForSeconds(0.75f); // give player warning where reticle is shown

        for (int i = 0; i <= 3; i++) {
            int randIdx = Random.Range(0, shootSFX.Count);
            myAudioSource.clip = shootSFX[randIdx];
            myAudioSource.Play();
            GameObject bullet = GameObject.Instantiate(bulletPrefab);
            bullet.GetComponent<Rigidbody>().position = bulletStartTrans.position;
            bullet.transform.position = bulletStartTrans.position;
            bullet.GetComponent<BulletStats>().target = targetReticle;
            bullet.GetComponent<BulletStats>().ShootBullet();
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
    private IEnumerator OneShootAction() {
        yield return new WaitForSeconds(0.75f); // give player warning where reticle is shown

        int randIdx = Random.Range(0, shootSFX.Count);
        myAudioSource.clip = shootSFX[randIdx];
        myAudioSource.Play();
        GameObject bullet = GameObject.Instantiate(bulletPrefab);
        bullet.GetComponent<Rigidbody>().position = bulletStartTrans.position;
        bullet.transform.position = bulletStartTrans.position;
        bullet.GetComponent<BulletStats>().target = targetReticle;
        bullet.GetComponent<BulletStats>().ShootBullet();
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
