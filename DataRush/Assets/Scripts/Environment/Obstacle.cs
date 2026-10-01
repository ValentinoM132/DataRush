using UnityEngine;

public class Obstacle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Obstacle collided with: " + other.gameObject.name);
    }
}
