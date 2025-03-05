using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Properties;
using UnityEngine;

public class BattleRoomController : RoomController
{
    /*
    [System.Serializable]
    public class SubList
    {
        public List<Enemy> waveEnemies = new List<Enemy>();
    }

    // использование
    public List<SubList> enemies = new List<SubList>();
    private List<List<Enemy>> enemyList;
    */
    [SerializeField] int xpReward;
    [SerializeField] private List<Wave> waves;
    [SerializeField] private GameObject enemySpawnDisplayObject;

    private int currentWave;
    private List<GameObject> displaysList = new List<GameObject>();
    

    override protected void SpecProcessing()
    {
        foreach (Wave wave in waves)
        {
            wave.Init();
        }
        if (waves.Count == 0)
        {
            instantCompletion = true;
            FinishRoomTask();
        }
        else
        {
            instantCompletion = false;
            LoadWave(0);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < waves.Count; i++)
        {
            Wave curWave = waves[i];
            for (int j = 0; j < curWave.enemiesList.Count; j++)
            {
                curWave.enemiesList[j].onEnemyDeath -= OnEnemyDeath;
            }
        }
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        waves[currentWave].enemiesList.Remove(enemy);   
        if (waves[currentWave].enemiesList.Count == 0 ) {
            currentWave++;
            LoadWave(currentWave);
        }
    }

    protected void LoadWave(int wave)
    {
        if(currentWave >= waves.Count)
        {
            FinishRoomTask();
        }
        else
        {
            waves[wave].InitWave(player, OnEnemyDeath);
        }
        ClearDisplays();
        if ((currentWave < waves.Count - 1) && (enemySpawnDisplayObject != null)) {
            SpawnDisplays();
        }
    }

    private void SpawnDisplays()
    {
        foreach(Enemy enemyToDisplay in waves[currentWave + 1].enemiesList)
        {
            RaycastHit hit;
            Physics.Raycast(enemyToDisplay.transform.position, new Vector3(0, -1, 1), out hit);
            GameObject spawnDisplay = Instantiate(enemySpawnDisplayObject, hit.point, Quaternion.identity);
            displaysList.Add(spawnDisplay);
        }
    }

    private void ClearDisplays()
    {
        foreach (GameObject display in displaysList)
        {
            Destroy(display);
        }
        displaysList = new List<GameObject>();
    }

    override protected void FinishRoomTask()
    {
        if (connectionsCount == 0)
        {
            for (int i = 0; i < activeTransitions.Length; ++i)
            {
                //Тут нужно сделать более сложную логику открытия и закрытию проходов
                activeTransitions[i].Enable();
            }
            Meta.PlayerXP += xpReward;
        }
        else
        {
            for (int i = 0; i < connectionsCount; ++i)
            {
                //Тут нужно сделать более сложную логику открытия и закрытию проходов
                activeTransitions[i].Enable();
            }
            Meta.PlayerXP += xpReward;
        }
        if (rewardContainer != null)
        {
            Instantiate(rewardContainer, rewardSpawnPosition);
        }
        
    }

    public virtual void AddEnemy(Enemy enemy)
    {
        waves[currentWave].Add(enemy);
    }
}
