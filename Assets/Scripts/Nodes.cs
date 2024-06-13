using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Nodes : MonoBehaviour
{
    public GameObject finishPanel;
    public TextAsset jsonFile;
    
    [SerializeField] private GameObject[] nodes;
    [SerializeField] private List<GameObject> nodesPath;
    [SerializeField] private string[] localVariablesNames;
    [SerializeField] private int[] localVariablesValue;
    [SerializeField] private int maxNode;

    public TMP_Text countdownText;
    public AudioClip song;
    public AudioClip finishAudio;
    public GameObject pauseButton;

    private CircuitsList circuitsData = new CircuitsList();

    
    private Chrono chronoScript;
    private int currentNode = 0;
    private TMP_Text testText;
    private TMP_Text variablesText;
    private string builderVariablesText = "";
    private int inputValue;
    private int countdownValue = 3;
    private GameObject musicManager;
    


    void Awake()
    {
        GameObject musicManager = GameObject.Find("MusicManager");
        Destroy(musicManager);
    }

    void Start()
    {
        variablesText = GameObject.Find("VariablesText").GetComponent<TMP_Text>();
        testText = GameObject.Find("TestCaseText").GetComponent<TMP_Text>();
        chronoScript = GameObject.Find("Controller").GetComponent<Chrono>();
        musicManager = GameObject.Find("MusicManagerGame");

        if (jsonFile != null)
        {
            circuitsData = JsonUtility.FromJson<CircuitsList>(jsonFile.text);
            string activeSceneName = SceneManager.GetActiveScene().name;

            Circuit circuit = circuitsData.circuits.FirstOrDefault(c => c.name == activeSceneName);

            if (circuit != null)
            {
                int randomCase = (int)Random.Range(0,circuit.test_case.Length);

                testText.text = circuit.test_case[randomCase].case_;

                inputValue = circuit.test_case[randomCase].input;

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

        UpdateVariablesText();

        StartCoroutine(CountdownStart());
        //TO DO: Esperar un segundo o dos para hacer el sonido de 3 2 1
    }

    void Update()
    {
        if(currentNode == nodesPath.Count)
        {
            if(!chronoScript.GetFinishedRace())
            {
                chronoScript.SetFinishedRace(true);
                musicManager.GetComponent<AudioSource>().Stop();
                musicManager.GetComponent<AudioSource>().loop = false;
                musicManager.GetComponent<AudioSource>().clip = finishAudio;
                musicManager.GetComponent<AudioSource>().Play();
                StartCoroutine(ShowFinishPanel());
            }
        }
    }

    public bool CheckNode(int nodeToCheck)
    {
        try
        {
            if(nodeToCheck == nodesPath[currentNode].GetComponent<Node>().GetId())
            {
                currentNode++;
                return true;
            }

            return false;
        }
        catch
        {
            return false;
        }

    }

    public int GetCurrentNodeNumber()
    {
        try
        {
            return nodesPath[currentNode-1].GetComponent<Node>().GetId();
        }
        catch
        {
            return 0;
        }

    }

    public GameObject GetCurrentNode()
    {
        return nodesPath[currentNode-1];
    }

    public int GetMaxNode()
    {
        return maxNode;
    }

    public int GetGoToNode()
    {
        try
        {
            return nodesPath[currentNode].GetComponent<Node>().GetId();
        }
        catch
        {
            return -999;
        }
    }

    IEnumerator ShowFinishPanel()
    {
        yield return new WaitForSeconds(2);

        finishPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CalculateVariables(int index, int num)
    {
        localVariablesValue[index] += num;
    }

    public void UpdateVariablesText()
    {
        for(int i = 0; i < localVariablesNames.Length; i++)
        {
            builderVariablesText += localVariablesNames[i] + " = " + localVariablesValue[i] + "  "; 
        }

        variablesText.text = builderVariablesText;

        builderVariablesText = "";
    }

    public int GetInputValue()
    {
        return inputValue;
    }


    IEnumerator CountdownStart()
    {
        while(countdownValue > 0)
        {
            countdownText.text = countdownValue.ToString();

            yield return new WaitForSeconds(1f);

            countdownValue--;
        }

        countdownText.text = "GO!";

        yield return new WaitForSeconds(1f);
        chronoScript.SetFinishedRace(false);

        countdownText.gameObject.SetActive(false);
        pauseButton.SetActive(true);

        musicManager.GetComponent<AudioSource>().clip = song;
        musicManager.GetComponent<AudioSource>().loop = true;
        musicManager.GetComponent<AudioSource>().Play();
    }


    //JSON circuits

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
        public int input;
        public int[] nodes;
    }

    [System.Serializable]
    public class CircuitsList
    {
        public Circuit[] circuits;
    }
}
