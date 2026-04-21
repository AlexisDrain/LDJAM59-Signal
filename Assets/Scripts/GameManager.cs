using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    /* Alexis Clay Drain */
    public static bool visionPowerUp = false;
    public static bool energyPowerUp = false;
    public static bool startedGame = false;
    public static bool tallLevel = false;
    public static int currentLevel = 0;
    public static int currentHealth = 3;
    public GameObject currentLevelInst;

    public static LayerMask layerMask_Floor;
    public static UnityEvent levelEndEvent = new UnityEvent();
    public UnityEvent tallLevelEvent = new UnityEvent();
    public UnityEvent notTallLevelEvent = new UnityEvent();

    public static PlayerInputAction playerInputAction;
    public static Pool pool_LoudAudioSource;

    public static GameManager myGameManager;
    public static PlayerHealth myPlayerHealth;
    public static Transform canvasWorld;
    public static GameObject startMenu;
    public static GameObject plotMenu;
    public static GameObject creditsMenu;
    public static GameObject endMenu;
    public static GameObject splashScreen;
    public static GameObject tutorialBox;
    public static GameObject tutorialBoxDoubleJump;
    public static TextMeshProUGUI plotText;
    public static Transform playerTrans;
    public static Transform goalTrans;
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

        layerMask_Floor = LayerMask.NameToLayer("Floor");

        pool_LoudAudioSource = transform.Find("Pool_LoudAudioSource").GetComponent<Pool>();

        Time.timeScale = 0f;
        myGameManager = GetComponent<GameManager>();
        myPlayerHealth = GetComponent<PlayerHealth>();
        canvasWorld = GameObject.Find("CanvasWorld").transform;
        startMenu = GameObject.Find("Canvas/StartMenu");
        plotText = GameObject.Find("Canvas/PlotMenu/PlotBox/Text (TMP)").GetComponent<TextMeshProUGUI>();
        plotMenu = GameObject.Find("Canvas/PlotMenu");
        plotMenu.SetActive(false);
        creditsMenu = GameObject.Find("Canvas/CreditsMenu");
        creditsMenu.SetActive(false);
        endMenu = GameObject.Find("Canvas/EndMenu");
        endMenu.SetActive(false);
        splashScreen = GameObject.Find("Canvas/SplashScreen");
        if(splashScreen == null) {
            Debug.LogError("Forgot to enable splash screen");
        }
        tutorialBox = GameObject.Find("Canvas/GameMenu/TutorialBox");
        tutorialBox.SetActive(false);
        tutorialBoxDoubleJump = GameObject.Find("Canvas/GameMenu/TutorialBoxDoubleJump");
        tutorialBoxDoubleJump.SetActive(false);

        playerTrans = GameObject.Find("Player").transform;
        goalTrans = GameObject.Find("Goal").transform;
        mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
        graphicsPlayerArrow = GameObject.Find("GraphicsPlayerArrow");

        trajectoryStarts = GameObject.Find("TrajectoryStarts").transform;
    }
    public void EndGame() {
        Time.timeScale = 0f;
        startedGame = false;
        currentLevel = 0;
        currentHealth = 3;
        playerInputAction.Disable();
        // startMenu.gameObject.SetActive(true);
        // creditsMenu.gameObject.SetActive(false);
        // plotMenu.gameObject.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void StartGame() {
        Time.timeScale = 0f;
        startedGame = true;
        startMenu.gameObject.SetActive(false);
        creditsMenu.gameObject.SetActive(false);
        plotMenu.gameObject.SetActive(true);

        currentHealth = 3;
        GetComponent<PlayerHealth>().UpdateHealthValue(currentHealth);
        currentLevel = 0;
        NewLevel(0);
    }
    public void PauseGame() {
        Time.timeScale = 0f;
        startMenu.gameObject.SetActive(true);
        creditsMenu.gameObject.SetActive(false);
        // plotMenu.gameObject.SetActive(false);
    }
    public void ResumeGame() {
        if(plotMenu.activeSelf) {
            startMenu.gameObject.SetActive(false);
            creditsMenu.gameObject.SetActive(false);
            plotMenu.gameObject.SetActive(true);
        } else {
            Time.timeScale = 1f;
            startMenu.gameObject.SetActive(false);
            creditsMenu.gameObject.SetActive(false);
            plotMenu.gameObject.SetActive(false);

        }
    }
    public void ToggleGlassesPowerUp(bool newState) {
        if (newState == true) {
            GameManager.visionPowerUp = true;
        } else {
            GameManager.visionPowerUp = false;
        }
    }
    public void ToggleDrinkPowerUp(bool newState) {
        if(newState == true) {
            GameManager.energyPowerUp = true;
            GameManager.playerTrans.GetComponent<PlayerController>().SetTrailColor(Color.green);
        } else {
            GameManager.energyPowerUp = false;
            GameManager.playerTrans.GetComponent<PlayerController>().SetTrailColor(Color.white);
        }
    }
    public void ToggleTallGoal(bool newState) {
        if (newState == true) {
            GameManager.tallLevel = true;
            goalTrans.position = new Vector3(goalTrans.position.x, 2, goalTrans.position.z);
            tallLevelEvent.Invoke();
        } else {
            GameManager.tallLevel = false;
            goalTrans.position = new Vector3(goalTrans.position.x, 0, goalTrans.position.z);
            notTallLevelEvent.Invoke();
        }
    }
    public void NewLevel(int levelNum) {
        Time.timeScale = 0f;

        // reset settings
        tutorialBox.SetActive(false); // enable for tutorial
        // tutorialBoxDoubleJump.SetActive(false);
        graphicsPlayerArrow.SetActive(false); // enable for tutorial
        TogglePossiblePlayerGoalsVisuals(true); // disable for tutorial, or for hard levels
        GameManager.myGameManager.ToggleGlassesPowerUp(false);
        GameManager.myGameManager.ToggleDrinkPowerUp(false);
        GameManager.tallLevel = false;
        // special settings
        if (levelNum == 0) {

            tutorialBox.SetActive(true);
            graphicsPlayerArrow.SetActive(true);
            TogglePossiblePlayerGoalsVisuals(false);
        }
        else if (levelNum == 1) {
            GameManager.visionPowerUp = true;
            graphicsPlayerArrow.SetActive(true);
        }

        // TESTING. ENDLESS MODE
        // if(levelNum >= 2) {
        //     levelNum = 2;
        // }

        currentLevelInst = GameObject.Instantiate(levels[levelNum]);
        currentLevelInst.SetActive(true);
        GameManager.myGameManager.ToggleTallGoal(currentLevelInst.GetComponent<LevelProperties>().tallLevel);
        tutorialBoxDoubleJump.SetActive(currentLevelInst.GetComponent<LevelProperties>().tutorialDoubleJump);
        if(currentLevelInst.GetComponent<LevelProperties>().removeColorGrid) {
            TogglePossiblePlayerGoalsVisuals(false);
        }
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
    public void PlayerDied() {
        Destroy(currentLevelInst);
        // currentLevel += 1;
        myPlayerHealth.PlayerRestoreAllHealth(); // to do, might remove
        levelEndEvent.Invoke();
        NewLevel(currentLevel);
    }
    public void FinishedLevel() {
        Destroy(currentLevelInst);
        currentLevel += 1;
        if(currentLevel == levels.Count) {
            Time.timeScale = 0f;
            endMenu.SetActive(true);
            return;
        }
        myPlayerHealth.PlayerRestoreAllHealth(); // to do, might remove
        levelEndEvent.Invoke();
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
        if(endMenu.activeSelf) {
            return;
        }
        if(plotMenu.activeSelf) {
            if(GameManager.playerInputAction.Player.PlotButtonStart.WasPressedThisFrame()) {
                PlotButtonStart();
            }
            if(GameManager.playerInputAction.Player.Pause.WasPressedThisFrame()) {
                PauseGame();
            }
            return;
        }

        if(GameManager.playerInputAction.Player.Pause.WasPressedThisFrame()) {
            if (startedGame == true) {
                if(plotMenu.activeSelf == false) {
                    if (startMenu.activeSelf == true) {
                        ResumeGame();
                    }
                    else {
                        PauseGame();
                    }
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
