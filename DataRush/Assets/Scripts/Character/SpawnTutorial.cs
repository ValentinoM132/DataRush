using UnityEngine;

public class SpawnTutorial : MonoBehaviour
{
    public GameObject Tutorial;

    private void OnTriggerEnter(Collider other)
    {
        Tutorial.SetActive(true);
    }
}
