using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

[System.Serializable]
public class Sound : MonoBehaviour
{
	[SerializeField]
	AudioMixer mixer;
	[SerializeField]
	Slider masterSlider;
	[SerializeField]
	Slider musicSlider;
	[SerializeField]
	Slider sfxSlider;

	public const string masterMixer = "Volume";
	public const string musicMixer = "MusicVolume";
	public const string sfxMixer = "SFXVolume";

	void Awake()
    {
		masterSlider.onValueChanged.AddListener(SetMasterVol);
		musicSlider.onValueChanged.AddListener(SetMusicVol);
		sfxSlider.onValueChanged.AddListener(SetSFXVol);
    }

    private void Start()
    {
		masterSlider.value = PlayerPrefs.GetFloat(SoundManager.masterKey, 1f);
		musicSlider.value = PlayerPrefs.GetFloat(SoundManager.musicKey, 1f);
		sfxSlider.value = PlayerPrefs.GetFloat(SoundManager.sfxKey, 1f);
    }

    private void OnDisable()
    {
		PlayerPrefs.SetFloat(SoundManager.masterKey, masterSlider.value);
		PlayerPrefs.SetFloat(SoundManager.musicKey, musicSlider.value);
		PlayerPrefs.SetFloat(SoundManager.sfxKey, sfxSlider.value);
    }

    void SetMasterVol(float masterVol)
    {
		mixer.SetFloat(SoundManager.masterKey, masterVol);
    }
	
	void SetMusicVol(float musicVol)
    {
		mixer.SetFloat(SoundManager.musicKey, musicVol);
    }
	
	void SetSFXVol(float sfxVol)
    {
		mixer.SetFloat(SoundManager.sfxKey, sfxVol);
    }

}
