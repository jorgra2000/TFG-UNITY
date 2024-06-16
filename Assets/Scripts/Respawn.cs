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
        GameObject respawnPlace;
        if(nodesScript.GetCurrentNode().GetComponent<Node>().GetId() == 99)
        {
            respawnPlace = nodesScript.GetBeforeNode();
        }
        else
        {
            respawnPlace = nodesScript.GetCurrentNode();
        }

        car.transform.position = respawnPlace.transform.position;
        car.transform.rotation = respawnPlace.transform.rotation;

        car.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
