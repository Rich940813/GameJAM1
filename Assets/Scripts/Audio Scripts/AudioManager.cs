using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // variable static para facil acceso

    public AudioSource audioSource;
    public AudioSource musicSource;
    public AudioSource eventsSource;
    public AudioSource ambientSource;

    public AudioClip musicClip;
    public AudioClip ambientTrack;
    public AudioClip[] jingles;


    public bool music = true;
    public bool ambient = true;

    private void Awake()
    {
        instance = this; // inicializar variable static
    }

    // Funcion que reproduce el sonido asignado, a cierto volumen, que puede randomizar el pitch, y puede reproducir un clip random de un array.
    public void PlaySFX(AudioClip audioClip, float volume = 1f, float randomMin = 0f, float randomMax = 0f, int randomIndex = 0)
    {
        audioSource.pitch = Random.Range(randomMin, randomMax); // randomiza el pitch
        audioSource.PlayOneShot(audioClip, volume); // Reproduce un audio una sola vez en su totalidad.
    }
    public void PlayJingle(int index, float volume = 1f, int randomIndex = 0)
    {
        eventsSource.PlayOneShot(jingles[index], volume); // Reproduce un audio una sola vez en su totalidad.
    }

    private void Start()
    {
        PlayMusic(musicClip);
        PlayAmbientTrack(ambientTrack);
    }
    private void Update()
    {
        if (music && !musicSource.isPlaying)
        {
            PlayMusic(musicClip);
        }
        else if (!music && musicSource.isPlaying)
        {
            StopMusic();
        }

        if (ambient && !ambientSource.isPlaying)
        {
            PlayAmbientTrack(ambientTrack);
        }
        else if (!ambient && ambientSource.isPlaying)
        {
            StopAmbientTrack();
        }
    }
    public void PlayMusic(AudioClip musicClip)
    {
        
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.volume = 0.2f;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
    public void PlayAmbientTrack(AudioClip ambientTrack)
    {

        {
            ambientSource.clip = ambientTrack;
            ambientSource.loop = true;
            ambientSource.volume = 2f;
            ambientSource.Play();
        }
    }

    public void StopAmbientTrack()
    {
        ambientSource.Stop();
    }
}
