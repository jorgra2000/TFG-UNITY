using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircuitSelect : MonoBehaviour
{
    public TextAsset jsonFile;
    public Image backImage;
    public Image codeImage;

    private CircuitsList circuitsData = new CircuitsList();
    private int currentNumberCircuit = 0;

    private GameObject leftButton;
    private GameObject rightButton;

    void Start()
    {
        if (jsonFile != null)
        {
            circuitsData = JsonUtility.FromJson<CircuitsList>(jsonFile.text);
        }

        leftButton = GameObject.Find("LeftButton");
        rightButton = GameObject.Find("RightButton");

        backImage.sprite = Resources.Load<Sprite>("ImageCircuits/Screen1");
        codeImage.sprite = Resources.Load<Sprite>("Code/CodeCircuit1");
    }

    void Update()
    {
        backImage.sprite = Resources.Load<Sprite>("ImageCircuits/Screen" + (currentNumberCircuit+1));
        codeImage.sprite = Resources.Load<Sprite>("Code/CodeCircuit" + (currentNumberCircuit+1));

        if(currentNumberCircuit == 0)
        {
            leftButton.SetActive(false);
            rightButton.SetActive(true);
        }
        else if(currentNumberCircuit == circuitsData.circuits.Length - 1)
        {
            rightButton.SetActive(false);
            leftButton.SetActive(true);
        }
        else
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Circuit " + (currentNumberCircuit+1));
    }

    public void LeftButton()
    {
        if(currentNumberCircuit > 0)
            currentNumberCircuit--;
    }

    public void RightButton()
    {
        if(currentNumberCircuit < circuitsData.circuits.Length - 1)
            currentNumberCircuit++;
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
        public int[] nodes;
    }

    [System.Serializable]
    public class CircuitsList
    {
        public Circuit[] circuits;
    }
}


