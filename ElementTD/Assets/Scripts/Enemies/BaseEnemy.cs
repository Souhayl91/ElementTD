using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    //Attributes
    [SerializeField]
    protected float _health;
    [SerializeField]
    protected float _speed;

    //Element resistance
    private float _waterResistance;
    private float _fireResistance;
    private float _natureResistance;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private int _wavePointIndex;
    // Use this for initialization
    void Start () {
        _health = 60;
        _speed = 3;
        _wavePointIndex = 0;

        _target = Waypoint.points[0];

        _waterResistance = 0;
        _fireResistance = 0;
        _natureResistance = 0;

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

        if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
        {
            GetNextWayPoint();
        }
    }
    void GetNextWayPoint()
    {
        _wavePointIndex++;
        if (_wavePointIndex >= Waypoint.points.Length - 1)
        {
            _wavePointIndex = 0;
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

    public void SetSpeed(float speed)
    {
        _speed = speed;
    }
}
