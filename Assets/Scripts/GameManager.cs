using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState { Menu, Game, LevelComplete, GameOver }
    public GameState state;
    public static Action<GameState> onGameStateChanged;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void SetGameState(GameState state)
    {
        this.state = state;
        onGameStateChanged?.Invoke(state);
    }

    public bool IsGameState()
    {
        return state == GameState.Game;
    }
}
