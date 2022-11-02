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
	public AudioSource gameSource;
	public AudioSource jumpSource;
	public AudioSource hitSource;
	public AudioSource coinSource;
	public AudioSource deathSource;
	public AudioSource menuSource;
	public AudioSource gameOverSource;
	public AudioSource winningSource;
	public AudioSource winningSourceLoop;

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

	public void WinningMusic()
    {
		winningSource.Play();
		winningSourceLoop.PlayDelayed(winningSource.clip.length);
    }

	public void GameOverMusic(bool playMusic)
    {
		if (playMusic)
			gameOverSource.Play();
		else
			gameOverSource.Stop();
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
