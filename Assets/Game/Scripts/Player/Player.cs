using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Components")]
    [SerializeField]
    private PlayerMovement _movement;
    [SerializeField]
    private Rigidbody _rigidbody;

    public PlayerMovement Movement => _movement;

    private void OnEnable()
    {
        GameController.Instance.GameStateUpdated += OnGameStateUpdated;
    }

    private void OnDisable()
    {
        GameController.Instance.GameStateUpdated -= OnGameStateUpdated;
    }

    private void OnGameStateUpdated(GameState state)
    {
        _rigidbody.useGravity = state == GameState.Over;

        if (state == GameState.Over)
        {
            _rigidbody.AddForce(Random.insideUnitCircle.normalized * 100f);
        }
    }
}
