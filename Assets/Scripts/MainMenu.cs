using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject helpPanel;
    public RawImage image;

    [SerializeField]
    private float x,y;

    public void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("Color", "Yellow");
    }

    void Update()
    {
        image.uvRect = new Rect(image.uvRect.position + new Vector2(x,y) * Time.deltaTime, image.uvRect.size);
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
