using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public event Action<Vector3> moveStick;
    public event Action stopMoveStick;
    public event Action addCoins;
    public event Action startCountdown;
    public event Action endCountdown;
    public event Action  levelCompleted;
    public event Action  changeLevel;
    public event Action  heroKilled;
    public event Action<Vector3> enemyKilled;

    public void AddCoin()
    {
        addCoins?.Invoke();
    }

    public void MoveStick(Vector3 direction)
    {
        moveStick?.Invoke(direction);
    }

    public void StopMoveStick()
    {
        stopMoveStick?.Invoke();
    }

    public void EnemyKilled(Vector3 potitionDeath)
    {
        enemyKilled?.Invoke(potitionDeath);
    }

    public void StartCountdown()
    {
        startCountdown?.Invoke();
    }

    public void EndCountdown()
    {
        endCountdown?.Invoke();
    }

    public void LevelCompleted()
    {
        levelCompleted?.Invoke();
    }

    public void ChangeLevel()
    {
        changeLevel?.Invoke();
    }

    public void HeroKilled()
    {
        heroKilled?.Invoke();
    }
}
