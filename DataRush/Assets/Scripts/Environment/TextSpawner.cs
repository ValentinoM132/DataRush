using TMPro;
using UnityEngine;

public class TextSpawner : MonoBehaviour
{
    public GameObject Player;
    public RectTransform canvasRectTransform;
    public GameObject textToSpawnPrefab;
    public Camera camera;

    public static TextSpawner Instance { get; private set; }
    private void Start()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Kill the duplicate
            return;
        }

        Player = GameObject.FindGameObjectWithTag("Player");
        camera = GameObject.FindGameObjectWithTag("FollowCam").GetComponent<Camera>();

        Instance = this;
    }
    public void spawnText(string Text, Color color)
    {
        Vector3 screenPos = camera.WorldToScreenPoint(Player.transform.position);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRectTransform,
            screenPos,
            null, // Null camera for Screen Space - Overlay
            out Vector2 localPoint
        );
        GameObject newText = Instantiate(textToSpawnPrefab, new Vector2(localPoint.x, localPoint.y+500), Quaternion.identity);
        newText.transform.SetParent(transform, false);
        newText.GetComponent<TMP_Text>().text = Text;
        newText.GetComponent<TMP_Text>().color = color;
        
        

    }
}
