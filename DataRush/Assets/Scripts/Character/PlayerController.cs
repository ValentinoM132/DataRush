using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
   
    public Transform targetTransform;
    private NavMeshAgent agent;

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
    }
}
