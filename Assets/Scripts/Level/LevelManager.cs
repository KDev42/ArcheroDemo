using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> arenas;
    [SerializeField] GameObject door;

    [Inject] LevelStart levelStart;
    [Inject] EventManager eventManager;
    [Inject] EnemiesFactory enemiesFactory;

    private GameObject currentArena;

    private void Start()
    {
        SpawnArena();
        eventManager.levelCompleted += LevelCompleted;
        eventManager.changeLevel += SpawnArena;
        eventManager.heroKilled += SpawnArena;
    }

    private void LevelCompleted()
    {
        door.SetActive(true);
    }

    private void SpawnArena()
    {
        enemiesFactory.DeleteAllEnemies();
        door.SetActive(false);
        if (currentArena!=null)
        {
            Destroy(currentArena);
        }

        currentArena = Instantiate(arenas[Random.Range(0, arenas.Count)], new Vector3(-3,0,4), new Quaternion(0, 0, 0, 0));

        levelStart.StartLevel(currentArena);
    }
}
