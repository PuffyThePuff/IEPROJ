using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] music;
    public Sounds[] soundEffects;

    public static AudioManager Instance;

    [Range(0f, 1f)] private float masterVolume = 1.0f;
    [Range(0f, 1f)] private float musicVolume = 1.0f;
    [Range(0f, 1f)] private float sfxVolume = 1.0f;

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
            return;
        }

        foreach (Sounds s in music)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    private void AdjustVolume()
    {
        for (int i = 0; i < music.Length; i++)
        {
            music[i].source.volume = masterVolume * musicVolume * music[i].source.volume;
        }

        for (int i = 0; i < soundEffects.Length; i++)
        {
            soundEffects[i].source.volume = masterVolume * sfxVolume * soundEffects[i].source.volume;
        }
    }

    public void UpdateMasterVolume(float newMasterVolume)
    {
        masterVolume = newMasterVolume;
        AdjustVolume();
    }

    public void UpdateMusicVolume(float newMusicVolume)
    {
        musicVolume = newMusicVolume;
        AdjustVolume();
    }

    public void UpdateSFXVolume(float newSFXVolume)
    {
        sfxVolume = newSFXVolume;
        AdjustVolume();
    }

    public void Play(string name, bool isLoop = true)
    {
        Sounds s = Array.Find(music, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Play();
        s.source.loop = isLoop;
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(music, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }

        s.source.Stop();
        s.source.loop = false;
    }
}
