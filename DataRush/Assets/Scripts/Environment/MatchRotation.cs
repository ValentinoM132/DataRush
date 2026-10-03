using Unity.VisualScripting;
using UnityEngine;

public class MatchRotation : MonoBehaviour
{
    public GameObject targetTransform; // The transform to match rotation with
    public Vector3 realRotation;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
            realRotation = targetTransform.gameObject.transform.rotation.eulerAngles;
            realRotation.x = 0f; // Lock the X rotation to 0
            realRotation.z = 0f; // Lock the Z rotation to 0
            realRotation.y = realRotation.y - 90f; // Keep the Y rotation as is
            gameObject.transform.rotation = Quaternion.Euler(realRotation);
       
        
    }
}
