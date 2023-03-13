using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public abstract class Sound : MonoBehaviour
{
    private AudioSource _audioSource;

    protected virtual void Awake() 
        => _audioSource = GetComponent<AudioSource>();

    protected void Play() => _audioSource.Play();
}