using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    //Attributes
    [SerializeField] protected float _maxHealth = 50f;
    protected float _health;
    [SerializeField]
    protected float _speed = 3f;

    [SerializeField] protected int _goldValue = 10;

    private float _distanceWalked;

    //Element resistance
    public float _waterResistance = .7f;
    public float _fireResistance = .3f;
    public float _natureResistance = 0f;

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
        _maxHealth = startingHealth;
        _goldValue = goldWorth;
        _waterResistance = wRes;
        _fireResistance = fRes;
        _natureResistance = nRes;
    }

    // Use this for initialization
    void Start () {
        Debug.Log("Health: " + _maxHealth + " Gold: " + _goldValue + " wRes: " + _waterResistance + " fRes: " + _fireResistance + " nRes: " + _natureResistance);
        _health = _maxHealth;
        _healthBarTransform = healthBar.GetComponent<Transform>();
        
        _wavePointIndex = 0;

        _target = GameManager.instance.wavePoint.points[0];
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * _speed * GameManager.instance.gameSpeed * Time.deltaTime, Space.World);
	    _distanceWalked += _speed;

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
        return _distanceWalked;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.GetComponent<BulletWater>() != null)
        {
            
            BulletWater bullet = other.gameObject.GetComponent<BulletWater>();
            DecreaseHealth(bullet.GetDamage() * (1 - _waterResistance));
            bullet.HitTarget();
            
        }

        if (other.gameObject.GetComponent<BulletFire>() != null)
        {
            BulletFire bullet = other.gameObject.GetComponent<BulletFire>();
            DecreaseHealth(bullet.GetDamage() * (1 - _fireResistance));
            bullet.HitTarget();
            
        }
        if (other.gameObject.GetComponent<BulletNature>() != null)
        {
            BulletNature bullet = other.gameObject.GetComponent<BulletNature>();
            DecreaseHealth(bullet.GetDamage() * (1 - _natureResistance));
            bullet.HitTarget();
        }
    }
}
