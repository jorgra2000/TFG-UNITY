using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
    private Nodes controllerScript;

    [SerializeField] private int id;

    private void Start()
    {
        controllerScript = GameObject.Find("Controller").GetComponent<Nodes>();
    }
    
    public int GetId()
    {
        return id;
    }

    public void OnTriggerEnter(){
        controllerScript.CheckNode(id);
    }

}
