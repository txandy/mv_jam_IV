using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Wall;

public class GameManager : MonoBehaviour
{
    public static event Action<Status> StatusChanged = delegate(Status status) { };
    public static event Action<int> LevelChanged = delegate(int level) { };
    public static event Action GameStart = delegate { };
    public static GameManager Instance;

    public enum Status
    {
        Pause,
        Runing
    }

    public Status gameStatus;

    public GameObject startPopup;
    public GameObject gameOverPopup;

    public int level;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        HorseController.HorseDie += HorseControllerOnHorseDie;
        SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        UpdateScore.ScoreUpdate += UpdateScoreOnScoreUpdate;
    }

    private void UpdateScoreOnScoreUpdate(int score)
    {
        if (score > 1 && score % 5 == 0)
        {
            level++;
            LevelChanged(level);
        }
    }

    private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
    {
        InitGame();
    }

    private void OnDestroy()
    {
        HorseController.HorseDie -= HorseControllerOnHorseDie;
        SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
        UpdateScore.ScoreUpdate -= UpdateScoreOnScoreUpdate;
    }

    private void HorseControllerOnHorseDie()
    {
        GameOver();
    }

    // Use this for initialization
    void Start()
    {
        InitGame();
    }

    void InitGame()
    {
        ChangeStatus(Status.Pause);
        level = 1;
        startPopup.SetActive(true);
        gameOverPopup.SetActive(false);
        
        GameStart();
    }

    public void GameOver()
    {
        ChangeStatus(Status.Pause);
        gameOverPopup.SetActive(true);
    }

    public void ButtonRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ButtonPlay()
    {
        ChangeStatus(Status.Runing);
        startPopup.SetActive(false);
        gameOverPopup.SetActive(false);
    }

    private void ChangeStatus(Status status)
    {
        switch (status)
        {
            case Status.Pause:
                Time.timeScale = 0;
                break;

            case Status.Runing:
                Time.timeScale = 1;
                break;
        }

        gameStatus = status;

        // Event
        StatusChanged(status);
    }
}