using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject creditsPanel;
    public GameObject mutedImage;
    private GameObject mainCamera;
    private float moveSpeed = 0.9f;
    private bool right = true;

    public void Awake()
    {
        if(PlayerPrefs.GetInt("Muted") == 1)
        {
            GameObject.Find("MusicManager").GetComponent<AudioSource>().enabled = false;
            mutedImage.SetActive(true);
        }
    }

    public void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("Color", "Yellow");
        mainCamera = GameObject.Find("Main Camera");
    }


    void Update()
    {
        CheckAudio();
        if(right && mainCamera.transform.position.x < 7.5f)
        {
            mainCamera.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        else
        {
           right = false; 
        }

        if(!right && mainCamera.transform.position.x > -9f)
        {
            mainCamera.transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        else
        {
           right = true; 
        }
        
    }

    public void StartSinglePlayer()
    {
        SceneManager.LoadScene("SelectCar");
    }

    public void HelpButton()
    {
        helpPanel.SetActive(true);
    }

    public void ExitHelpButton()
    {
        helpPanel.SetActive(false);
    }

    public void CreditsButton()
    {
        creditsPanel.SetActive(true);
    }

    public void ExitCreditsButton()
    {
        creditsPanel.SetActive(false);
    }

    public void GoToItchio()
    {
        Application.OpenURL("https://jorgra2000-games.itch.io/");
    }

    //Only Windows version
    public void ExitGame()
    {
        Application.Quit();
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