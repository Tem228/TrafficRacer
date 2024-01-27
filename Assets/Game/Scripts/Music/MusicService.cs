using UnityEngine;

public class MusicService : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private AudioClip _musicClip;

    [Header("Services")]
    [SerializeField]
    private GameController _gameController;

    [Header("Components")]
    [SerializeField]
    private AudioSource _audioSource;

    private void OnEnable()
    {
        _gameController.GameStateUpdated += OnGameStateUpdated;
    }

    private void OnDisable()
    {
        _gameController.GameStateUpdated -= OnGameStateUpdated;
    }

    private void OnGameStateUpdated(GameState state)
    {
        if(state != GameState.Playing)
        {
            _audioSource.Stop();

            return;
        }

        _audioSource.clip = _musicClip;

        _audioSource.Play();
    }
}
