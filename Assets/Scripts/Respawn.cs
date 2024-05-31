using UnityEngine;

public class Respawn : MonoBehaviour
{
    private Nodes nodesScript;

    private GameObject car;


    void Start()
    {
        car = GameObject.Find("Player_Car");
        nodesScript = GameObject.Find("Controller").GetComponent<Nodes>();
    }


    void Update()
    {
        if(car.transform.position.y <= -20)
        {
            RespawnInNode();
        }
    }

    public void RespawnInNode()
    {
        car.transform.position = nodesScript.GetCurrentNode().transform.position;
        car.transform.rotation = nodesScript.GetCurrentNode().transform.rotation;
        car.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
