using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Obstacle collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(-100);
            GameManager.Instance.reduceHealth(1);
            Destroy(gameObject);
            Debug.Log("Damage Taken");
            //Add Damage Script Here
        }
    }
}
