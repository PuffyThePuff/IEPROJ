using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] Toggle fullscreenToggle;
    [SerializeField] Dropdown resolutionDropdown;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = Screen.fullScreen;

        Screen.SetResolution(1920, 1080, Screen.fullScreen);
        resolutionDropdown.value = 0;

        masterSlider.value = 1.0f;
        musicSlider.value = 1.0f;
        sfxSlider.value = 1.0f;
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void SetResolution()
    {
        switch (resolutionDropdown.value)
        {
            case 0: Screen.SetResolution(1920, 1080, Screen.fullScreen); break;
            case 1: Screen.SetResolution(1366, 768, Screen.fullScreen); break;
            case 2: Screen.SetResolution(1280, 720, Screen.fullScreen); break;
        }
    }

    public void SetMasterVolume()
    {
        AudioManager.Instance.UpdateMasterVolume(masterSlider.value);
    }

    public void SetMusicVolume()
    {
        AudioManager.Instance.UpdateMusicVolume(musicSlider.value);
    }

    public void SetSFXVolume()
    {
        AudioManager.Instance.UpdateSFXVolume(sfxSlider.value);
    }
}