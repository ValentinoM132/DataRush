using UnityEngine;
using UnityEngine.UI;

// Plays a list of sprites over and over, like a flipbook.
// Works on a UI Image (menus) or a SpriteRenderer (objects in the game world).
//
// How to use:
// 1. Add this script to the object that shows the coin (Add Component > SpriteFlipbook).
// 2. Drag the 8 coin frames into "Frames", in order (frame 0 first).
// 3. Leave Frames Per Second at 10, or change it to spin faster or slower.
public class SpriteFlipbook : MonoBehaviour
{
    [Tooltip("The animation frames, in order.")]
    public Sprite[] frames;

    [Tooltip("How many frames to show each second. 10 = one full coin spin every 0.8 seconds.")]
    public float framesPerSecond = 10f;

    [Tooltip("Wait this many seconds before starting. Handy if you have several copies and don't want them in sync.")]
    public float startDelay = 0f;

    Image uiImage;
    SpriteRenderer spriteRenderer;
    float timer;

    void Awake()
    {
        uiImage = GetComponent<Image>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timer = -startDelay;
    }

    void Update()
    {
        if (frames == null || frames.Length == 0) return;

        // unscaledDeltaTime keeps it moving even if the game is paused (Time.timeScale = 0)
        timer += Time.unscaledDeltaTime;
        if (timer < 0f) return;

        int index = (int)(timer * framesPerSecond) % frames.Length;
        if (uiImage != null) uiImage.sprite = frames[index];
        else if (spriteRenderer != null) spriteRenderer.sprite = frames[index];
    }
}
