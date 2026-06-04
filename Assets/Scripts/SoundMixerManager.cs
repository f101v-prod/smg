using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;

    private readonly string masterVoluemName = "masterVolume";
    private readonly string musicVoluemName = "musicVolume";
    private readonly string sfxVoluemName = "soundFXVolume";

    private readonly float defaultVolume = 0.8f;

    private void Start()
    {
        InitTrackVolume(masterVoluemName);
        InitTrackVolume(musicVoluemName);
        InitTrackVolume(sfxVoluemName);
    }

    public void SetMasterVolume(float level)
    {
        SetTrackVolume(masterVoluemName, level);
    }

    public void SetMusicVolume(float level)
    {
        SetTrackVolume(musicVoluemName, level);
    }

    public void SetSoundFXVolume(float level)
    {
        SetTrackVolume(sfxVoluemName, level);
    }

    private void InitTrackVolume(string track)
    {
        if (PlayerPrefs.HasKey(track))
            LoadPref(track);
        else
            SetTrackVolume(track, defaultVolume);
    }

    private void LoadPref(string name)
    {
        var level = PlayerPrefs.GetFloat(name);
        SetTrackVolume(name, level);
    }

    private void SetTrackVolume(string track, float level)
    {
        if (Mathf.Approximately(level, 0))
            audioMixer.SetFloat(track, float.MinValue);
        else
            audioMixer.SetFloat(track, Mathf.Log10(level) * 20f);

        PlayerPrefs.SetFloat(track, level);
        PlayerPrefs.Save();
    }
}
