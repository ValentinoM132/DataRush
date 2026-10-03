using UnityEngine;
using UnityEngine.InputSystem;

public class RealPlayerController : MonoBehaviour
{
        public Vector2 moveInput;
        public float moveSpeed = 5f;
        public GameObject joystick;

        public void Start()
        {
            joystick.GetComponent<FloatingJoystick>();

        }
        // This method is called automatically if you use Player Input "Send Messages" behavior
        

        void Update()
        {
        moveInput = joystick.GetComponent<FloatingJoystick>().JoystickValue;
        Vector3 currentRotation = transform.localEulerAngles;
        currentRotation.z = moveInput.x * 60f; // Set only the Y axis to 45 degrees
        transform.localEulerAngles = currentRotation;
        
                                                                              //transform.Translate(movement, Space.World);
    }
    
}
