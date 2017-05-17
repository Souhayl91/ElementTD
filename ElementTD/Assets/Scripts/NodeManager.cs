using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    public List<Node> nodes = new List<Node>();
    public Shop shop;
    private GameObject _shopGameObject;
    private int _nodeNumber;
    public Node nodeInstance;
    public Node selectedNode;
    private Node _lastNode;
    // Use this for initialization
    void Start ()
    {
        _shopGameObject = GameObject.Find("Shop");
        shop = _shopGameObject.GetComponent<Shop>();
        AddNodes();
     
    }
	
	// Update is called once per frame
	void Update ()
	{
	    Pressed();
    }


    private void AddNodes()
    {
        GameObject[] gameNodes = GameObject.FindGameObjectsWithTag("Node");
        //Debug.Log(gameNodes.Length);
        foreach (GameObject node in gameNodes)
        {
            nodeInstance = node.GetComponent<Node>();
            nodes.Add(nodeInstance);

        }
    }

    void Pressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            

            if (hit != null && hit.collider != null && hit.collider.tag == "Node")
            {

                    CycleNodes();
            }

         


            
        }
    }

    private void CycleNodes()
    {
        
        foreach (Node node in nodes)
            {

                if (node.isClicked && node != _lastNode)
                {

                    selectedNode = node;

                }
            }
        if (_lastNode != null && _lastNode != selectedNode)
        {
            _lastNode.isClicked = false;
        }
        if (selectedNode != null && _lastNode != selectedNode)
        {
            _lastNode = selectedNode;

        }
        if (_lastNode != null && selectedNode != null) Debug.Log(_lastNode.isClicked);
    }

}
