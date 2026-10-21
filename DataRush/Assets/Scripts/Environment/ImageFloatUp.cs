using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ImageFloatUp : MonoBehaviour
{
    [SerializeField] public Image image;
    public Sprite sprite;
    void Start()
    {
        image.SetNativeSize();
        image.sprite = sprite;
        image.CrossFadeAlpha(0, .75f, true);
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * 150f * Time.deltaTime);
    }
}
