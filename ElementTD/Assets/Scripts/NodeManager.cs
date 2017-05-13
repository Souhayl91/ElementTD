using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{

    public List<Node> nodes = new List<Node>();
    public Shop shop;
    
    private int _nodeNumber;
    public Node nodeInstance;
    public Node selectedNode;
    
    // Use this for initialization
    void Start ()
    {
        AddNodes();
     
    }
	
	// Update is called once per frame
	void Update ()
	{
	  
	}

    void LateUpdate()
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
            
            CycleNodes();
        }
    }

    private void CycleNodes()
    {
        
        foreach (Node node in nodes)
        {
            
            if (node.isClicked)
            {
                if (selectedNode != null)
                {
                    selectedNode.isClicked = false;
                }
                selectedNode = node;
                
 
            }
        }
    }

}
