using UnityEngine;

public class PlayerService : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private Vector3 _playerSpawnOffset;
    [SerializeField]
    private Player _playerPrefab;

    [Header("Services")]
    [SerializeField]
    private GameController _gameController;

    public Player Player { get; private set; }

    private void Awake()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        Player = Instantiate(_playerPrefab, Vector3.zero + _playerSpawnOffset, Quaternion.identity);
    }
}
