using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;

    public void Start()
    {
        PlayerPrefs.SetString("Color", "Yellow");
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
}
