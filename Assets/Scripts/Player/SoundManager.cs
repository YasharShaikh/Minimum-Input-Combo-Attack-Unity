using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Action Menu")]
    [SerializeField] AudioSource as_ActionMenuBG;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void PlayActionMenuBG(bool play)
    {
        if (play && !as_ActionMenuBG.isPlaying)
        {
            as_ActionMenuBG.Play();
        }
        else if (!play && as_ActionMenuBG.isPlaying)
        {
            as_ActionMenuBG.Stop();
        }
    }
}
