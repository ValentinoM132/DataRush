using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BetterPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float rightDistance;
    private float leftDistance;
    private Vector3 rightDistanceVector;
    private Vector3 leftDistanceVector;
    private Vector3 movementDirection;
    public Transform raycastShootOut;
    public float turnSpeed = 20f;

    public LayerMask layerMask;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit leftHitData;
        RaycastHit rightHitData;

        
        if (Physics.Raycast(raycastShootOut.position, -transform.right * 10f, out leftHitData, 10f, layerMask))
        {
            leftDistance = leftHitData.distance;

        }

        if (Physics.Raycast(raycastShootOut.position, transform.right * 10f, out rightHitData, 10f, layerMask))
        {
            rightDistance = rightHitData.distance;

        }

        if (rightDistance > leftDistance)
        {
            rightDistanceVector = Vector3.right * turnSpeed * Time.deltaTime;

        }
        else
        {
            rightDistanceVector = Vector3.zero;
        }

        if (leftDistance > rightDistance)
        {
            leftDistanceVector = Vector3.left * turnSpeed * Time.deltaTime;
        }
        else
        {
            leftDistanceVector = Vector3.zero;
        }

       

        // Calculate target rotation based on movement vector

        movementDirection = rightDistanceVector + leftDistanceVector;
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        Debug.DrawRay(raycastShootOut.position, -transform.right * 10f, Color.red);
        Debug.DrawRay(raycastShootOut.position, transform.right * 10f, Color.green);

       
    }
}
