using UnityEngine;
using UnityEngine.UI;

public class SettingsTab : TabBase
{
    [Header("Music")]
    [SerializeField]
    private Slider _musicSlider;

    [Header("Services")]
    [SerializeField]
    private Settings _settings;

    private void Awake()
    {
        _musicSlider.onValueChanged.AddListener((value) => OnMusicSliderValueChanged(value));
    }

    protected override void Opened()
    {
        InitializeMusicSlider();
    }

    protected override void Closed() { }

    private void InitializeMusicSlider()
    {
        _musicSlider.value = _settings.MusicVolume;
    }

    private void OnMusicSliderValueChanged(float value)
    {
        _settings.SetMusicVolume(value);
    }
}
