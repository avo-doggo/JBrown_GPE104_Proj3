using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Audio Clips")]
    public AudioClip shootingSound;
    public AudioClip damageSound;
    public AudioClip destructionSound;
    public AudioClip targetHum;
    public AudioClip healSound;
    public AudioClip backgroundMusic;


    [Header("Audio Mixer")]
    public AudioMixer mainAudioMixer;

    [Header("Audio Volume")]
    public float mainVolume = 1f;
    public float musicVolume = 1f;
    public float SFXVolume = 1f;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        LoadSoundSettings();
    }


    public void LoadSoundSettings()
    {
        mainVolume = PlayerPrefs.GetFloat("MainVolume", 1f);
        musicVolume = PlayerPrefs.GetFloat("MusicVolume", 1f);
        SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);

        UpdateMainVolume();
        UpdateMusicVolume();
        UpdateSFXVolume();
    }


    public void SaveSoundSettings()
    {
        PlayerPrefs.SetFloat("MainVolume", mainVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);
        PlayerPrefs.SetFloat("SFXVolume", SFXVolume);

        PlayerPrefs.Save();
    }


    public void UpdateMainVolume()
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat(
                "MasterVolume",
                ConvertToDecibels(mainVolume)
            );
        }
    }


    public void UpdateMusicVolume()
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat(
                "MusicVolume",
                ConvertToDecibels(musicVolume)
            );
        }
    }


    public void UpdateSFXVolume()
    {
        if (mainAudioMixer != null)
        {
            mainAudioMixer.SetFloat(
                "SFXVolume",
                ConvertToDecibels(SFXVolume)
            );
        }
    }


    private float ConvertToDecibels(float sliderValue)
    {
        if (sliderValue <= 0)
        {
            return -80f;
        }

        return Mathf.Log10(sliderValue) * 20f;
    }
}