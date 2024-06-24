using UnityEngine;

public class CodeImage : MonoBehaviour
{
    public GameObject[] panels;

    private Nodes nodesScript;
    private int currentNode = 0;

    void Start()
    {
        nodesScript = GameObject.Find("Controller").GetComponent<Nodes>();
    }

    void Update()
    {
        CalculatePanel();
    }

    public void SetCurrentNode(int node)
    {
        currentNode = node;
    }

    public void CalculatePanel()
    {
        int nodeBefore = currentNode;
        currentNode = nodesScript.GetCurrentNodeNumber() - 1;
        if(currentNode == 98)
        {
            currentNode = nodesScript.GetMaxNode() - 1;
        }
        if(currentNode < 0)
        {
            currentNode = 0;
        }
        ShowPanel(currentNode, nodeBefore);
    }

    public void ShowPanel(int node, int before)
    {
        panels[before].SetActive(false);
        panels[node].SetActive(true);
    }
}
