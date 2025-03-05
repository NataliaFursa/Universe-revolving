using UnityEngine;

public class ParticlePortalAnimationController : MonoBehaviour
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

    private void OnEnable()
    {

        // += ActivatePortal;

    }

    private void OnDisable()
    {

        // -= ActivatePortal;

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
