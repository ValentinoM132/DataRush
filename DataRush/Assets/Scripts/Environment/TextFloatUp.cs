using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextFloatUp : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    void Start()
    {
        scoreText.CrossFadeAlpha(0, 1f, true);
        Destroy(gameObject, 1f);
    }

    void Update()
    {
        transform.Translate(Vector3.up * 500f * Time.deltaTime);
    }
}
