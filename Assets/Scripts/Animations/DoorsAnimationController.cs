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
        DoorA_animator.SetTrigger("OpenDoorA");
        DoorB_animator.SetTrigger("OpenDoorB");
    }
}
