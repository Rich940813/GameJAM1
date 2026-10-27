using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // variable static para facil acceso

    public AudioSource audioSource;
    public AudioSource musicSource;
    public AudioSource eventsSource;

    public AudioClip musicClip;

    public bool music = true;

    private void Awake()
    {
        instance = this; // inicializar variable static
        audioSource = GetComponent<AudioSource>(); // Vincular componente audio source a variable
    }

    // Funcion que reproduce el sonido asignado, a cierto volumen, que puede randomizar el pitch, y puede reproducir un clip random de un array.
    public void PlaySFX(AudioClip audioClip, float volume = 1f, float randomMin = 0f, float randomMax = 0f, int randomIndex = 0)
    {
        audioSource.pitch = Random.Range(randomMin, randomMax); // randomiza el pitch
        audioSource.PlayOneShot(audioClip, volume); // Reproduce un audio una sola vez en su totalidad.
    }

    private void Start()
    {
        if (music == true)
        {
            PlayMusic(musicClip);
        }
        if (music == false)
        {
            StopMusic();
        } 
    }
    public void PlayMusic(AudioClip musicClip)
    {
        
        {
            musicSource.clip = musicClip;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
