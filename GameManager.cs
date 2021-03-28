using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager thisGameManager { get; private set; }

    public static bool Paused { get; private set; } = false;

    public AudioClip thisMusic;

    public int LifeCount { get; private set; }

    public static GameObject Player { get; private set; }

    public Scene[] theseScenes;

    [Header("Events")]
    [SerializeField, Tooltip("Raised every time the game is paused.")]
    private UnityEvent onPause;

    [SerializeField, Tooltip("Raised every time the game is resumed.")]
    private UnityEvent onResume;

    [SerializeField, Tooltip("Raised once when the player loses all lives.")]
    private UnityEvent onGameOver;
    
    private GameObject PauseMenu;

    private GameObject[] PlayerSpawnPoints;

    [SerializeField, Tooltip("These are the starting lives.")]
    private int startingLives;

    private void Awake()
    {
        if (thisGameManager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            thisGameManager = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.PlayMusic(thisMusic);
        LifeCount = startingLives;
        PlayerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawner");
        if (PlayerSpawnPoints != null)
        {
            spawnPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Player == null)
        {
            LifeCount--;
            if (LifeCount <= 0)
            {
                onGameOver.Invoke();
            }
            else
            {
                spawnPlayer();
            }
        }
    }

    private void spawnPlayer()
    {
        if (Player == null)
        {
            Player = makePlayerSpawn();
        }
    }

    // makes the player spawn at the spawn point
    private GameObject makePlayerSpawn()
    {
        GameObject thisIsTheNewPlayer;
        int thisLocation = UnityEngine.Random.Range(0, PlayerSpawnPoints.Length);
        thisIsTheNewPlayer = PlayerSpawnPoints[thisLocation].GetComponent<PlayerSpawner>().createPlayer();
        return thisIsTheNewPlayer;
    }

    // pauses the game
    public static void PauseGame()
    {
        Paused = true;
        Time.timeScale = 0f;
        thisGameManager.PauseMenu = GameObject.FindGameObjectWithTag("CameraOverlay");
        thisGameManager.PauseMenu = thisGameManager.PauseMenu.GetComponent<CreatePauseMenu>().createPauseMenu();
        thisGameManager.onPause.Invoke();
    }

    // unpauses the game
    public static void UnpauseGame()
    {
        Paused = false;
        Time.timeScale = 1f;
        if (thisGameManager.PauseMenu != null)
        {
            Destroy(thisGameManager.PauseMenu);
            thisGameManager.PauseMenu = null;
        }
        thisGameManager.onResume.Invoke();
    }

    // hard quits the game
    public static void QuitTheGame()
    {
        Application.Quit();
    }

    // resets the game
    public static void RestartLevel()
    {
        thisGameManager.LifeCount = thisGameManager.startingLives;
    }

    // made to toggle the current pause state
    public static void TogglePause()
    {
        if (Paused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    // triggers a game over condition for the game manager
    public static void triggerGameOver()
    {
        Paused = true;
        Time.timeScale = 0f;
        thisGameManager.PauseMenu = GameObject.FindGameObjectWithTag("CameraOverlay");
        thisGameManager.PauseMenu = thisGameManager.PauseMenu.GetComponent<CreatePauseMenu>().createPauseMenu();
    }

    public static void loadStartScreen()
    {
        SceneManager.LoadScene(thisGameManager.theseScenes[0].name);
    }

    public static void loadFirstLevel()
    {
        SceneManager.LoadScene(thisGameManager.theseScenes[1].name);
    }

    public static void loadSecondLevel()
    {
        SceneManager.LoadScene(thisGameManager.theseScenes[2].name);
    }
}
