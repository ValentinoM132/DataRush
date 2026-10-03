using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentScore { get; private set; } = 0;
    public TMPro.TextMeshProUGUI scoreText; // Reference to the UI Text component

    private void Awake()
    {
        // 2. Enforce the single-instance constraint
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Enforce only one instance exists
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        UpdateUI();
    }

    public void AddScore(int points)
    {
        CurrentScore += points;
    }

    private void UpdateUI()
    {
        scoreText.text = "Score: " + CurrentScore.ToString();
    }
}
