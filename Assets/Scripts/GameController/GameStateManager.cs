using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Init = 0,
    Playing = 1,
    End = 2,
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private StartMenu startMenu;
    [SerializeField] private GameMenu gameMenu;
    [SerializeField] private EndMenu endMenu;
    [SerializeField] private CountdownEffect countDownEffect;


    public static GameStateManager Instance { get; internal set; }

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        ChaneStateGame(GameState.Init);
    }

    void Initialized()
    {       
        NoteLoader.Instance.Initialize();
        AudioManager.Instance.Initialize();
        countDownEffect.Initialize();
        StartCoroutine(PlayingRoutine());
    }
    void EndGame()
    {
        NoteLoader.Instance.SetEndGame();
        ObjectPool.Instance.SetDeactiveAll();
        AudioManager.Instance.EndMusic();
        ScoreManager.Instance.RestartHP();
        StopAllCoroutines();
    }
    
    public void StartBattle()
    {
        ChaneStateGame(GameState.Playing);
    }

    IEnumerator PlayingRoutine()
    {
        yield return new WaitForSeconds(NoteLoader.Instance.lastNoteTime);
        ChaneStateGame(GameState.End);
    }

    public void ChaneStateGame(GameState state)
    {
        gameState = state;
        switch (gameState)
        {
            case GameState.Init:
                startMenu.Show();
                gameMenu.Hide();
                endMenu.Hide();
                break;
            case GameState.Playing:
                startMenu.Hide();
                endMenu.Hide();
                gameMenu.Show();
                Initialized();
                StartCoroutine(countDownEffect.PlayCountdown());
                break;
            case GameState.End:
                gameMenu.Hide();
                endMenu.Show();
                EndGame();
                break;
            default:
                break;
        }
    }
}