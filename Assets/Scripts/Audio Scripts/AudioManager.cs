using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX(AudioClip audioClip, float volume = 1f, float randomMin = 0f, float randomMax = 0f, int randomIndex = 0)
    {
        audioSource.pitch = Random.Range(randomMin, randomMax);
        audioSource.PlayOneShot(audioClip, volume);
    }
}
