using System;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class RoadsService : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _distanceBetweenRoads;
    [SerializeField]
    private float _replaceRoadDistance;
    [SerializeField]
    private float _updateRoadSpeed;
    [SerializeField]
    private GameObject _roadPrefab;
    [SerializeField]
    private Transform _roadsParent;

    private int _lastRoadIndex;
    private int _currentRoadIndex;

    public GameObject FirstRoad => _roads[0];
    public GameObject LastRoad => _roads[_roads.Length - 1];

    private GameObject[] _roads;

    [Header("Services")]
    [SerializeField]
    private GameController _gameController;

    private float _travalledDistance;

    public event Action<float> TravalledDistanceUpdated;

    private const int MAX_ROADS = 5;

    private void Awake()
    {
        InitializeRoads();
    }

    private void Update()
    {

        MoveRoads();
        ReplaceRoads();
        UpdateTravalledDistance();
    }

    private void InitializeRoads()
    {
        _roads = new GameObject[MAX_ROADS];

        for(int i = 0; i < MAX_ROADS; i++)
        {
            Vector3 roadPosition = Vector3.forward * _distanceBetweenRoads * i;

            GameObject road = Instantiate(_roadPrefab, roadPosition, Quaternion.identity, _roadsParent);

            _roads[i] = road;
        }
    }

    private void MoveRoads()
    {
        if(_gameController.State == GameState.Over)
        {
            return;
        }

        for (int i = 0; i < _roads.Length; i++)           
        {                                                 
            _roads[i].transform.Translate(-transform.forward * _updateRoadSpeed * Time.deltaTime);
        }     
    }

    private void ReplaceRoads()
    {
        if (_gameController.State == GameState.Over)
        {
            return;
        }

        if (_roads[_currentRoadIndex].transform.position.z > _replaceRoadDistance)
        {
            return;
        }

        _lastRoadIndex = (_currentRoadIndex - 1) < 0 ? _roads.Length - 1 : _currentRoadIndex - 1;

        _roads[_currentRoadIndex].transform.position = _roads[_lastRoadIndex].transform.position + Vector3.forward * _distanceBetweenRoads;

        _currentRoadIndex = ++_currentRoadIndex >= _roads.Length ? 0 : _currentRoadIndex;
    }

    private void UpdateTravalledDistance()
    {
        if (_gameController.State != GameState.Playing)
        {
            return;
        }

        _travalledDistance += _updateRoadSpeed * Time.deltaTime;

        TravalledDistanceUpdated?.Invoke(_travalledDistance);
    }
}
