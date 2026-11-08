using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    public GameObject pauseMenuUI; // Reference to the pause menu UI GameObject
    public GameObject GameScreen;
    public GameObject nextLevelButton;

    [Tooltip("Set to current level. Leave at 0 if not one of the main 5 levels"), Range(0,5)] 
    public int level = 0;
    string nextLevelName = "Level";

    private void Awake()
    {
        // Checks the current level #. If not one of the main 5 levels, do not show the next level button
        if (level > 0 && level < 6)
        {
            nextLevelName += (level+1).ToString();
        }
        else
        {
            nextLevelButton.SetActive(false);
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f; // Pause the game
        SoundManager.Instance.PlaySound("MenuClick", 1f);
        pauseMenuUI.SetActive(true);
        GameScreen.SetActive(false);
    }

    public void resumeGame()
    {
        Time.timeScale = 1f; // Resume the game
        SoundManager.Instance.PlaySound("MenuClick", 1f);
        pauseMenuUI.SetActive(false);
        GameScreen.SetActive(true);
    }

    public void MainMenu(string SceneName)
    {
        Time.timeScale = 1f; // Resume the game before going to the main menu
        SceneManager.LoadScene(SceneName); // Load the main menu scene
    }
   
    public void RestartGame()
    {         
        Time.timeScale = 1f; // Resume the game before restarting
        GameManager.Instance.CurrentScore = 0; // Reset the score
        GameManager.Instance.PlayerDead = false; // Reset the PlayerDead flag
        GameManager.Instance.Playerhurt = false; // Reset the Playerhurt flag
        GameManager.Instance.UpdateUI(); // Update UI
        GameManager.Instance.canGetHit = true; // Reset the canGetHit flag
        GameManager.Instance.playerMaterial.color = GameManager.Instance.playerColor; // Reset the player's material color
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextLevelName);
    }
}

