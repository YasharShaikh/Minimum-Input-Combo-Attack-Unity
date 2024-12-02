using UnityEngine;

public class AnimationComponent : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayHitAnimation()
    {
        if (animator)
            animator.SetTrigger("Hit");
    }

    public void PlayDeathAnimation()
    {
        if (animator)
            animator.SetTrigger("Die");
    }
}
