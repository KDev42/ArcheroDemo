using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour,ITakeDamage
{
    [SerializeField] float speedMovement;
    [SerializeField] float rangeMovement;
    [SerializeField] float timeImmobility;
    [SerializeField] int hp;
    [SerializeField] float speedFire;
    [SerializeField] int damage;
    [SerializeField] bool isFlying;
    [SerializeField] Weapon weapon;

    public int Hp { get { return hp; } set { hp = value; } }
    public EnemiesFactory enemiesFactory { get; set; }
    public LevelData levelData { get; set; }

    private float standTime;
    private float shotTime;
    private float distanceTraveled;
    private Vector3 previosPosition;
    private NavMeshSurface navMeshSurface;
    private NavMeshAgent navMeshAgent;

    private enum States
    {
        move,
        stand,
        idle
    }

    private States state = States.idle;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speedMovement;
    }

    private void Update()
    {
        if(state == States.stand)
        {
            Stand();
        }
        else if(state == States.move)
        {
            Move();
        }
    }

    public void Activate()
    {
        standTime = Time.time;
        shotTime = Time.time;
        state = States.stand;
        if (isFlying)
            navMeshSurface = levelData.FlyingNavMesh;
        else
            navMeshSurface = levelData.HumanoidNavMesh;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            enemiesFactory.DeleteEnemy(gameObject);
        }
    }

    private void Stand()
    {
        transform.LookAt(levelData.Hero.transform.position);

        if (Time.time - standTime >= timeImmobility)
        {
            distanceTraveled = 0;
            previosPosition = transform.position;
            state = States.move;
            navMeshAgent.isStopped = false;
        }

        if ((Time.time - shotTime) >= speedFire)
        {
            shotTime = Time.time;
            weapon.Shot(levelData.Hero.transform.position, damage);
        }
    }

    private void Move()
    {
        distanceTraveled += Vector3.Distance(transform.position, previosPosition);
        previosPosition = transform.position;

        transform.LookAt(levelData.Hero.transform);
        navMeshAgent.SetDestination(levelData.Hero.transform.position);

        if (distanceTraveled >= rangeMovement)
        {
            standTime = Time.time;
            state = States.stand;
            navMeshAgent.isStopped = true;
        }
    }
}
