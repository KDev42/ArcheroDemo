using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelStart : MonoBehaviour
{
    [SerializeField] int minNumberEnemies;
    [SerializeField] int maxNumberEnemies;
    [SerializeField] Vector2 arenaSize;
    [SerializeField] float spawnStep;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] GameObject hero;

    private Vector3 originCoordinates;
    private int cellX, cellY;
    private bool[,] grid;
    private GameObject arena;

    [Inject] EventManager eventManager;
    [Inject] EnemiesFactory enemiesFactory;
    [Inject] LevelData levelData;

    public void StartLevel(GameObject arena)
    {
        this.arena = arena;
        levelData.InitializeNavMesh(arena);

        originCoordinates = Vector3.zero - new Vector3(arenaSize.x / 2, 0, -arenaSize.y / 2);
        cellX = (int)(arenaSize.x / spawnStep);
        cellY = (int)(arenaSize.y / spawnStep);

        GenerateGrid();

        hero.GetComponent<Hero>().SetIdle();
        hero.transform.position = new Vector3(0, 0.25f, -3f);
        hero.transform.rotation = Quaternion.Euler(0, 0, 0);
        levelData.Hero = hero;

        SpawnEnemy();
        eventManager.StartCountdown();

        eventManager.endCountdown += () => {
            enemiesFactory.ActivateEnemies();
            hero.GetComponent<Hero>().Activate(); 
        };       
    }

    private void SpawnEnemy()
    {
        int numberEnemies = Random.Range(minNumberEnemies, maxNumberEnemies + 1);

        int typeEnemy=0;

        for(int i = 0; i < numberEnemies; i++)
        {
            typeEnemy = Random.Range(0,System.Enum.GetNames(typeof(Types.TypeEnemy)).Length);
            enemiesFactory.InstantiateEnemy((Types.TypeEnemy)typeEnemy, GetPosition());
        }
    }

    private void GenerateGrid()
    {
        grid = new bool[cellX, cellY];
        int indexX, indexY;

        for (int i=0; i < arena.transform.childCount; i++)
        {
            if((obstacleLayer & arena.transform.GetChild(i).gameObject.layer) ==0)
            {
                indexX = CalculationIndex(Mathf.Abs(arena.transform.GetChild(i).localPosition.x));
                indexY = CalculationIndex(Mathf.Abs(arena.transform.GetChild(i).localPosition.z));

                grid[indexX, indexY] = true;
            }
        }
    }

    private int CalculationIndex(float distance)
    {
        return System.Convert.ToInt32((distance - 0.5f * spawnStep) /spawnStep);
    }

    private Vector3 enemyPosition;

    private Vector3 GetPosition()
    {
        float posX = spawnStep / 2 + spawnStep * Random.Range(0,cellX);
        float posZ = spawnStep / 2 + spawnStep * Random.Range(0, (int)(cellY * 0.66f));

        enemyPosition = originCoordinates + new Vector3(posX, 0.5f, -posZ);

        if (!IsPlaceFree(new Vector3(posX, 0, posZ)))
        {
            GetPosition();
        }

        return enemyPosition;
    }

    public bool IsPlaceFree(Vector3 coordinates)
    {
        int indexX, indexY;
        indexX = CalculationIndex(coordinates.x);
        indexY = CalculationIndex(coordinates.z);
        bool isFree = !grid[indexX, indexY];

        if (isFree)
            grid[indexX, indexY] = true;

        return isFree;
    }
}
