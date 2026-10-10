using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

//Responsibilities of Game Manager

//Scenes / Levels
//Title screen
//Level 1
//Level 2
//Level 3

//Game States
//Pause
//Game over
//OnGameStart
//OnGameEnd

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        GamePaused,
        GameIntro,
        GameStart,
        GameOver,
        GameEnd,
        LevelStart,
        LevelEnd,
        GamePlaying
    }

    public GameState currentGameState = GameState.GameIntro;

    public static GameManager instance = null;
    public Camera mainCamera;
    // public List<LevelManager> levels = new List<LevelManager>();
    public LevelManager currentLevel;
    // public event Action OnEnemyKilled;
    public AudioSource GameSfxSource;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        } else
        {
            instance = this;
        }
    }

    void Start()
    {
        ChangeState(GameState.GameStart);
    }

    void Update()
    {
        if (PlayerInput.Instance.pausePressed)
        {
            if(currentGameState == GameState.GamePaused)
            {
                ChangeState(GameState.GamePlaying);
            } 
            else if(currentGameState == GameState.GamePlaying)
            {
                ChangeState(GameState.GamePaused);
            }
        }
    }

    public void ChangeState(GameState newState)
    {
        currentGameState = newState;

        switch (newState)
        {
            case GameState.GameIntro:
                OnGameIntro();
                break;
            case GameState.GameStart:
                OnGameStart();
                break;
            case GameState.GameOver:
                OnGameOver();
                break;
            case GameState.GamePlaying:
                OnGamePlaying();
                break;
            case GameState.GameEnd:
                OnGameEnd();
                break;
            case GameState.LevelStart:
                LevelStart();
                break;
            case GameState.LevelEnd:
                LevelEnd(); 
                break;
            case GameState.GamePaused:
                OnGamePaused();
                break;
        } 
    }

    private void OnGameIntro()
    {
        //Game Intro Scene
    }

    private void OnGameStart()
    {
        //Initialize whatever is needed when starting the game
        ChangeState(GameState.GamePlaying);
    }

    private void OnGameOver()
    {
        //Handles logic when the player loses / dies
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void OnGameEnd()
    {
        //Logic that pertains the overarching game experience
        //Calculating final score, determing if player won or lost
        //Figure out the next level, or next scene to go to
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Debug.Log("Game Ended");
        Debug.Log(currentGameState);
    }

    private void OnGamePlaying()
    {
       Time.timeScale = 1f;
    }

    private void LevelStart()
    {
        //Start cinematics / instructions / tutorial video
        // StartCoroutine(ChangeStateDelay(GameState.GamePlaying, 2f));
        currentLevel.LevelStart();
    }

    private void LevelEnd()
    {
        //When we finish a level / hit that ending cut scene
        currentLevel.LevelEnd();
    }

    private void OnGamePaused()
    {
        //When game is paused
        Time.timeScale = 0f;
    }

    public void CinematicStarted()
    {
        Debug.Log("Cinematic Started");
        // ChangeState(GameState.LevelStart);
    }

    public void CinematicEnded()
    {
        Debug.Log("Cinematic Ended");
        mainCamera.transform.localPosition = new Vector3(0, .75f, 0);
        ChangeState(GameState.GamePlaying);
    }

    private IEnumerator ChangeStateDelay(GameState newState, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        ChangeState(newState);
    }

    public void RestartLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    

}