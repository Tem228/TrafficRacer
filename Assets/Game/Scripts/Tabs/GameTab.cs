using UnityEngine;
using UnityEngine.UI;

public class GameTab : TabBase
{
    [Header("Distance")]
    [SerializeField]
    private Text _distanceText;

    [Header("Services")]
    [SerializeField]
    private RoadsService _roadsService;

    private void OnDestroy()
    {
        _roadsService.TravalledDistanceUpdated -= OnTravalledDistanceUpdated;
    }

    protected override void Opened()
    {
        _roadsService.TravalledDistanceUpdated += OnTravalledDistanceUpdated;
    }

    protected override void Closed()
    {
        _roadsService.TravalledDistanceUpdated -= OnTravalledDistanceUpdated;
    }

    private void OnTravalledDistanceUpdated(float distance)
    {
        _distanceText.text = $"{distance.ToString("0")}m";
    }
}
