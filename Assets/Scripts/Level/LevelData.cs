using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelData : MonoBehaviour
{
    public GameObject Hero { get; set; }
    public GameObject arena { get; set; }
    public NavMeshSurface HumanoidNavMesh { get; set; }
    public NavMeshSurface FlyingNavMesh { get; set; }

    public void InitializeNavMesh(GameObject arena)
    {
        NavMeshSurface[] navMeshes = arena.GetComponents<NavMeshSurface>();
        this.arena = arena;
        HumanoidNavMesh = navMeshes[0];
        FlyingNavMesh = navMeshes[1];
    }
}
