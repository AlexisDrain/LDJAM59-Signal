using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Alexis Clay Drain */
    public static bool startedGame = false;
    public static PlayerInputAction playerInputAction;
    public static Pool pool_LoudAudioSource;

    public static GameObject startMenu;
    public static GameObject creditsMenu;
    public static Transform playerTrans;
    public static Camera mainCamera;

    void Awake() {
        GameObject.Find("Canvas/CreditsMenu/GameVersion").GetComponent<TextMeshProUGUI>().text = $"Version: {Application.version.ToString()}";
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();

        Time.timeScale = 0f;

        startMenu = GameObject.Find("Canvas/StartMenu");
        creditsMenu = GameObject.Find("Canvas/CreditsMenu");
        creditsMenu.SetActive(false);

        playerTrans = GameObject.Find("Player").transform;
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    public void StartGame() {
        Time.timeScale = 1f;
        startedGame = true;
        startMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
    }
    public void PauseGame() {
        Time.timeScale = 0f;
        startMenu.gameObject.SetActive(true);
        creditsMenu.gameObject.SetActive(false);
    }
    public void ResumeGame() {
        Time.timeScale = 1f;
        startMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
    }


    public void Update() {
        if(GameManager.playerInputAction.Player.Pause.WasPressedThisFrame()) {
            if (startedGame == true) {
                if (startMenu.activeSelf == true) {
                    ResumeGame();
                }
                //else if(creditsMenu.activeSelf == true) {
                //    PauseGame();
                //}
                else {
                    PauseGame();
                }
            }
        }
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
