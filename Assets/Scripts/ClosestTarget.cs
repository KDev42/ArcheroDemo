using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ClosestTarget : MonoBehaviour
{
    [Inject] EnemiesFactory enemiesFactory;

    public bool HasTarget(Vector3 origin, out Transform target)
    {
        target = null;
        if (enemiesFactory.enemies.Count > 0)
        {
            target = ClosestEnemy(origin);
            return true;
        }

        return false;
    }

    private Transform ClosestEnemy(Vector3 origin)
    {
        Transform closestEnemy =null;
        float closestDistance = 1000;
        float currentDistance;

        foreach(GameObject enemy in enemiesFactory.enemies)
        {
            currentDistance = (origin - enemy.transform.position).magnitude;

            if (currentDistance < closestDistance)
            {
                closestDistance = currentDistance;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }
}
