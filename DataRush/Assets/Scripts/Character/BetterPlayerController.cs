using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class BetterPlayerController : MonoBehaviour
{
    

    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float rayDistance = 10f;
    [SerializeField] private float centeringSpeed = 5f;

    void Update()
    {
        // Cast rays to the left and right relative to the character's facing direction
        bool hitLeft = Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, rayDistance, wallLayer);
        bool hitRight = Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, rayDistance, wallLayer);

        if (hitLeft && hitRight)
        {
            // Calculate the exact midpoint between the two hit points
            Vector3 midpoint = (leftHit.point + rightHit.point) / 2f;
            Vector3 targetPosition = new Vector3(midpoint.x, transform.position.y, midpoint.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, centeringSpeed * Time.deltaTime);
        }
    }
}

