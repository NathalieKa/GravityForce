using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Audiomanager : MonoBehaviour
{
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    public AudioClip background;
    public AudioClip death;
    public AudioClip shooting;
    public AudioClip damageTaken;
    public AudioClip gameOver;
    public AudioClip victory;
    public AudioClip rocketEngine;


    void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayerSFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

    public void StopEngineSound()
    {
        if (SFXSource.isPlaying)
        {
            SFXSource.Stop();
        }
    }



}
