using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource mainAudioSource;
    public AudioMixerGroup soundEffectMixer;
    public AudioMixerGroup musicEffectMixer;
    
    [Tooltip("Index 0 = Menu, 1 = Niveau 1, 2 = Niveau 2 (selon les Build Settings)")]
    public AudioClip[] playlist;
    
    private float volumeOnPaused = 0.35f;
    private float volumeOnPlay = 1f;
    private float volumeStep = 0.005f;

    [Header("Listen to event channels")]
    public PlaySoundAtEventChannel sfxAudioChannel;

    private void OnEnable()
    {
        sfxAudioChannel.OnEventRaised += PlayClipAt;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex < playlist.Length && playlist[scene.buildIndex] != null)
        {
            if (mainAudioSource.clip != playlist[scene.buildIndex])
            {
                mainAudioSource.clip = playlist[scene.buildIndex];
                mainAudioSource.outputAudioMixerGroup = musicEffectMixer;
                mainAudioSource.Play();
            }
        }
    }

    IEnumerator IncreaseVolume()
    {
        while (mainAudioSource.volume < volumeOnPlay)
        {
            mainAudioSource.volume += volumeStep;
            yield return null;
        }
    }

    IEnumerator DecreaseVolume()
    {
        while (mainAudioSource.volume > volumeOnPaused)
        {
            mainAudioSource.volume -= volumeStep;
            yield return null;
        }
    }

    public void PlayClipAt(AudioClip clip, Vector3 position)
    {
        GameObject tempGO = new GameObject("TempAudio");
        tempGO.transform.position = position;
        AudioSource audioSource = tempGO.AddComponent<AudioSource>();
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = soundEffectMixer;
        audioSource.Play();
        Destroy(tempGO, clip.length);
    }

    public void OnTogglePause(bool isGamePaused)
    {
        if (isGamePaused)
        {
            StopAllCoroutines();
            StartCoroutine(DecreaseVolume());
        }
        else
        {
            StopAllCoroutines();
            StartCoroutine(IncreaseVolume());
        }
    }

    private void OnDisable()
    {
        sfxAudioChannel.OnEventRaised -= PlayClipAt;
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
