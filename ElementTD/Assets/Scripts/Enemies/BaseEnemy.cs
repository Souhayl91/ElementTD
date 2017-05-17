﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    [System.Serializable]
    public class Gene
    {
        public float waterResistance;
        public float fireResistance;
        public float natureResistance;
        public float distanceWalked;
        public float damageTaken;

        public new string ToString()
        {
            return "wRes: " + waterResistance + " fRes: " + fireResistance + " nRes: " + natureResistance + " damage taken: " + damageTaken;
        }
    }

    //Attributes
    [SerializeField] protected float _maxHealth = 50f;
    protected float _health;
    [SerializeField]
    protected float _speed = 3f;

    [SerializeField] protected int _goldValue = 10;

    //private float _distanceWalked;

    //Element resistance
    public Gene gene = new Gene();

    //Move target
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private int _wavePointIndex;

    //Healthbar
    public GameObject healthBar;
    private Transform _healthBarTransform;

    public void SetStats(float startingHealth, int goldWorth, float wRes, float fRes, float nRes)
    {
        //Debug.Log("WTF - Health: " + _maxHealth + " Gold: " + _goldValue + " wRes: " + gene.waterResistance + " fRes: " + gene.fireResistance + " nRes: " + gene.natureResistance);
        _maxHealth = startingHealth;
        _goldValue = goldWorth;
        gene.waterResistance = wRes;
        gene.fireResistance = fRes;
        gene.natureResistance = nRes;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Health: " + _maxHealth + " Gold: " + _goldValue + " " + gene.ToString());
        _health = _maxHealth;
        _healthBarTransform = healthBar.GetComponent<Transform>();
        
        _wavePointIndex = 0;

        _target = GameManager.instance.wavePoint.points[0];
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * _speed * GameManager.instance.gameSpeed * Time.deltaTime, Space.World);
	    gene.distanceWalked += _speed;

        if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }
    void GetNextWayPoint()
    {
        _wavePointIndex++;
        if (_wavePointIndex >= GameManager.instance.wavePoint.points.Length)
        {
            _wavePointIndex = 0;
            //TODO: Remove live from player
            GameManager.instance.data.DecreaseHP(1);
            //Debug.Log(GameManager.instance.data.GetHP());
        }

        _target = GameManager.instance.wavePoint.points[_wavePointIndex];
    }

    public float GetHealth()
    {
        return _health;
    }

    public float GetSpeed()
    {
        return _speed;
    }
    public void SetHealth(float health)
    {
        _health = health;
    }


    public void DecreaseHealth(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            //TODO: THIS IS WHERE THE ENEMY DIES
            GameManager.instance.data.IncreaseGold(_goldValue);
            GameManager.instance.genetics.AddGene(gene);

            Destroy(this.gameObject);
            return;
        }

        _healthBarTransform.localScale = new Vector3(_health / _maxHealth, _healthBarTransform.localScale.y, _healthBarTransform.localScale.z);
    }

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }

    public float GetDistanceWalked()
    {
        return gene.distanceWalked;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<BulletWater>() != null && other.gameObject.GetComponent<BulletWater>()._target == this.gameObject.GetComponent<Transform>())
        {
            
            BulletWater bullet = other.gameObject.GetComponent<BulletWater>();
            gene.damageTaken += bullet.GetDamage();
            DecreaseHealth(bullet.GetDamage() * (1 - gene.waterResistance));
            bullet.HitTarget();
            
        }

        if (other.gameObject.GetComponent<BulletFire>() != null)
        {
            BulletFire bullet = other.gameObject.GetComponent<BulletFire>();
            gene.damageTaken += bullet.GetDamage();
            DecreaseHealth(bullet.GetDamage() * (1 - gene.fireResistance));
            bullet.HitTarget();
            
        }
        if (other.gameObject.GetComponent<BulletNature>() != null)
        {
            BulletNature bullet = other.gameObject.GetComponent<BulletNature>();
            gene.damageTaken += bullet.GetDamage();
            DecreaseHealth(bullet.GetDamage() * (1 - gene.natureResistance));
            bullet.HitTarget();
        }
    }
}
