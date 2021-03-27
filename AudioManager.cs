using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField, Tooltip("Master Volume")]
    private float masterVolume;

    [SerializeField, Tooltip("Music Volume")]
    private float musicVolume;

    [SerializeField, Tooltip("SFX Volume")]
    private float sfxVolume;

    public static AudioManager audioManager;
    private static AudioClip currentMusic;
    private static GameObject currentMusicContainer;

    private void Awake()
    {
        if (audioManager != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            audioManager = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // this method is written to play global sounds
    public static void PlaySound(AudioClip thisSound)
    {
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.clip = thisSound;
        audioSource.volume = audioManager.sfxVolume * audioManager.masterVolume;
        audioSource.Play();
        Destroy(soundGameObject, thisSound.length);
    }

    // this method is written to play only one music track at a time
    public static void PlayMusic(AudioClip thisMusic)
    {
        // during an update of music, this checks to see if the music already exists in a game object
        if ((currentMusicContainer == null) && (currentMusic == null))
        {
            // already null, we don't need to blank it
        }
        else
        {
            currentMusic = null;
            if (currentMusicContainer != null)
            {
                Destroy(currentMusicContainer);
            }
        }

        currentMusicContainer = new GameObject("AmbientMusics");
        AudioSource thisMusicAS = currentMusicContainer.AddComponent<AudioSource>();
        thisMusicAS.clip = thisMusic;
        thisMusicAS.loop = true;
        thisMusicAS.volume = audioManager.masterVolume * audioManager.musicVolume;
        thisMusicAS.Play();
    }

    // this method is to play a sound at a position in the environment
    public static void PlaySoundAt(AudioClip thisSound, Transform thisLocation, float volume)
    {
        GameObject soundLocationGameObject = new GameObject("SoundAtLocation");
        AudioSource audioSource = soundLocationGameObject.AddComponent<AudioSource>();
        soundLocationGameObject.transform.position = thisLocation.position;
        audioSource.volume = volume * audioManager.sfxVolume * audioManager.masterVolume;
        audioSource.clip = thisSound;
        audioSource.Play();
        Destroy(soundLocationGameObject, thisSound.length);
    }
}
