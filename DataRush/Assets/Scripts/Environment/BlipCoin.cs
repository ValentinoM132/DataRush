using UnityEngine;

public class BlipCoin : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Obstacle collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(100);
            Destroy(gameObject);
            
        }
    }
}
