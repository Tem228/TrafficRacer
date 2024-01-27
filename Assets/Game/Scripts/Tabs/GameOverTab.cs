using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverTab : TabBase
{
    [Header("Parameters")]
    [SerializeField]
    private int _gameSceneIndex;

    [Header("Buttons")]
    [SerializeField]
    private Button _retryButton;

    private void Awake()
    {
        _retryButton.onClick.AddListener(() => Retry());
    }

    protected override void Opened() { }

    protected override void Closed() { }

    private void Retry()
    {
        SceneManager.LoadScene(_gameSceneIndex);
    }
}
