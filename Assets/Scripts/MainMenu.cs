using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public TextAsset jsonFile;
    public GameObject helpPanel;
    public RawImage image;

    [SerializeField]
    private float x,y;

    private GameList gamesData = new GameList();

    public void Start()
    {
        Time.timeScale = 1f;
        PlayerPrefs.SetString("Color", "Yellow");

        if (jsonFile != null)
        {
            gamesData = JsonUtility.FromJson<GameList>(jsonFile.text);          
        }
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



    //JSON

    [System.Serializable]
    public class Game
    {
        public string name;
        public string test_case;
        public string time;
    }

    [System.Serializable]
    public class GameList
    {
        public Game[] games;
    }
}
