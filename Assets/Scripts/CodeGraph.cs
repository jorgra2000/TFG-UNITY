using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodeGraph : MonoBehaviour
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

    public void CalculatePanel()
    {
        int nodeBefore = currentNode;
        currentNode = nodesScript.GetCurrentNodeNumber() - 1;
        ShowPanel(currentNode, nodeBefore);
    }

    public void ShowPanel(int node, int before)
    {
        panels[before].SetActive(false);
        panels[node].SetActive(true);
    }
}
