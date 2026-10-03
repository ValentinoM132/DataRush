using UnityEngine;

public class InGameUI : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI GameObject
    public GameObject GameScreen;
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        pauseMenuUI.SetActive(true);
        GameScreen.SetActive(false);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        pauseMenuUI.SetActive(false);
        GameScreen.SetActive(true);
    }

    public void MainMenu(string SceneName)
    {
        Time.timeScale = 1f; // Resume the game before going to the main menu
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName); // Load the main menu scene
    }
   
    public void RestartGame()
    {         
        Time.timeScale = 1f; // Resume the game before restarting
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name); // Reload the current scene
    }
}
