using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float _speed;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private int wavePointIndex;

    // Use this for initialization
    void Start ()
    {
        _speed = 5f;
        wavePointIndex = 0;

        _target = Waypoint.points[0];
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Vector3 dir = _target.position - transform.position;
        transform.Translate(dir.normalized * _speed * Time.deltaTime, Space.World);

	    if (Vector3.Distance(transform.position, _target.position) <= 0.4f)
	    {
	        GetNextWayPoint();
	    }
	}

    void GetNextWayPoint()
    {
        wavePointIndex++;
        if (wavePointIndex >= Waypoint.points.Length - 1)
        {
            wavePointIndex = 0;
        }
        
        _target = Waypoint.points[wavePointIndex];
    }
}
