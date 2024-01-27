using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField]
    private float _speed;

    private void Update()
    {
        if (!gameObject.activeSelf 
        || GameController.Instance.State != GameState.Playing)
        {
            return;
        }

        transform.Translate(-transform.forward * _speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Player player))
        {
            GameController.Instance.GameOver();
        }
    }

    public void SetVisible(bool isVisible)
    {
        gameObject.SetActive(isVisible);
    }
}
