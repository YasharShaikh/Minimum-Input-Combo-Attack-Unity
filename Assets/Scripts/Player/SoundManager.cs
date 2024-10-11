using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] float fadeSeconds = 1.0f; // Duration for fade-in/fade-out

    [Header("Action Menu")]
    [SerializeField] AudioSource as_ActionMenuBG;

    private bool fadingIn;
    private bool fadingOut;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    void Update()
    {
        //HandleAudioFading();
    }

    public void PlayActionMenuBG(bool play)
    {
        if (play && !as_ActionMenuBG.isPlaying && !fadingIn)
        {
            fadingOut = false;
            fadingIn = true;
            as_ActionMenuBG.Play();
        }
        else if (!play && as_ActionMenuBG.isPlaying && !fadingOut)
        {
            fadingIn = false;
            fadingOut = true;
        }
    }

    private void HandleAudioFading(AudioSource audioSource)
    {
        if (fadingIn)
        {
            // Fade in
            audioSource.volume += (1f / fadeSeconds) * Time.deltaTime;
            if (audioSource.volume >= 1f)
            {
                audioSource.volume = 1f;
                fadingIn = false; // Stop fading when fully faded in
            }
        }
        else if (fadingOut)
        {
            // Fade out
            audioSource.volume -= (1f / fadeSeconds) * Time.deltaTime;
            if (audioSource.volume <= 0f)
            {
                audioSource.volume = 0f;
                fadingOut = false;
                audioSource.Stop(); // Only stop once volume is fully faded out
            }
        }
    }
}
