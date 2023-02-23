using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemiesFactory : MonoBehaviour
{
    [SerializeField] GameObject simpleEnemyPrefab;
    [SerializeField] GameObject flyingEnemyPrefab;

    [Inject] EventManager eventManager;
    [Inject] LevelData levelData;

    public List<GameObject> enemies;

    public void InstantiateEnemy(Types.TypeEnemy typeEnemy, Vector3 spawnPosition)
    {
        GameObject enemyPrefab = null;
        GameObject enemy = null;

        switch (typeEnemy)
        {
            case Types.TypeEnemy.simple:
                enemyPrefab = simpleEnemyPrefab;
                break;
            case Types.TypeEnemy.flying:
                enemyPrefab = flyingEnemyPrefab;
                break;
        }

        if (enemyPrefab != null)
        {
            enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.Euler(0,180,0));
            enemy.GetComponent<Enemy>().enemiesFactory = this;
            enemy.GetComponent<Enemy>().levelData = levelData;
            enemies.Add(enemy);
        }
    }

    public void ActivateEnemies()
    {
        foreach(GameObject enemy in enemies)
        {
            enemy.GetComponent<Enemy>().Activate();
        }
    }

    public void DeleteEnemy(GameObject enemy)
    {
        eventManager.EnemyKilled(enemy.transform.position);

        enemies.Remove(enemy);
        Destroy(enemy);

        if (enemies.Count == 0)
            eventManager.LevelCompleted();
    }

    public void DeleteAllEnemies()
    {
        for(int i =0;i<enemies.Count;i++)
        {
            Destroy(enemies[i]);
        }

        enemies.Clear();
    }
}

public class Types
{
    public enum TypeEnemy
    {
        simple,
        flying
    }
}


