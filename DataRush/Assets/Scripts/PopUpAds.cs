using UnityEngine;
using UnityEngine.UI; 
public class PopUpAds : MonoBehaviour
{
    [Header("Da Arrays")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private Sprite[] Sprites;
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            AddPopUp();
        }
    }
    private void AddPopUp()
    {
        if (Panels == null || Sprites == null) return;
        int randomIndex = Random.Range(0, 5);
        GameObject selectedObject = Panels[randomIndex];
        if (selectedObject != null)
        {
            selectedObject.SetActive(true);
            Image uiImage = selectedObject.GetComponent<Image>();
            if (uiImage != null && Sprites != null && randomIndex < Sprites.Length)
            {
                if (Sprites[randomIndex] != null)
                {
                    uiImage.sprite = Sprites[randomIndex];
                }
            }
            
        }
    }
}
