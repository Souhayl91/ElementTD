﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBase : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletSpawn;

    private GameObject[] _enemies;
    private Transform _target;
    [SerializeField] protected float _range = 7f;
    [SerializeField] protected float _fireRate = 1f;
    [SerializeField] protected float _damage = 10f;
    private float _fireCountDown;

    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        bulletSpawn = GetComponentInChildren<Transform>().GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTarget();

        if (_target == null)
        {
            return;
        }

        //Vector3 dir = _target.position - transform.position;
        //Quaternion lookRotation = Quaternion.LookRotation(dir);

        Quaternion rotation = Quaternion.LookRotation(_target.position - transform.position, transform.TransformDirection(Vector3.back));
        rotation.x = 0;
        rotation.y = 0;
        transform.rotation = rotation;

        if (_fireCountDown <= 0f)
        {
            Shoot();
            _fireCountDown = 1f / _fireRate;
        }
        _fireCountDown -= Time.deltaTime * GameManager.instance.gameSpeed;
    }

    void Shoot()
    {

        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        BulletBase b = newBullet.GetComponent<BulletBase>();
        b.SetDamage(_damage);
        b.FindTarget(_target);
    }

    void UpdateTarget()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //float shortestDistance = Mathf.Infinity;
        float furthestEnemy = 0f;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in _enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            float enemyDistanceWalked = enemy.GetComponent<BaseEnemy>().GetDistanceWalked();
            if (distanceToEnemy <= _range && enemyDistanceWalked > furthestEnemy)
            {
                furthestEnemy = enemyDistanceWalked;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null)
        {
            _target = nearestEnemy.transform;
        }
        else
        {
            _target = null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _range);
    }
}
