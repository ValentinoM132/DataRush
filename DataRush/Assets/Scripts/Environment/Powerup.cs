using UnityEngine;

public class Powerup : MonoBehaviour
{
    enum PowerupType { Random, X2Multiplier, Invulnerability, Blaster }

    [SerializeField] PowerupType type = PowerupType.Random;
    [SerializeField] Texture2D[] powerupTextures;
    [SerializeField] Renderer powerupIconRenderer;

    void Awake()
    {
        // If set to random, pick a powerup at random
        if (type == PowerupType.Random)
        {
            switch (Random.Range(0, powerupTextures.Length))
            {
                case 0:
                    type = PowerupType.X2Multiplier;
                    break;
                case 1:
                    type = PowerupType.Invulnerability;
                    break;
                case 2:
                    type = PowerupType.Blaster;
                    break;
            }
        }
        var particleRenderer = GetComponentInChildren<ParticleSystemRenderer>();


        // Set the Texture to match the corresponding PowerupType
        switch (type)
        {
            case PowerupType.X2Multiplier:
                powerupIconRenderer.material.SetTexture("_IconSprite", powerupTextures[0]);
                particleRenderer.material.SetTexture("_IconSprite", powerupTextures[0]);
                break;
            case PowerupType.Invulnerability:
                powerupIconRenderer.material.SetTexture("_IconSprite", powerupTextures[1]);
                particleRenderer.material.SetTexture("_IconSprite", powerupTextures[1]);
                break;
            case PowerupType.Blaster:
                powerupIconRenderer.material.SetTexture("_IconSprite", powerupTextures[2]);
                particleRenderer.material.SetTexture("_IconSprite", powerupTextures[2]);
                break;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Obstacle collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            switch (type)
            {
                case PowerupType.X2Multiplier:
                    GameManager.Instance.MultiplierPowerup();
                    break;
                case PowerupType.Invulnerability:
                    GameManager.Instance.InvulnerabilityPowerup();
                    break;
                case PowerupType.Blaster:
                    GameManager.Instance.BlasterPowerup();
                    break;
            }
            
            Destroy(gameObject);
        }
    }
}
