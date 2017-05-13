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
    
    //Static
    //Element resistance
    private float _waterResistance = .7f;
    private float _fireResistance = .3f;
    private float _natureResistance = 0;

    //Move target
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private int _wavePointIndex;

    //Healthbar
    public GameObject healthBar;
    private Transform _healthBarTransform;

    // Use this for initialization
    void Start ()
    {
        _waterResistance = Random.Range(0, 1.1f);
        _fireResistance = Random.Range(0, 1.1f);
        _natureResistance = Random.Range(0, 1.1f);
        Debug.Log(_waterResistance);
        Debug.Log(_fireResistance);
        Debug.Log(_natureResistance);


        Color color = new Color();

        color.r = _fireResistance;
        color.g = _natureResistance;
        color.b = _waterResistance;
        color.a = 1;

        GetComponent<SpriteRenderer>().color = color;

        _health = _maxHealth;
        _healthBarTransform = healthBar.GetComponent<Transform>();
        
        _wavePointIndex = 0;

        _target = Waypoint.points[0];

       
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
        if (_wavePointIndex >= Waypoint.points.Length)
        {
            _wavePointIndex = 0;
            //TODO: Remove live from player
            GameManager.instance.data.DecreaseHP(1);
            //Debug.Log(GameManager.instance.data.GetHP());
        }

        _target = Waypoint.points[_wavePointIndex];
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
