using System.Collections;
using NUnit;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class FloatingJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform backgroundRect;
    private RectTransform handleRect;
    private Vector2 originalPosition;

    public float movementRange = 50f;
    public Vector2 JoystickValue;
        private bool PointerDown = false;
    void Start()
    {
        backgroundRect = GetComponent<RectTransform>();
        handleRect = transform.GetChild(0).GetComponent<RectTransform>();
        originalPosition = backgroundRect.anchoredPosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PointerDown = true;
        // Convert screen touch position to local canvas coordinates and snap joystick background there
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            backgroundRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPosition
        );

        handleRect.anchoredPosition = localPosition;
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            backgroundRect,
            eventData.position,
            eventData.pressEventCamera,
            out Vector2 localPosition
        );

        // Clamp the handle within the movement range radius
        localPosition = Vector2.ClampMagnitude(localPosition, movementRange);
        handleRect.anchoredPosition = localPosition;
        JoystickValue = localPosition / movementRange;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        PointerDown = false;
        // Reset handle and optionally snap background back to original position (or hide it)
        handleRect.anchoredPosition = Vector2.zero;
        backgroundRect.anchoredPosition = originalPosition;
        
        StartCoroutine(ResetJoystickValue(JoystickValue.x, 0f, JoystickValue.x));
        
    }

    public IEnumerator ResetJoystickValue(float start, float end, float totalTime)
    {
        if (totalTime <= 0f)
        {
            totalTime= totalTime * -1f;
        }
        yield return null;
        float elapsedTime = 0f;

        while (elapsedTime < totalTime)
        {
            if(PointerDown == true)
            {
                yield break; // Exit the coroutine if the joystick is pressed again
            }
            elapsedTime += Time.deltaTime; // Track how much time has passed

            // Calculate percentage (0 to 1)
            float t = elapsedTime / totalTime;

            // Interpolate the value
            float currentValue = Mathf.Lerp(start, end, t);

            JoystickValue = new Vector2(currentValue, JoystickValue.y); // Update only the x value

            yield return null; // Wait until the next frame
        }

        // Ensure it hits the exact end value when finished
        float finalValue = end;
    }
}