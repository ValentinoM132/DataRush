using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.UI;

public class JoystickAdjustments : MonoBehaviour
{
    public Canvas canvas;
    public void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                UpdateJoystickPosition(touch);
            }
        }


        
    }

    public void UpdateJoystickPosition(Touch touch)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            touch.position,
            canvas.worldCamera,
            out Vector2 localPoint);
        Debug.Log($"Touch position: {touch.position}, Local point: {localPoint}");
        // Assuming the joystick is a child of the canvas
        RectTransform joystickRect = GetComponent<RectTransform>();
        joystickRect.localPosition = localPoint;
    }

}