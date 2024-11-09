using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public enum AudioChannel
    {
        Master,
        Sfx,
        Music,
    }

    [Range(0, 1)]
    public float masterVolumePercent;
    [Range(0, 1)]
    public float sfxVolumePercent;
    [Range(0, 1)]
    public float musicVolumePercent;

    AudioSource[] musicSources;
    int activeMusicSourceIndex;
    AudioSource sfx2DSource;
    Transform audioListenerTransform;
    Transform playerTransform;

    SoundLibrary library;

    public static AudioManager Instance { get; private set; }

    public void PlayMusic(AudioClip clip, float fadeDuration = 1)
    {
        activeMusicSourceIndex = 1 - activeMusicSourceIndex;
        musicSources[activeMusicSourceIndex].clip = clip;
        musicSources[activeMusicSourceIndex].Play();

        StartCoroutine(nameof(AnimateMusicCrossfade), fadeDuration);
    }

    public void PlaySound(string clipName, Vector3 position)
    {
        PlaySound(library.GetClipFromName(clipName), position);
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, position, sfxVolumePercent * masterVolumePercent);
        }
    }

    public void PlaySound2D(string clipName)
    {
        sfx2DSource.PlayOneShot(library.GetClipFromName(clipName), sfxVolumePercent * masterVolumePercent);
    }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        audioListenerTransform = FindObjectOfType<AudioListener>().transform;
        playerTransform = FindObjectOfType<Player>()?.transform;
        library = GetComponent<SoundLibrary>();

        masterVolumePercent = PlayerPrefs.GetFloat("master volume", 0.75f);
        sfxVolumePercent = PlayerPrefs.GetFloat("sfx volume", 0.75f);
        musicVolumePercent = PlayerPrefs.GetFloat("music volume", 0.75f);

        sfx2DSource = CreateAudioSource("2D Sfx source", sfxVolumePercent * masterVolumePercent);

        musicSources = new AudioSource[2];
        for (int i = 0; i < musicSources.Length; i += 1)
        {
            musicSources[i] = CreateAudioSource($"Music source {i + 1}", musicVolumePercent * masterVolumePercent);
            musicSources[i].loop = true;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    AudioSource CreateAudioSource(string name, float volume)
    {
        var newMusicSource = new GameObject(name);
        newMusicSource.transform.parent = transform;
        var source = newMusicSource.AddComponent<AudioSource>();
        source.volume = volume;
        return source;
    }

    void Start()
    {

    }

    void Update()
    {
        if (playerTransform != null)
        {
            audioListenerTransform.position = playerTransform.position;
        }
    }

    public void SetVolume(float volumePercent, AudioChannel channel)
    {
        switch (channel)
        {
            case AudioChannel.Master:
                masterVolumePercent = volumePercent;
                break;
            case AudioChannel.Sfx:
                sfxVolumePercent = volumePercent;
                break;
            case AudioChannel.Music:
                musicVolumePercent = volumePercent;
                break;
        }

        foreach (var source in musicSources)
        {
            source.volume = musicVolumePercent * masterVolumePercent;
        }

        PlayerPrefs.SetFloat("master volume", masterVolumePercent);
        PlayerPrefs.SetFloat("sfx volume", sfxVolumePercent);
        PlayerPrefs.SetFloat("music volume", musicVolumePercent);
        PlayerPrefs.Save();
    }

    IEnumerator AnimateMusicCrossfade(float duration)
    {
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / duration;
            var maxVolume = musicVolumePercent * masterVolumePercent;
            musicSources[activeMusicSourceIndex].volume = Mathf.Lerp(0, maxVolume, percent);
            musicSources[1 - activeMusicSourceIndex].volume = Mathf.Lerp(maxVolume, 0, percent);

            yield return null;
        }
    }
}
