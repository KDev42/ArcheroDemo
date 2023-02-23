using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    [SerializeField] float speedBullet;

    public void Shot(Vector3 target, int damage)
    {
        GameObject bullet = Instantiate(bulletPrefab.gameObject, transform.position, new Quaternion(0, 0, 0, 0));

        bullet.transform.LookAt(new Vector3(target.x,bullet.transform.position.y,target.z));
        bullet.GetComponent<Bullet>().Initialize(speedBullet, damage);
    }
}
