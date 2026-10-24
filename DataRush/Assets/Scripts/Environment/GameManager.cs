using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int CurrentScore { get; set; } = 0;
    public int PlayerHealth { get; set; } = 5;
    [Header("Player Settings")]
    public Material playerMaterial; // Reference to the player's material
    public Color playerColor;
    public bool canGetHit = true;
    [Header("UI Connections")]
    public TMPro.TextMeshProUGUI scoreText; // Reference to the UI Text component
    public TMPro.TextMeshProUGUI healthText; // Reference to the UI Text component
    public GameObject gameOverPanel; // Reference to the Game Over UI panel

    [Header("Powerup Settings")]
    private int scoreMultiplier = 1;
    private bool multPowerupActive = false;
    private float multPowerupDuration = 0f;
    [SerializeField, Range(0.5f, 30f)] public float baseMultPowerupDuration = 15f;

    private bool invulnPowerupActive = false;
    private float invulnPowerupDuration = 0f;
    [SerializeField, Range(0.5f, 30f)] public float baseInvulnPowerupDuration = 10f;

    private bool blasterPowerupActive = false;
    private float blasterPowerupDuration = 0f;
    [SerializeField, Range(0.5f, 30f)] public float baseBlasterPowerupDuration = 12f;
    private PlayerBlaster blaster;

    [Header("Powerup UI Connections")]
    [SerializeField] public GameObject[] durationBars; // 0 = Mult, 1 = Blaster, 2 = Invuln
    [SerializeField] public Image[] durationBarFills;



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

        blaster = Component.FindAnyObjectByType<PlayerBlaster>(); // Get the playerblaster in the scene
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
            durationBarFills[0].fillAmount = multPowerupDuration / baseMultPowerupDuration;
            if (multPowerupDuration <= 0)
            {
                multPowerupActive = false;
                scoreMultiplier = 1;
                durationBars[0].SetActive(false);
            }
        }
        if (invulnPowerupActive) // invulnerability powerup timer
        {
            invulnPowerupDuration -= Time.deltaTime;
            durationBarFills[2].fillAmount = invulnPowerupDuration / baseInvulnPowerupDuration;
            if (invulnPowerupDuration <= 0)
            {
                invulnPowerupActive = false;
                canGetHit = true;
                durationBars[2].SetActive(false);
            }
        }
        if (blasterPowerupActive) // blaster powerup timer
        {
            blasterPowerupDuration -= Time.deltaTime;
            durationBarFills[1].fillAmount = blasterPowerupDuration / baseBlasterPowerupDuration;
            if (blasterPowerupDuration <= 0)
            {
                blasterPowerupActive = false;
                if (blaster != null)
                {
                    blaster.DeactivateBlaster();
                }
                durationBars[1].SetActive(false);
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
        durationBarFills[0].fillAmount = multPowerupDuration;
        durationBars[0].SetActive(true);
    }
    // Call when Invulnerability powerup is picked up (or maybe a debug function if needed for testing)
    public void InvulnerabilityPowerup()
    {
        canGetHit = false;
        invulnPowerupDuration = baseInvulnPowerupDuration;
        invulnPowerupActive = true;
        durationBarFills[2].fillAmount = invulnPowerupDuration;
        durationBars[2].SetActive(true);
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
        durationBarFills[2].fillAmount = blasterPowerupDuration;
        durationBars[1].SetActive(true);
    }

    public void PauseBlaster() // used to pause this powerup, mostly for the ramps so you don't just shoot wildly mid jump
    {
        if (blasterPowerupActive && blaster != null && durationBars[1] != null)
        {
            blaster.DeactivateBlaster();
            blasterPowerupActive = false;
            durationBars[1].SetActive(false);
        }
    }

    public void UnpauseBlaster() // unpauses the blaster if any duration was remaining on the powerup
    {
        if (blasterPowerupDuration > 0 && blaster != null)
        {
            blaster.ActivateBlaster();
            blasterPowerupActive = true;
            durationBars[1].SetActive(true);
        }
    }

    public void PauseInvuln()
    {
        invulnPowerupActive = false;
        if (durationBars[2] != null)
        {
            durationBars[2].SetActive(false);
        }
    }

    public void UnpauseInvuln()
    {
        if (invulnPowerupDuration > 0 && durationBars[2] != null)
        {
            invulnPowerupActive = true;
            durationBars[2].SetActive(true);
        }
    }

    public bool IsInvulnerable()
    {
        return invulnPowerupActive;
    }
}
