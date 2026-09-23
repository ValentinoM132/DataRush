using Unity.VisualScripting;
using UnityEngine;

public class TestDebuggingScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(gameObject.name + " Collider entered " + other.gameObject.name);

    }
}
