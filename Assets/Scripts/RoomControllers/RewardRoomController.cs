using UnityEngine;

public class RewardRoomController : RoomController
{
    [SerializeField] RewardContainer reward;
    [SerializeField] Transform roomRewardSpawnPosition;

    override protected void SpecProcessing()
    {
        if (reward != null)
        {
            Instantiate(reward, roomRewardSpawnPosition);
            reward.onOpen += OnContainerOpen;
        }
        else
        {
            FinishRoomTask();
        }
    }

    public void OnContainerOpen()
    {
        FinishRoomTask();
    }
}
