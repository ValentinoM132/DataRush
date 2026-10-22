using UnityEngine;

public class Obstacle : MonoBehaviour
{
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
            
            Destroy(gameObject);
            Debug.Log("Damage Taken");
            //Add Damage Script Here
        }
        // Destroy upon getting shot by the player's blaster
        if (other.CompareTag("PlayerProjectile"))
        {
            GameManager.Instance.AddScore(100);
            Destroy(other);
            Destroy(gameObject);
        }
    }
}
