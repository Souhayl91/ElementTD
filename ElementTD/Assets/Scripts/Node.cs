﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public bool isClicked;

    public GameObject _tower;
    private SpriteRenderer _rend;
    public Color startColor;
    private bool _isHovering;


	// Use this for initialization
	void Start ()
	{
        _rend = GetComponent<SpriteRenderer>();
	    hoverColor = Color.green;
	    startColor = _rend.material.color;
	}

    void OnMouseDown()
    {
        if (isClicked == false)
        {
            Debug.Log("here");
            _rend.material.color = hoverColor;  
            isClicked = true;
            
        }
        else if (isClicked == true)
        {

          _rend.material.color = startColor;
            isClicked = false;
        }
        
    }

    void OnMouseEnter()
    {
        _isHovering = true;
        _rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        _isHovering = false;
        if (isClicked == false)
        {
            
            _rend.material.color = startColor;
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (isClicked == false && _isHovering == false)
	    {
	        _rend.material.color = startColor;
        }
	}
}
