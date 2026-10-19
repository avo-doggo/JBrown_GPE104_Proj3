using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class OptionsScreenUI : MonoBehaviour
{
    public Slider mainVolumeSlider;
    public Slider musicVolumeSlider;
    public Slider SFXVolumeSlider;
    public Key quitKey;

    public GameObject settingsMenuPanel;

    public void Start()
    {
        // Get saved settings from GameManager
        mainVolumeSlider.value = GameManager.instance.mainVolume;
        musicVolumeSlider.value = GameManager.instance.musicVolume;
        SFXVolumeSlider.value = GameManager.instance.SFXVolume;

        // Apply the values
        OnMainVolumeChange();
        OnMusicVolumeChange();
        OnSFXVolumeChange();
    }

    public void Update()
    {
        if (Keyboard.current[quitKey].wasPressedThisFrame)
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        if (settingsMenuPanel != null)
        {
            bool isActive = settingsMenuPanel.activeSelf;

            settingsMenuPanel.SetActive(!isActive);

            if (isActive)
            {
                GameManager.instance.SaveSoundSettings();
            }
        }
    }

    public void OnMainVolumeChange()
    {
        GameManager.instance.mainVolume = mainVolumeSlider.value;

        GameManager.instance.UpdateMainVolume();
    }

    public void OnMusicVolumeChange()
    {
        GameManager.instance.musicVolume = musicVolumeSlider.value;

        GameManager.instance.UpdateMusicVolume();
    }

    public void OnSFXVolumeChange()
    {
        GameManager.instance.SFXVolume = SFXVolumeSlider.value;

        GameManager.instance.UpdateSFXVolume();
    }
}