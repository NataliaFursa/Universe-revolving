using UnityEditor;
using UnityEngine;

public class RewardRoomController : RoomController
{
    [SerializeField] RewardContainer reward;
    [SerializeField] Transform roomRewardSpawnPosition;

    override protected void SpecProcessing()
    {
        if (reward != null)
        {
            try
            {
                Instantiate(reward, roomRewardSpawnPosition);
            }
            catch { }
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
