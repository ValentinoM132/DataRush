using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

public class TutorialPauseTime : MonoBehaviour
{
    private void OnEnable()
    {
        Time.timeScale = 0.0f;
        EnhancedTouchSupport.Enable(); // Must be enabled for Touch.activeTouches to work
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    void Update()
    {
        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
        {
            UnityEngine.InputSystem.EnhancedTouch.Touch mainTouch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0];

            if (mainTouch.phase == UnityEngine.InputSystem.TouchPhase.Began)
            {
                Time.timeScale = 1f;
                gameObject.SetActive(false);
            }
        }


    }
}
