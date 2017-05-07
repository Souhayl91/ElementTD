using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform _target;
    private float range;
    private float _fireRate;
    private float _fireCountDown;
    public GameObject bullet;
    public Transform bulletSpawn;

    private GameObject[] enemies;


	// Use this for initialization
	void Start () {
		InvokeRepeating("UpdateTarget", 0f, 0.5f);
	    range = 10f;
	    _fireRate = 1f;
	    _fireCountDown = 0f;
	    bulletSpawn = GetComponentInChildren<Transform>().GetChild(0);
	}
	
	// Update is called once per frame
	void Update () {
	    if (_target == null)
	    {
	        return;
	    }

	    Vector3 dir = _target.position - transform.position;
	    Quaternion lookRotation = Quaternion.LookRotation(dir);

        Quaternion rotation = Quaternion.LookRotation(_target.position - transform.position, transform.TransformDirection(Vector3.back));
        rotation.x = 0;
        rotation.y = 0;
        transform.rotation = rotation;

	    if (_fireCountDown <= 0f)
	    {
	        Shoot();
	        _fireCountDown = 1f / _fireRate;
	    }
	    _fireCountDown -= Time.deltaTime;
	}

    void Shoot()
    {

        GameObject newBullet = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        Bullet b = newBullet.GetComponent<Bullet>();
        if(b != null)
            b.FindTarget(_target);
    }
    void UpdateTarget()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        if (nearestEnemy != null && shortestDistance <= range)
        {
            _target = nearestEnemy.transform;
        }
        
    }
    //void ChangeTarget()
    //{
    //    Transform newTarget =
    //    _target = newTarget;
    //}

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
