using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectVehicle : MonoBehaviour
{
    public GameObject car;
    public GameObject bodyCar;
    public Material[] colors;
    public GameObject mutedImage;

    private float rotationSpeed = 10f;

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
        PlayerPrefs.SetString("Color", "Yellow");
    }

    void Update()
    {
        CheckAudio();
        car.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }

    public void GoToCircuit()
    {
        SceneManager.LoadScene("CircuitSelect");
    }

    public void ColorYellow()
    {
        bodyCar.GetComponent<Renderer>().material = colors[0];
        PlayerPrefs.SetString("Color", "Yellow");
    }

    public void ColorBlue()
    {
        bodyCar.GetComponent<Renderer>().material = colors[1];
        PlayerPrefs.SetString("Color", "Blue");
    }

    public void ColorRed()
    {
        bodyCar.GetComponent<Renderer>().material = colors[2];
        PlayerPrefs.SetString("Color", "Red");
    }

    public void ColorGray()
    {
        bodyCar.GetComponent<Renderer>().material = colors[3];
        PlayerPrefs.SetString("Color", "Gray");
    }

    public void ColorPurple()
    {
        bodyCar.GetComponent<Renderer>().material = colors[4];
        PlayerPrefs.SetString("Color", "Purple");
    }

    public void BackScene()
    {
        SceneManager.LoadScene("MainMenu");
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
}
