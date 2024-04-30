using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathError : MonoBehaviour
{
    public GameObject pathErrorImage; 

    private Nodes nodesScript;

    void Start()
    {
        nodesScript = GameObject.Find("Controller").GetComponent<Nodes>();
    }

    public void OnTriggerStay(UnityEngine.Collider collision)
    {
        if(collision.gameObject.tag == "GoTo " + nodesScript.GetGoToNode() || collision.gameObject.tag == "Untagged")
        {
            pathErrorImage.SetActive(false);
        }
        else
        {
            pathErrorImage.SetActive(true);
        }
    }
}
