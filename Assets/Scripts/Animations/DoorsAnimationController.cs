using UnityEngine;

public class DoorsAnimationController : MonoBehaviour
{
    [SerializeField] private Animator DoorA_animator; 
    [SerializeField] private Animator DoorB_animator; 


    private void OnEnable()
    {

    }

    private void OnDisable()
    {

    }

    public virtual void Activate()
    {
        if (DoorA_animator != null)  DoorA_animator.SetTrigger("OpenDoorA");
        if (DoorA_animator != null) DoorB_animator.SetTrigger("OpenDoorB");
    }
}
