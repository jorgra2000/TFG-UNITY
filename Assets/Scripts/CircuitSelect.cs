using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CircuitSelect : MonoBehaviour
{
    public TextAsset jsonFile;
    public Image codeImage;
    public Material[] skyboxBackground;
    public GameObject mutedImage;

    private CircuitsList circuitsData = new CircuitsList();
    private int currentNumberCircuit = 0;

    private GameObject leftButton;
    private GameObject rightButton;

    public void Awake()
    {
        if(PlayerPrefs.GetInt("Muted") == 1)
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().enabled = false;
            mutedImage.SetActive(true);
        }
    }

    void Start()
    {
        if (jsonFile != null)
        {
            circuitsData = JsonUtility.FromJson<CircuitsList>(jsonFile.text);
        }

        leftButton = GameObject.Find("LeftButton");
        rightButton = GameObject.Find("RightButton");

        codeImage.sprite = Resources.Load<Sprite>("Code/CodeCircuit0");
        RenderSettings.skybox = skyboxBackground[0];
    }

    void Update()
    {
        CheckAudio();
        codeImage.sprite = Resources.Load<Sprite>("Code/CodeCircuit" + (currentNumberCircuit));
        RenderSettings.skybox = skyboxBackground[currentNumberCircuit];

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
        SceneManager.LoadScene("Circuit " + (currentNumberCircuit));
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

    public void BackScene()
    {
        SceneManager.LoadScene("SelectCar");
    }

        public void MuteButton()
    {
        if(PlayerPrefs.GetInt("Muted") == 0)
        {
            PlayerPrefs.SetInt("Muted", 1);
            mutedImage.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("Muted", 0);
            mutedImage.SetActive(false);
        }
    }

    public void CheckAudio()
    {
        if(PlayerPrefs.GetInt("Muted") == 1)
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().enabled = false;
        }
        else
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().enabled = true;
        }
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


