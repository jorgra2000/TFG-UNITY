using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayButtons : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject exitPanel;

    public void ExitLevel()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OpenExitMenu()
    {
        exitPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitExitMenu()
    {
        exitPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;       
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void BackToSelect()
    {
        SceneManager.LoadScene("CircuitSelect");
        Time.timeScale = 1f;      
    }

}
