using UnityEngine;

public abstract class TabBase : MonoBehaviour
{
    [SerializeField]
    private string _name;

    [SerializeField]
    private GameObject _panel;

    public string Name => _name;

    public bool IsOpen => _panel.activeSelf;

    public void SetVisible(bool isVisible)
    {
        _panel.SetActive(isVisible);

        if (IsOpen)
        {
            Opened();
        }
        else
        {
            Closed();
        }
    }

    protected abstract void Opened();
    protected abstract void Closed();
}

