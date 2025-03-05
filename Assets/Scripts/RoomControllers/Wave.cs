using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public List<Enemy> enemiesList;

    public void Init()
    {
        enemiesList = new List<Enemy>(GetComponentsInChildren<Enemy>());
    }

    public void Add(Enemy enemy)
    {
        enemiesList.Add(enemy);
    }
    public void InitWave(Player pl, Action<Enemy> onEnemyDeath)
    {
        gameObject.SetActive(true);
        for (int i = 0; i < enemiesList.Count; i++)
        {
            enemiesList[i].SetTarget(pl.transform);
            enemiesList[i].onEnemyDeath += onEnemyDeath;
        }
    }
}
