using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBase : MonoBehaviour
{
    private Transform _target;
    [SerializeField] private float _speed = 10f;
    private float _damage;

    void Start()
    {
    }

    void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = _target.position - transform.position;
        float distanceThisFrame = _speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    public float GetDamage()
    {
        return _damage;
    }

    public void SetDamage(float damage)
    {
        _damage = damage;
    }

    public void FindTarget(Transform target)
    {
        _target = target;
    }

    private void HitTarget()
    {
        Destroy(gameObject);
    }
}
