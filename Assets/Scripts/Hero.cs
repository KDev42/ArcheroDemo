using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

public class Hero : MonoBehaviour,ITakeDamage
{
    [SerializeField] float speedMovement;
    [SerializeField] int hp;
    [SerializeField] float speedFire;
    [SerializeField] int damage;
    [SerializeField] Weapon weapon;

    [Inject] ClosestTarget closestTarget;
    [Inject] EventManager eventManager;

    private int startHp;
    private float timeShot;
    private NavMeshAgent navMeshAgent;

    private enum States
    {
        move,
        stand,
        idle
    }

    private States state;

    public int Hp { get { return hp; } set { hp = value; } }

    private void Start()
    {
        startHp = hp;
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speedMovement;

        eventManager.moveStick += Move;
        eventManager.stopMoveStick += StopMove;
    }

    private void Update()
    {
        if(state == States.stand)
        {
            Stand();
        }
    }

    public void Activate()
    {
        navMeshAgent.enabled = true;
        timeShot = Time.time;
        hp = startHp;
        state = States.stand;
    }

    public void SetIdle()
    {
        state = States.idle;

        navMeshAgent.enabled = false;
    }

    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if (Hp <= 0)
        {
            eventManager.HeroKilled();
        }
    }

    private void Move(Vector3 direction)
    {
        if (state != States.idle)
        {
            state = States.move;
            transform.LookAt(transform.position + new Vector3(direction.x, 0, direction.y));

            navMeshAgent.SetDestination(transform.position + transform.forward);
        }
    }

    private void StopMove()
    {
        state = States.stand;
    }

    private void Stand()
    {
        if (closestTarget.HasTarget(transform.position, out Transform target))
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            if ((Time.time - timeShot) >= speedFire)
            {
                timeShot = Time.time;
                weapon.Shot(target.position,damage);
            }
        }
    }
}
