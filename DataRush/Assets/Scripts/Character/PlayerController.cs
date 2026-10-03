using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
   
    public Transform targetTransform;
    private NavMeshAgent agent;
    [SerializeField] private GameObject EndingScreen;
    [SerializeField] private GameObject[] UITurnOff;

    void Start()
    {
       
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (targetTransform != null)
        {
            agent.SetDestination(targetTransform.position);
        }
        if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
        {
            EndingScreen.SetActive(true);
            foreach (GameObject uiElement in UITurnOff)
            {
                if (uiElement != null && uiElement.activeSelf)
                {
                    uiElement.SetActive(false);
                }
            }
        }
    }
}
