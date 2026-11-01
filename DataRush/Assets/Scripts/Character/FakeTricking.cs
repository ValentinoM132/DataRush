using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class FakeTricking : MonoBehaviour
{
  
    public float speed = 0.75f;
  
   

  


    void Update()
    {
      
            // Calculates a repeating value between 0 and 1
            float positionPercentage = Mathf.PingPong(speed * Time.unscaledTime, .9f);
            RectTransform rectTransform = GetComponent<RectTransform>();
            // Moves the UI element horizontally across its anchor range
            rectTransform.anchorMin = new Vector2(positionPercentage, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(positionPercentage, rectTransform.anchorMax.y);

    }

}

