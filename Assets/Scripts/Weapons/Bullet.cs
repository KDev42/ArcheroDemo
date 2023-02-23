using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] LayerMask obstacleLayer;

    private float speed;
    private int damage;

    public void Initialize(float speed,int damage)
    {
        this.damage = damage;
        this.speed = speed;
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent(typeof(ITakeDamage)) && ((1 << other.gameObject.layer) & targetLayer) != 0)
        {
            other.GetComponent<ITakeDamage>().TakeDamage(damage);
            Destroy(gameObject);
        }

        if(((1 << other.gameObject.layer) & obstacleLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
