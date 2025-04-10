using UnityEngine;

public class PersistentMusic : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponentInChildren<AudioSource>();
        audioSource.Play();
    }

    public void SetVolume(float volume) {
        audioSource.volume = volume;
    }
}
