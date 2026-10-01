using UnityEngine;
using UnityEngine.InputSystem;

public class RealPlayerController : MonoBehaviour
{
        public Vector2 moveInput;
        public float moveSpeed = 5f;

        public void Start()
        {
            
        }
        // This method is called automatically if you use Player Input "Send Messages" behavior
        public void OnMove(InputValue value)
        {
            moveInput = value.Get<Vector2>();
        }

        void Update()
        {
        // Calculate 3D movement direction based on Vector2 joystick coordinates
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = moveInput.x * 60f; // Set only the Y axis to 45 degrees
        transform.localEulerAngles = currentRotation;
        
                                                                              //transform.Translate(movement, Space.World);
    }
    
}
