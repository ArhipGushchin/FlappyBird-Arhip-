using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private List<AudioClip> musicTracks = new List<AudioClip>();
    [SerializeField] private List<AudioClip> soundEffects = new List<AudioClip>();

    [Header("Volume Settings")]
    [Range(0f, 1f)] public float musicVolume = 0.7f;
    [Range(0f, 1f)] public float sfxVolume = 0.7f;

    private int currentTrackIndex = 0;

    private void Awake()
    {
        // ���������� Singleton
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

        // ������������� ����� ����������
        if (musicSource == null)
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true;
        }

        if (sfxSource == null)
        {
            sfxSource = gameObject.AddComponent<AudioSource>();
        }

        // ��������� ���������
        UpdateVolumes();
    }

    private void Start()
    {
        // ��������������� ������� ����� ��� ������
        if (musicTracks.Count > 0)
        {
            PlayMusicTrack(0);
        }
    }

    private void UpdateVolumes()
    {
        musicSource.volume = musicVolume;
        sfxSource.volume = sfxVolume;
    }

    // ��������������� ������ �� �������
    public void PlayMusicTrack(int trackIndex)
    {
        if (trackIndex >= 0 && trackIndex < musicTracks.Count)
        {
            currentTrackIndex = trackIndex;
            musicSource.clip = musicTracks[trackIndex];
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid music track index: " + trackIndex);
        }
    }

    // ��������������� ���������� �����
    public void PlayNextTrack()
    {
        currentTrackIndex = (currentTrackIndex + 1) % musicTracks.Count;
        PlayMusicTrack(currentTrackIndex);
    }

    // ��������������� ����������� �����
    public void PlayPreviousTrack()
    {
        currentTrackIndex--;
        if (currentTrackIndex < 0) currentTrackIndex = musicTracks.Count - 1;
        PlayMusicTrack(currentTrackIndex);
    }

    // ��������������� ��������� ������� �� �������
    public void PlaySFX(int sfxIndex)
    {
        if (sfxIndex >= 0 && sfxIndex < soundEffects.Count)
        {
            sfxSource.PlayOneShot(soundEffects[sfxIndex]);
        }
        else
        {
            Debug.LogWarning("Invalid SFX index: " + sfxIndex);
        }
    }

    // ��������������� ��������� ������� �� �����
    public void PlaySFX(string sfxName)
    {
        AudioClip clip = soundEffects.Find(s => s.name == sfxName);
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("SFX not found: " + sfxName);
        }
    }

    // ��������� ������
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ����� ������
    public void PauseMusic()
    {
        musicSource.Pause();
    }

    // ���������� ��������������� ������
    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // ��������� ��������� ������
    public void SetMusicVolume(float volume)
    {
        musicVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    // ��������� ��������� �������� ��������
    public void SetSFXVolume(float volume)
    {
        sfxVolume = Mathf.Clamp01(volume);
        UpdateVolumes();
    }

    // ������������ ����� �� �����
    public void PlayMusicByName(string trackName)
    {
        AudioClip clip = musicTracks.Find(m => m.name == trackName);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("Music track not found: " + trackName);
        }
    }
}
