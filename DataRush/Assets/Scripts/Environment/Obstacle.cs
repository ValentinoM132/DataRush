using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] float audioVolume = 1f;
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Obstacle collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            if (!GameManager.Instance.IsInvulnerable())
            {
                GameManager.Instance.AddScore(-100);
                GameManager.Instance.ReduceHealth(1);
            }
            else
            {
                GameManager.Instance.AddScore(50);
            }
            SoundManager.Instance.PlaySound("ObstacleBoom", audioVolume);
            Destroy(gameObject);
            Debug.Log("Damage Taken");
            //Add Damage Script Here
        }
        // Destroy upon getting shot by the player's blaster
        if (other.CompareTag("PlayerProjectile"))
        {
            SoundManager.Instance.PlaySound("ObstacleBoom", audioVolume);
            GameManager.Instance.AddScore(50);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
