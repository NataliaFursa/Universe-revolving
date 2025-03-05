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
            RewardContainer spawnChest = Instantiate(reward, roomRewardSpawnPosition);
            spawnChest.onOpen += OnContainerOpen;
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
