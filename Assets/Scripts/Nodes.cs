using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Nodes : MonoBehaviour
{
    [SerializeField]
    private GameObject[] nodes;
    [SerializeField]
    private List<GameObject> nodesPath;

    private CircuitsList circuitsData = new CircuitsList();
    
    private Chrono chronoScript;
    private int currentNode = 0;
    private TMP_Text testText;

    [System.Serializable]
    public class Circuit
    {
        public string name;
        public TestCase[] test_case;
    }
    [System.Serializable]
    public class TestCase
    {
        public string case_;
        public int[] nodes;
    }

    [System.Serializable]
    public class CircuitsList
    {
        public Circuit[] circuits;
    }

    // Start is called before the first frame update
    void Start()
    {
        testText = GameObject.Find("TestCaseText").GetComponent<TMP_Text>();
        chronoScript = GameObject.Find("Controller").GetComponent<Chrono>();
        TextAsset jsonFile = Resources.Load<TextAsset>("TestCases");


        if (jsonFile != null)
        {
            circuitsData = JsonUtility.FromJson<CircuitsList>(jsonFile.text);
            string activeSceneName = SceneManager.GetActiveScene().name;

            Circuit circuit = circuitsData.circuits.FirstOrDefault(c => c.name == activeSceneName);

            if (circuit != null)
            {
                int randomCase = (int)Random.Range(0,circuit.test_case.Length);

                testText.text = circuit.test_case[randomCase].case_;

                foreach (int nodeId in circuit.test_case[randomCase].nodes)
                {
                    GameObject nodeObject = nodes.FirstOrDefault(n => n.GetComponent<Node>().GetId() == nodeId);
                    if (nodeObject != null)
                    {
                        nodesPath.Add(nodeObject);
                    }
                }
            }           
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(currentNode == nodesPath.Count)
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
        if(nodeToCheck == nodesPath[currentNode].GetComponent<Node>().GetId())
        {
            print(nodeToCheck);
            currentNode++;
        }

    }
}
