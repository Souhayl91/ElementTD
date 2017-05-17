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
    public float _waterResistance;
    public float _fireResistance;
    public float _natureResistance;

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
    void Start ()
    {
        _waterResistance = 0.7f;
        _fireResistance = 0.3f;
        _natureResistance = 0.0f;

        Color color = new Color();

        color.r = _fireResistance;
        color.g = _natureResistance;
        color.b = _waterResistance;
        color.a = 1;

        GetComponent<SpriteRenderer>().color = color;
        Debug.Log("Health: " + _maxHealth + " Gold: " + _goldValue + " wRes: " + _waterResistance + 
                " fRes: " + _fireResistance + " nRes: " + _natureResistance);
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

            //TODO: FIX THIS PLEASE 
            int index = GameManager.instance.waveManager.enemies.IndexOf(this.gameObject);
            Destroy(GameManager.instance.waveManager.enemiesUI[index]);
            GameManager.instance.waveManager.enemiesUI.RemoveAt(index);
            GameManager.instance.waveManager.enemies.RemoveAt(index);
            
            Debug.Log("Enemies " + GameManager.instance.waveManager.enemies.Count);
            Debug.Log("Enemy UI " + GameManager.instance.waveManager.enemiesUI.Count);
            GameManager.instance.waveManager.enemies.Remove(this.gameObject);
            
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
