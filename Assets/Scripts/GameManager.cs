using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Alexis Clay Drain */

    public static PlayerInputAction playerInputAction;
    public static Pool pool_LoudAudioSource;
    void Awake()
    {
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();
        Time.timeScale = 0f;
    }

    public void StartGame() {
        Time.timeScale = 1f;
    }

    public static AudioSource SpawnLoudAudio(AudioClip newAudioClip, Vector2 pitch = new Vector2(), float newVolume = 1f) {

        if (newAudioClip == null) {
            return null;
        }

        float sfxPitch;
        if (pitch.x <= 0.1f) {
            sfxPitch = 1;
        } else {
            sfxPitch = Random.Range(pitch.x, pitch.y);
        }

        AudioSource audioObject = pool_LoudAudioSource.Spawn(new Vector3(0f, 0f, 0f)).GetComponent<AudioSource>();
        audioObject.GetComponent<AudioSource>().pitch = sfxPitch;
        audioObject.GetComponent<AudioSource>().clip = newAudioClip;
        audioObject.GetComponent<AudioSource>().volume = newVolume;
        audioObject.ignoreListenerPause = true; // TESTING
        audioObject.Play();
        return audioObject;
        // audio object will set itself to inactive after done playing.
    }
}
