using UnityEngine;
using UnityEngine.UI;

public class MenuTab : TabBase
{
    [Header("Buttons")]
    [SerializeField]
    private Button _startGameButton;
    [SerializeField]
    private Button _openSettingsButton;
    [SerializeField]
    private Button _exitButton;

    [Header("Services")]
    [SerializeField]
    private TabsService _tabsService;
    [SerializeField]
    private GameController _gameController;

    private void Awake()
    {
        _startGameButton.onClick.AddListener(() => StartGame());

        _openSettingsButton.onClick.AddListener(() => ShowSettings());

        _exitButton.onClick.AddListener(() => Exit());
    }

    protected override void Opened() { }

    protected override void Closed() { }

    private void StartGame()
    {
        _gameController.StartGame();
    }

    private void ShowSettings()
    {
        _tabsService.OpenTab("Settings");
    }

    private void Exit()
    {
        Application.Quit();
    }
}
