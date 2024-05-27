using UnityEngine;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using System.IO;

public class Chrono : MonoBehaviour
{
    public TMP_Text timeTextFinished;
    public TextAsset jsonFile;
    public TMP_Text testCaseText;

    private float currentTime = 0f;
    private int secs, min;
    private bool finishedRace = true;
    private TMP_Text timeText;

    private GameList gamesData = new GameList();

    private void Start()
    {
        timeText = GameObject.Find("TimeText").GetComponent<TMP_Text>();
    }

    void Update()
    {
        if(!finishedRace)
        {
            AdvanceChrono();
        }
        else
        {
            timeTextFinished.text = timeText.text;
            AddGameJSON();
        }

    }

    private void UpdateChrono()
    {
        string formatedTime = string.Format("{0:0}:{1:00}.{2:00}", min, secs, (int)(currentTime * 100 % 100));

        timeText.text = formatedTime;
    }

    private void AdvanceChrono()
    {
        currentTime += Time.deltaTime;

        secs = (int)(currentTime % 60);
        min = (int)(currentTime / 60 % 60);

        UpdateChrono();
    }

    public void SetFinishedRace(bool finishedRace)
    {
        this.finishedRace = finishedRace;
    }

    public bool GetFinishedRace()
    {
        return finishedRace;
    }

    public void AddGameJSON()
    {

        GameList gameList = new GameList();

        Game newGame = new Game
        {
            name = SceneManager.GetActiveScene().name,
            test_case = testCaseText.text,
            time = timeTextFinished.text
        };

        AddGameToList(gameList, newGame);

        string json = JsonUtility.ToJson(gameList, true);
        File.WriteAllText(Application.dataPath + "/TimeRecords.json", json);
    }

    public GameList AddGameToList(GameList gameData, Game newGame)
    {
        Game[] updatedGames = new Game[gameData.games.Length + 1];

        for (int i = 0; i < gameData.games.Length; i++)
        {
            updatedGames[i] = gameData.games[i];
        }

        updatedGames[updatedGames.Length - 1] = newGame;

        gameData.games = updatedGames;

        return gameData;
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

        public GameList()
        {
            games = new Game[0];
        }
    }

}
