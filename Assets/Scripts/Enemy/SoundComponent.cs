using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    public void PlayHitSound()
    {
        if (audioSource && hitSound)
            audioSource.PlayOneShot(hitSound);
    }

    public void PlayDeathSound()
    {
        if (audioSource && deathSound)
            audioSource.PlayOneShot(deathSound);
    }
}
