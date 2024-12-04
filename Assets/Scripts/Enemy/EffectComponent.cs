using UnityEngine;

public class EffectComponent : MonoBehaviour
{
    [SerializeField] ParticleSystem woodHit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayHitParticle()
    {
        woodHit.Play();
    }
}
