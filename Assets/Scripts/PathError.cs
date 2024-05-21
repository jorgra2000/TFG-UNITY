using UnityEngine;

public class PathError : MonoBehaviour
{
    public GameObject pathErrorImage; 

    private Nodes nodesScript;
    private Respawn respawnScript;
    private float timeErrorPath = 0f;

    void Start()
    {
        nodesScript = GameObject.Find("Controller").GetComponent<Nodes>();
        respawnScript = GameObject.Find("Controller").GetComponent<Respawn>();
    }

    public void OnTriggerStay(Collider collision)
    {
        if(collision.gameObject.tag == "GoTo " + nodesScript.GetGoToNode() || collision.gameObject.tag == "Untagged")
        {
            pathErrorImage.SetActive(false);
            timeErrorPath = 0f;
        }
        else
        {
            pathErrorImage.SetActive(true);
            timeErrorPath += Time.deltaTime;

            if (timeErrorPath >= 5f)
            {
                respawnScript.RespawnInNode();
            }
        }
    }
}
