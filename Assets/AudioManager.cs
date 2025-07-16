using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Singleton { get; internal set; }
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource soundFXSource;
    public AudioClip music;
    public AudioClip click;
    public AudioClip win;
    public AudioClip fail;

    public void Awake()
    {
        if (Singleton != null)
        {
            // As long as you aren't creating multiple LevelHandler instances, throw an exception.
            // (***the current position of the callstack will stop here***)
            throw new Exception($"Detected more than one instance of {nameof(AudioManager)}! " +
                $"Do you have more than one component attached to a {nameof(GameObject)}");
        }
        Singleton = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        musicSource.clip = music;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip sfx)
    {
        soundFXSource.PlayOneShot(sfx);
    }
}
