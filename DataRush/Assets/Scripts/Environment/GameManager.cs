using Unity.VectorGraphics;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentScore { get; set; } = 0;
    public int PlayerHealth { get; set; } = 5;
    public Material playerMaterial; // Reference to the player's material
    public Color playerColor;
    public bool canGetHit = true;
    public TMPro.TextMeshProUGUI scoreText; // Reference to the UI Text component
    public TMPro.TextMeshProUGUI healthText; // Reference to the UI Text component
    public GameObject gameOverPanel; // Reference to the Game Over UI panel

    private int scoreMultiplier = 1;
    private bool multPowerupActive = false;
    private float multPowerupDuration = 0f;
    public float baseMultPowerupDuration = 15f;

    public bool invulnPowerupActive = false;
    private float invulnPowerupDuration = 0f;
    public float baseInvulnPowerupDuration = 10f;

    private bool blasterPowerupActive = false;
    private float blasterPowerupDuration = 0f;
    public float baseBlasterPowerupDuration = 12f;
    private PlayerBlaster blaster;


    private void Awake()
    {
        // 2. Enforce the single-instance constraint
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Enforce only one instance exists
            return;
        }
        playerMaterial.color = playerColor; // Set the player's material color to the specified color
        Instance = this;
        UpdateUI();

        blaster = Component.FindAnyObjectByType<PlayerBlaster>();
    }

    private void Update()
    {
        if (PlayerHealth <= 0)
        {
            Time.timeScale = 0; // Pause the game
            gameOverPanel.SetActive(true); // Show the Game Over UI panel
            SoundManager.Instance.PlaySound("GameOver", .15f);
            Debug.Log("Game Over!");
        }
        if (multPowerupActive) // mult powerup timer
        {
            multPowerupDuration -= Time.deltaTime;
            if (multPowerupDuration <= 0)
            {
                multPowerupActive = false;
                scoreMultiplier = 1;
            }
        }
        if (invulnPowerupActive) // invulnerability powerup timer
        {
            invulnPowerupDuration -= Time.deltaTime;
            if (invulnPowerupDuration <= 0)
            {
                invulnPowerupActive = false;
                canGetHit = true;
            }
        }
        if (blasterPowerupActive) // blaster powerup timer
        {
            blasterPowerupDuration -= Time.deltaTime;
            if (blasterPowerupDuration <= 0)
            {
                blasterPowerupActive = false;
                if (blaster != null)
                {
                    blaster.DeactivateBlaster();
                }
            }
        }
    }

    public void AddScore(int points)
    {
        int modifiedScore = points * scoreMultiplier;
        if (modifiedScore >= 1)
            TextSpawner.Instance.spawnText("+" + modifiedScore, Color.green);
        else if (modifiedScore < 0)
        {
            modifiedScore = points; // Don't want the score multiplier to affect negative points, that's a bit harsh
            TextSpawner.Instance.spawnText(modifiedScore.ToString(), Color.red);
        }

        CurrentScore += modifiedScore;
        UpdateUI();
    }

    public void UpdateUI() // Removed this from update to prevent needlessly calling it every frame, instead only called when values are changed.
    {
        scoreText.text = CurrentScore.ToString();

        healthText.text = "Health: " + PlayerHealth.ToString();
    }

    public void ReduceHealth(int damage)
    {
        if (canGetHit)
        {
            SoundManager.Instance.PlaySound("CharacterDamage", 1f);
            PlayerHealth -= damage;
            UpdateUI();
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
    // Call when a multiplier powerup is picked up
    public void MultiplierPowerup()
    {
        scoreMultiplier *= 2; // 2x Mult powerups can stack
        multPowerupDuration = baseMultPowerupDuration;
        multPowerupActive = true;
    }
    // Call when Invulnerability powerup is picked up (or maybe a debug function if needed for testing)
    public void InvulnerabilityPowerup()
    {
        canGetHit = false;
        invulnPowerupDuration = baseInvulnPowerupDuration;
        invulnPowerupActive = true;
    }
    // Call when player pickes up Blaster powerup
    public void BlasterPowerup()
    {
        blasterPowerupActive = true;
        blasterPowerupDuration = baseBlasterPowerupDuration;

        if (blaster != null)
        {
            blaster.ActivateBlaster();
        }
    }

    public void CancelBlaster() // used to cancel this powerup, mostly for the ramps so you don't just shoot wildly mid jump
    {
        if (blasterPowerupActive && blaster != null)
        {
            blaster.DeactivateBlaster();
            blasterPowerupActive = false;
        }
    }
}
