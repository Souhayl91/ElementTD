
using UnityEngine;

public class Bullet : MonoBehaviour
{

    private Transform _target;
    private float speed;



    void Start()
    {
        speed = 10f;
    }
	void Update () {
	    if (_target == null)
	    {
	        Destroy(gameObject);
            return;
	    }

	    Vector3 dir = _target.position - transform.position;
	    float distanceThisFrame = speed * Time.deltaTime;

	    if (dir.magnitude <= distanceThisFrame)
	    {
	        HitTarget();
            return;
	    }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

	}
    public void FindTarget(Transform target)
    {
        _target = target;
    }

    void HitTarget()
    {
        Destroy(gameObject);
    }
}
