using UnityEngine;

public class ParticlePortalAnimationController : MonoBehaviour
{
    private ParticleSystem portalParticle;
    private void Awake()
    {
        portalParticle = GetComponent<ParticleSystem>();

        if (portalParticle != null)
        {
            portalParticle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            portalParticle.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {

        // += HandlePlayerShoot;

    }

    private void OnDisable()
    {

        // -= HandlePlayerShoot;

    }

    private void HandlePlayerShoot()
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
