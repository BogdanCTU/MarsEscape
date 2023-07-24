using System;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Fields

    public static SoundManager instance;

    public List<AudioSource> musicAudios = new List<AudioSource>();

    public List<AudioSource> laserAudios = new List<AudioSource>();
    public List<AudioSource> powerUpAudios = new List<AudioSource>();
    public List<AudioSource> explosionAudios = new List<AudioSource>();

    public AudioSource buttonAudio;

    public float globalVolume = 1.0f;

    #endregion Fields

    #region Mono

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        PlayBGMusic();
    }

    #endregion Mono

    #region Methods

    public void MuteAudio()
    {
        globalVolume = 0;

        foreach (var item in musicAudios)
        {
            item.volume = globalVolume;
        }
        foreach (var item in laserAudios)
        {
            item.volume = globalVolume;
        }
        foreach (var item in powerUpAudios)
        {
            item.volume = globalVolume;
        }
        foreach (var item in explosionAudios)
        {
            item.volume = globalVolume;
        }
        buttonAudio.volume = globalVolume;
    }

    public void UnmuteAudio()
    {
        globalVolume = 1;

        foreach (var item in musicAudios)
        {
            item.volume = globalVolume;
            if (musicAudios.Count != 1) item.volume = globalVolume / 2;
        }
        foreach (var item in laserAudios)
        {
            item.volume = globalVolume;
        }
        foreach (var item in powerUpAudios)
        {
            item.volume = globalVolume;
        }
        foreach (var item in explosionAudios)
        {
            item.volume = globalVolume;
        }
        buttonAudio.volume = globalVolume;
    }

    private void PlayBGMusic()
    {
        int index = UnityEngine.Random.Range(0, musicAudios.Count);
        musicAudios[index].loop = true;
        musicAudios[index].Play();
        if (musicAudios.Count != 1) musicAudios[index].volume = globalVolume / 2;
    }

    public void LaserSound()
    {
        int index = UnityEngine.Random.Range(0, laserAudios.Count);
        laserAudios[index].Play();
    }

    public void PowerUpSound()
    {
        int index = UnityEngine.Random.Range(0, powerUpAudios.Count);
        powerUpAudios[index].Play();
    }

    public void ExplosionSound()
    {
        int index = UnityEngine.Random.Range(0, explosionAudios.Count);
        explosionAudios[index].Play();
    }

    public void ButtonSound()
    {
        buttonAudio.Play();
    }

    #endregion Methods

}
