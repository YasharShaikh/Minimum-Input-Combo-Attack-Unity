using UnityEngine;

public class SoundComponent : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    public void PlayHitSound()
    {
        if (audioSource && hitSound)
        {
            float pitch = Random.Range(1f, 1.5f);
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(hitSound);
        }
    }

    public void PlayDeathSound()
    {
        if (audioSource && deathSound)
            audioSource.PlayOneShot(deathSound);
    }
}
