using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[Serializable]
public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;
	[SerializeField]
	AudioMixer mixer;
	[SerializeField]
	AudioSource gameSource;
	[SerializeField]
	AudioSource jumpSource;
	[SerializeField]
	AudioSource hitSource;
	[SerializeField]
	AudioSource coinSource;
	[SerializeField]
	AudioSource deathSource;
	[SerializeField]
	AudioSource menuSource;

	public const string masterKey = "Volume";
	public const string musicKey = "MusicVolume";
	public const string sfxKey = "SFXVolume";

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
			return;
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		LoadSound();
	}

	public void MenuMusic(bool playMusic)
    {
		if (playMusic)
			menuSource.Play();
		else
			menuSource.Stop();
    }

	public void GameMusic(bool playMusic)
    {
		if (playMusic)
			gameSource.Play();
		else
			gameSource.Stop();
    }

	public void DeathSFX()
    {
		deathSource.Play();
    }

	public void JumpSFX()
    {
		jumpSource.Play();
    }

	public void HitSFX()
    {
		hitSource.Play();
    }

	public void CoinSFX()
    {
		coinSource.Play();
    }

	//take saved data in Sound.cs
	void LoadSound()
    {
		float masterVol = PlayerPrefs.GetFloat(masterKey, 1f);
		float musicVol = PlayerPrefs.GetFloat(musicKey, 1f);
		float sfxVol = PlayerPrefs.GetFloat(sfxKey, 1f);

		mixer.SetFloat(Sound.masterMixer, masterVol);
		mixer.SetFloat(Sound.musicMixer, musicVol);
		mixer.SetFloat(Sound.sfxMixer, sfxVol);

    }
}
