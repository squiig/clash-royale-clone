using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : DDOLSingleton<AudioManager>
{
    private AudioSource[] m_SFXSources;

    [SerializeField]
    private AudioSource m_MusicSource;
    public AudioSource MusicSource { get { return m_MusicSource; } }

    [SerializeField]
    private int m_SFXBuffers = 8;
    public int SFXBuffers { get { return m_SFXBuffers; } }

    [SerializeField]
    private const float m_DefaultSFXVolume = .5f;
    public float DefaultSFXVolume { get { return m_DefaultSFXVolume; } }

    private Vector2 m_DefaultPitchRange = new Vector2(.95f, 1.05f);

    /// <summary>
    /// This function is called first thing in the entire script.
    /// </summary>
    public override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// This function is called after the Awake function.
    /// </summary>
    void Start()
    {
        m_SFXSources = new AudioSource[m_SFXBuffers];

        for (int i = 0; i < m_SFXSources.Length; i++)
        {
            m_SFXSources[i] = gameObject.AddComponent<AudioSource>();
        }
    }

    public void PlayEffect( AudioClip clip, float volume = m_DefaultSFXVolume, bool loop = false )
    {
        AudioSource source = GetInactiveAudioSource();
        source.pitch = Random.Range(m_DefaultPitchRange.x, m_DefaultPitchRange.y);
        source.clip = clip;
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    public void PlayEffect( AudioClip clip, int priority, float volume = m_DefaultSFXVolume, bool loop = false )
    {
        AudioSource source = m_SFXSources[Mathf.Clamp(priority, 0, m_SFXSources.Length)];
        source.pitch = Random.Range(m_DefaultPitchRange.x, m_DefaultPitchRange.y);
        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    public void PlayRandomEffect( AudioClip[] clips, float volume = m_DefaultSFXVolume, bool loop = false )
    {
        AudioSource source = GetInactiveAudioSource();
        source.pitch = Random.Range(m_DefaultPitchRange.x, m_DefaultPitchRange.y);
        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    public void PlayRandomEffect( AudioClip[] clips, int priority, float volume = m_DefaultSFXVolume, bool loop = false )
    {
        AudioSource source = m_SFXSources[Mathf.Clamp(priority, 0, m_SFXSources.Length)];
        source.pitch = Random.Range(m_DefaultPitchRange.x, m_DefaultPitchRange.y);
        source.clip = clips[Random.Range(0, clips.Length)];
        source.volume = volume;
        source.loop = loop;
        source.Play();
    }

    private AudioSource GetInactiveAudioSource()
    {
        foreach (AudioSource source in m_SFXSources)
        {
            if (!source.isPlaying) return source;
        }

        Debug.LogWarning("Cannot find inactive audio source! Returning last in list.");
        return m_SFXSources[m_SFXSources.Length - 1];
    }
}
