using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;

    private GameObject _tower;

    private SpriteRenderer _rend;
    private Color _startColor;


	// Use this for initialization
	void Start ()
	{
        _rend = GetComponent<SpriteRenderer>();
	    hoverColor = Color.black;
	    _startColor = _rend.material.color;
	}

    void OnMouseDown()
    {
        if (BuildManager.instance.GetTowerToBuild() == null)
        {
            return;
        }

        if (_tower != null)
        {
            Debug.Log("Already a tower placed at this node!");
            return;
        }

        GameObject towerToBuild = BuildManager.instance.GetTowerToBuild();
        _tower = Instantiate(towerToBuild, transform.position, transform.rotation);

    }

    void OnMouseEnter()
    {
        _rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        _rend.material.color = _startColor;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
