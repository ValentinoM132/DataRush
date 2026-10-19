using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UIElements;

public class TrickingMinigame : MonoBehaviour
{
    public GameObject hitTarget;
    public GameObject Needle;
    public GameObject player;
    public float speed = 0.75f;
    public float trickSuccessVolume = 1f;
    public float trickFailureVolume = 1f;
    private Touchscreen touchScreen;
    private bool Overlapping;
    private float comboCounter;
    public GameObject comboTransform;
    public GameObject comboPrefab;
    public Sprite[] comboImage = new Sprite[3];
    void Start()
    {
        hitTarget.transform.localPosition = new Vector2(Random.Range(-320, 320), hitTarget.transform.localPosition.y);
        
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable(); // Must be enabled for Touch.activeTouches to work
        comboCounter = 0;
        speed = 0.75f;
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
                // This is the clean, unbuggy equivalent to wasPressedThisFrame
                Debug.Log("Touch pressed safely!");

                
                if (IsOverlapping(hitTarget.GetComponent<RectTransform>(), Needle.GetComponent<RectTransform>()))
                {
                    GameObject Combo = Instantiate(comboPrefab, comboTransform.transform);
                    Combo.gameObject.transform.SetParent(comboTransform.transform, false);
                    if (comboCounter < 4)
                    {
                        Combo.GetComponent<ImageFloatUp>().sprite = comboImage[1];
                    }
                    else if (comboCounter >= 4)
                    {
                        Combo.GetComponent<ImageFloatUp>().sprite = comboImage[2];
                    }
                        comboCounter++;
                    speed = speed + .15f;
                    GameManager.Instance.AddScore(500);
                    SoundManager.Instance.PlaySound("TrickSuccess", trickSuccessVolume);
                    player.transform.rotation = Random.rotation;
                }
                else
                {
                    
                    comboCounter = 0;
                    GameObject Combo = Instantiate(comboPrefab, comboTransform.transform);
                    Combo.gameObject.transform.SetParent(comboTransform.transform, false);
                    Combo.GetComponent<ImageFloatUp>().sprite = comboImage[0];
                    
                    speed = 1f;
                    SoundManager.Instance.PlaySound("TrickFailure", trickFailureVolume);
                    GameManager.Instance.AddScore(-500);
                }
                hitTarget.transform.localPosition = new Vector2(Random.Range(-320, 320), hitTarget.transform.localPosition.y);
            }
        }
            // Calculates a repeating value between 0 and 1
            float positionPercentage = Mathf.PingPong(speed * Time.time, .9f);
            RectTransform rectTransform = GetComponent<RectTransform>();
            // Moves the UI element horizontally across its anchor range
            rectTransform.anchorMin = new Vector2(positionPercentage, rectTransform.anchorMin.y);
            rectTransform.anchorMax = new Vector2(positionPercentage, rectTransform.anchorMax.y);

        

    }

    private bool IsOverlapping(RectTransform a, RectTransform b)
    {
        // Convert RectTransform boundaries to screen-space Rects
        Rect rectA = GetScreenRect(a);
        Rect rectB = GetScreenRect(b);

        return rectA.Overlaps(rectB);

    }

    private Rect GetScreenRect(RectTransform rectTransform)
    {
        Vector3[] corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);

        // corners[0] is bottom-left, corners[2] is top-right
        float width = corners[2].x - corners[0].x;
        float height = corners[2].y - corners[0].y;

        return new Rect(corners[0].x, corners[0].y, width, height);
    }
}

