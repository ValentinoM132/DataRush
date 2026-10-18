using UnityEngine;

public class BlipCoin : MonoBehaviour
{
    public float audioVolume = .75f;
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Obstacle collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(100);
            SoundManager.Instance.PlaySound("CoinPickup", audioVolume);
            Destroy(gameObject);
            
        }
    }
}
