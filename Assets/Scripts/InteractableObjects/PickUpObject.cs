public class PickUpObject : IInteractable
{
    public override void Interact()
    {
        onPickup?.Invoke(this);
        //player.Pickup(m_part);
    }
}
