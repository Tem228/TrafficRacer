using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameState _state;

    public GameState State
    {
        get
        {
            return _state;
        }

        set
        {
            _state = value;

            GameStateUpdated?.Invoke(_state);
        }
    }

    public event Action<GameState> GameStateUpdated;

    public static GameController Instance { get; private set; }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartGame()
    {
        State = GameState.Playing;
    }

    public void GameOver()
    {
        State = GameState.Over;
    }
}
