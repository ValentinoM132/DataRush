using UnityEngine;

public class FakeWall : MonoBehaviour
{
    [Tooltip("Put objects that you wish to have hidden by this wall in this array.")]
    [SerializeField] private GameObject[] objectsToHide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < objectsToHide.Length; i++)
        {
            objectsToHide[i].SetActive(false);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Obstacle collided with: " + other.gameObject.name);
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < objectsToHide.Length; i++)
            {
                objectsToHide[i].SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}