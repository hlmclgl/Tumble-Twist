using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Theme");
        LoadSettings();
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
        SaveSettings();
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
        SaveSettings();
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
        SaveSettings();
    }

    public void SFXVolume(float volume)
    {
        sfxSource.volume = volume;
        SaveSettings();
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void UnPauseMusic()
    {
        musicSource.UnPause();
    }

    private void SaveSettings()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicSource.volume);
        PlayerPrefs.SetFloat("SFXVolume", sfxSource.volume);
        PlayerPrefs.SetInt("MusicMuted", musicSource.mute ? 1 : 0);
        PlayerPrefs.SetInt("SFXMuted", sfxSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }

    private void LoadSettings()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        if (PlayerPrefs.HasKey("MusicMuted"))
        {
            musicSource.mute = PlayerPrefs.GetInt("MusicMuted") == 1;
        }
        if (PlayerPrefs.HasKey("SFXMuted"))
        {
            sfxSource.mute = PlayerPrefs.GetInt("SFXMuted") == 1;
        }

    }
}
