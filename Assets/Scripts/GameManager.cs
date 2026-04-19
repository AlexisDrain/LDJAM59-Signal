using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    /* Alexis Clay Drain */
    public static bool visionPowerUp = false;
    public static bool startedGame = false;
    public static int currentLevel = 1;
    public GameObject currentLevelInst;

    public static PlayerInputAction playerInputAction;
    public static Pool pool_LoudAudioSource;

    public static GameManager myGameManager;
    public static Transform canvasWorld;
    public static GameObject startMenu;
    public static GameObject plotMenu;
    public static GameObject creditsMenu;
    public static GameObject splashScreen;
    public static GameObject tutorialBox;
    public static TextMeshProUGUI plotText;
    public static Transform playerTrans;
    public static Camera mainCamera;
    public static GameObject graphicsPlayerArrow;

    public static Transform trajectoryStarts;

    public List<AudioClip> crowdCheerStartGame = new List<AudioClip>();

    public List<Transform> possibleGoalTargets = new List<Transform>();
    public List<GameObject> levels = new List<GameObject>();

    void Awake() {
        GameObject.Find("Canvas/CreditsMenu/GameVersion").GetComponent<TextMeshProUGUI>().text = $"Version: {Application.version.ToString()}";
        playerInputAction = new PlayerInputAction();
        playerInputAction.Enable();
        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();

        Time.timeScale = 0f;
        myGameManager = GetComponent<GameManager>();
        canvasWorld = GameObject.Find("CanvasWorld").transform;
        startMenu = GameObject.Find("Canvas/StartMenu");
        plotText = GameObject.Find("Canvas/PlotMenu/PlotBox/Text (TMP)").GetComponent<TextMeshProUGUI>();
        plotMenu = GameObject.Find("Canvas/PlotMenu");
        plotMenu.SetActive(false);
        creditsMenu = GameObject.Find("Canvas/CreditsMenu");
        creditsMenu.SetActive(false);
        splashScreen = GameObject.Find("Canvas/SplashScreen");
        if(splashScreen == null) {
            Debug.LogError("Forgot to enable splash screen");
        }
        tutorialBox = GameObject.Find("Canvas/GameMenu/TutorialBox");
        tutorialBox.SetActive(false);

        playerTrans = GameObject.Find("Player").transform;
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        graphicsPlayerArrow = GameObject.Find("GraphicsPlayerArrow");

        trajectoryStarts = GameObject.Find("TrajectoryStarts").transform;
    }

    public void StartGame() {
        Time.timeScale = 0f;
        startedGame = true;
        startMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        plotMenu.gameObject.SetActive(true);

        NewLevel(1);
    }
    public void PauseGame() {
        Time.timeScale = 0f;
        startMenu.gameObject.SetActive(true);
        creditsMenu.gameObject.SetActive(false);
        plotMenu.gameObject.SetActive(false);
    }
    public void ResumeGame() {
        Time.timeScale = 1f;
        startMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        plotMenu.gameObject.SetActive(false);
    }

    public void NewLevel(int levelNum) {
        Time.timeScale = 0f;


        // reset settings
        tutorialBox.SetActive(false);
        graphicsPlayerArrow.SetActive(false);
        TogglePossiblePlayerGoalsVisuals(true);
        GameManager.visionPowerUp = false;
        // special settings
        if (levelNum == 1) {
            tutorialBox.SetActive(true);
            graphicsPlayerArrow.SetActive(true);
            TogglePossiblePlayerGoalsVisuals(false);
        }
        else if (levelNum == 2) {
            graphicsPlayerArrow.SetActive(true);
        }

        // TESTING
        if(levelNum >= 2) {
            levelNum = 2;
        }

        currentLevelInst = GameObject.Instantiate(levels[levelNum]);
        plotMenu.SetActive(true);
        plotText.text = currentLevelInst.GetComponent<LevelProperties>().levelStory;
    }
    public void PlotButtonStart() {
        int idx = Random.Range(0, crowdCheerStartGame.Count);
        GameManager.SpawnLoudAudio(crowdCheerStartGame[idx]);

        startMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        plotMenu.gameObject.SetActive(false);

        // this is to remove the jump SFX when player presses Start
        StartCoroutine(StartLevelCountdown());
    }
    private IEnumerator StartLevelCountdown() {
        // this is to remove the jump SFX when player presses Start
        yield return new WaitForSecondsRealtime(0.1f);
        Time.timeScale = 1f;
    }
    public void FinishedLevel() {
        Destroy(currentLevelInst);
        currentLevel += 1;
        NewLevel(currentLevel);
    }
    private void TogglePossiblePlayerGoalsVisuals(bool newState) {
        foreach (Transform trans in possibleGoalTargets) {
            if (trans.GetComponent<SpriteRenderer>()) {
                trans.GetComponent<SpriteRenderer>().enabled = newState;
            }

        }
    }
    public void Update() {
        if(plotMenu.activeSelf) {
            if(GameManager.playerInputAction.Player.PlotButtonStart.WasPressedThisFrame()) {
                PlotButtonStart();
            }
            return;
        }

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
