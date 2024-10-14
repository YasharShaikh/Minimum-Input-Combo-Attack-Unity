using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Settings")]
    [SerializeField] float fadeSeconds = 1.0f; // Duration for fade-in/fade-out

    [Header("Action Menu")]
    [SerializeField] AudioSource as_ActionMenuBG;

    [Header("sword attack")]
    [SerializeField] AudioSource as_Attack;
    [SerializeField] List<AudioClip> ac_Sword = new List<AudioClip>();

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
        RadialMenuBackground();
    }


    private void RadialMenuBackground()
    {
        if (ActionMenuInputHandler.Instance.RadialMenuTriggered)
            FadeInHandler(as_ActionMenuBG);
        else
            FadeOutHandler(as_ActionMenuBG);
    }

    private void FadeOutHandler(AudioSource audioSource)
    {
        audioSource.volume = Mathf.Clamp(audioSource.volume - (Time.deltaTime / fadeSeconds), 0f, 1f);

        if (audioSource.volume <= 0f && audioSource.isPlaying)
            audioSource.Stop();
    }

    private void FadeInHandler(AudioSource audioSource)
    {
        if (!audioSource.isPlaying)
            audioSource.Play();

        audioSource.volume = Mathf.Clamp(audioSource.volume + (Time.deltaTime / fadeSeconds), 0f, 1f);
    }


    public void PlaySwordClip()
    {
        int clip = Random.Range(0, ac_Sword.Count);
        as_Attack.PlayOneShot(ac_Sword[clip]);
    }
}
