using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer _masterMixer;

    public float MusicVolume { get; private set; }

    private const string SAVE_KEY_MUSIC_VOLUME = "MusicVolume";

    private const string AUDIO_MIXER_MUSIC_PARAMETER_VOLUME_NAME = "MusicVolume";

    private void Start()
    {
        InitializeMusicVolume();
    }

    private void InitializeMusicVolume()
    {
        if (PlayerPrefs.HasKey(SAVE_KEY_MUSIC_VOLUME))
        {
            SetMusicVolume(PlayerPrefs.GetFloat(SAVE_KEY_MUSIC_VOLUME));
        }
        else
        {
            SetMusicVolume(1);
        }
    }

    public void SetMusicVolume(float volume)
    {
        if (volume == 0)
        {
            volume = 0.0001f;
        }

        MusicVolume = volume;

        _masterMixer.SetFloat(AUDIO_MIXER_MUSIC_PARAMETER_VOLUME_NAME, Mathf.Log(volume) * 20);

        PlayerPrefs.SetFloat(SAVE_KEY_MUSIC_VOLUME, volume);
    }
}
