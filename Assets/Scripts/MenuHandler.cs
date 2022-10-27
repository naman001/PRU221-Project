using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviour
{
    public AudioMixer audMixer;
    Resolution[] resolutions;
    public TMPro.TMP_Dropdown resolutionDropdown;
    public Toggle fScreenToggle;
    public Button isMuteBtn;
    public Button loadBtn;

    private void Awake()
    {
        string path = Application.persistentDataPath + "/character.save";
        if (!File.Exists(path))
        {
            loadBtn.interactable = false;
            loadBtn.colors = ColorBlock.defaultColorBlock;
        }

        if (Screen.fullScreen == true)
            fScreenToggle.isOn = true;

        else
        {
            fScreenToggle.isOn = false;
        }
    }

    private void Start()
    {

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void LoadMap(int index)
    {
        SoundManager.instance.MenuMusic(false);
        SoundManager.instance.GameMusic(true);
        SceneManager.LoadScene(index);
    }

    public void Mute(bool isMute)
    {
        if (isMute)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;

    }

    public void SetVolume(float volume)
    {
        audMixer.SetFloat("Volume", volume);
    }

    public void SetMusicVolume(float volume)
    {
        audMixer.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audMixer.SetFloat("SFXVolume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool isFScreen)
    {
        if (isFScreen)
        {
            Screen.fullScreen = isFScreen;
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            fScreenToggle.isOn = isFScreen;
        }
        else
        {
            Screen.fullScreen = false;
            Screen.fullScreenMode = FullScreenMode.Windowed;
            fScreenToggle.isOn = isFScreen;
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
