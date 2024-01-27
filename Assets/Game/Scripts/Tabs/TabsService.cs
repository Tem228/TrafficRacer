using System.Collections.Generic;
using UnityEngine;

public class TabsService : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private TabBase _defaultTab;
    [SerializeField]
    private TabBase[] _tabs;

    private Dictionary<string, TabBase> _tabsDictionary;

    private TabBase _currentTab;

    [Header("Services")]
    [SerializeField]
    private GameController _gameController;

    private void OnEnable()
    {
        _gameController.GameStateUpdated += OnGameStateUpdated;
    }

    private void OnDisable()
    {
        _gameController.GameStateUpdated -= OnGameStateUpdated;
    }

    private void Start()
    {
        InitializeTabsDictionary();
    }

    private void InitializeTabsDictionary()
    {
        _tabsDictionary = new Dictionary<string, TabBase>();

        for (int i = 0; i < _tabs.Length; i++)
        {
            _tabsDictionary.Add(_tabs[i].Name, _tabs[i]);
        }

        OpenTab(_defaultTab.Name);

        _tabs = null;
    }

    public void OpenTab(string name)
    {
        if (_currentTab != null)
        {
            _currentTab.SetVisible(false);
        }

        _currentTab = _tabsDictionary[name];

        _currentTab.SetVisible(true);
    }

    private TabBase GetTabByName(string name)
    {
        if (!_tabsDictionary.ContainsKey(name))
        {
            throw new System.Exception($"Вкладки с '{name}' именем не существует");
        }

        return _tabsDictionary[name];
    }

    private void OnGameStateUpdated(GameState state)
    {
        _currentTab.SetVisible(false);

        switch (state)
        {
            case GameState.Idle:
                OpenTab("Menu");
                break;
            case GameState.Playing:
                OpenTab("Game");
                break;
            case GameState.Over:
                OpenTab("GameOver");
                break;
        }
    }
}
