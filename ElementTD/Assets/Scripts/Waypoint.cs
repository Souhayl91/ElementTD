using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Transform[] points;

	// Use this for initialization
	public void SetWayPoints (GameObject wayPointParent) {
        points = new Transform[wayPointParent.transform.childCount];
	    for (int i = 0; i < points.Length; i++)
	    {
	        points[i] = wayPointParent.transform.GetChild(i);
	    }
	}
}
