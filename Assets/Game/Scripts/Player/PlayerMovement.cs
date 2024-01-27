using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Paramaters")]
    [SerializeField]
    private float _positionShiftSmooth;

    private float _endXPosition;

    private void Update()
    {
        if (GameController.Instance.State != GameState.Playing)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _endXPosition = transform.position.x + 3;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _endXPosition = transform.position.x - 3;
        }

        _endXPosition = Mathf.Clamp(_endXPosition, -3, 3);

        transform.DOMoveX(_endXPosition, _positionShiftSmooth);
    }
}