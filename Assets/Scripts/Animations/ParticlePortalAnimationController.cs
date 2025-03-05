using UnityEngine;

public class ParticlePortalAnimationController : DoorsAnimationController
{
    [SerializeField] private ParticleSystem portalParticle;
    private void Awake()
    {
        portalParticle = GetComponent<ParticleSystem>();

        if (portalParticle != null)
        {
            portalParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            portalParticle.gameObject.SetActive(false);
        }
    }

    override public void Activate()
    {
        ActivatePortal();        
    }

    public void ActivatePortal()
    {
        if (portalParticle != null)
        {
            if (!portalParticle.gameObject.activeSelf)
            {
                portalParticle.gameObject.SetActive(true);
            }
            portalParticle.Play();
        }
    }
}
