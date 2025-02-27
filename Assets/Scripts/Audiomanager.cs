using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    //Autor Kascha und Korte

    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    private AudioSource engineSource; // Dedicated source for engine sounds

    public AudioClip background;
    public AudioClip death;
    public AudioClip shooting;
    public AudioClip damageTaken;
    public AudioClip gameOver;
    public AudioClip victory;
    public AudioClip rocketEngine;

    void Awake()
    {
        // Create a separate audio source for engine sounds
        engineSource = gameObject.AddComponent<AudioSource>();
        // Copy any important settings from SFXSource if needed
        engineSource.volume = SFXSource.volume;
        engineSource.spatialBlend = SFXSource.spatialBlend;
    }

    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        musicSource.loop = true;
    }

    void Update()
    {

    }

    // Play regular one-shot sound effects
    public void PlayerSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    // Handle engine sound specifically
    public void PlayEngineSound()
    {
        if (!engineSource.isPlaying)
        {
            engineSource.clip = rocketEngine;
            engineSource.loop = true;
            engineSource.Play();
        }
    }

    public void StopEngineSound()
    {
        if (engineSource.isPlaying)
        {
            engineSource.Stop();
        }
    }
}