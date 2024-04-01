using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes : MonoBehaviour
{
    [SerializeField] private GameObject[] nodes;
    
    private Chrono chronoScript;
    private int currentNode = 0;

    // Start is called before the first frame update
    void Start()
    {
        chronoScript = GameObject.Find("Controller").GetComponent<Chrono>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentNode == nodes.Length)
        {
            if(!chronoScript.getFinishedRace())
            {
                chronoScript.setFinishedRace(true);
                print("Terminado");
            }

        }
    }

    public void CheckNode(int nodeToCheck)
    {
        if(nodeToCheck == nodes[currentNode].GetComponent<Node>().GetId())
        {
            print(nodeToCheck);
            currentNode++;
        }

    }
}
