using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public GameObject creditsPanel;
    private GameObject mainCamera;
    private float moveSpeed = 0.9f;
    private bool right = true;

    public void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("Color", "Yellow");
        mainCamera = GameObject.Find("Main Camera");
    }


    void Update()
    {
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
}