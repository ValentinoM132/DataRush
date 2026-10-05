using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentScore { get;  set; } = 0;
    public int PlayerHealth { get;  set; } = 5;
    public Material playerMaterial; // Reference to the player's material
    public Color playerColor;
    public bool canGetHit = true;
    public TMPro.TextMeshProUGUI scoreText; // Reference to the UI Text component
    public TMPro.TextMeshProUGUI healthText; // Reference to the UI Text component
    public GameObject gameOverPanel; // Reference to the Game Over UI panel

    private void Awake()
    {
        // 2. Enforce the single-instance constraint
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Enforce only one instance exists
            return;
        }
        playerColor= playerMaterial.color; // Set the player's material color to the specified color
        Instance = this;
    }

    private void Update()
    {
        UpdateUI();
        if(PlayerHealth <= 0)
        {
            Time.timeScale = 0; // Pause the game
            gameOverPanel.SetActive(true); // Show the Game Over UI panel
            Debug.Log("Game Over!");
        }
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + CurrentScore.ToString();
        healthText.text = "Health: " + PlayerHealth.ToString();
    }

    public void reduceHealth(int damage)
    {
        if (canGetHit)
        {
            PlayerHealth -= damage;
            canGetHit = false;
            playerMaterial.color = Color.red; // Change the player's material color to red
            Invoke("ResetCanGetHit", 1f); // Adjust the delay as needed

        }
    }

    private void ResetCanGetHit()
    {
        canGetHit = true;
        playerMaterial.color = playerColor; // Reset the player's material color to the specified color
    }
}
