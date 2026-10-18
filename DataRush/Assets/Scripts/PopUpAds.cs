using UnityEngine;
using UnityEngine.UI; 
public class PopUpAds : MonoBehaviour
{
    [Header("Da Arrays")]
    [SerializeField] private GameObject[] Panels;
    [SerializeField] private Sprite[] Sprites;


    private void Start()
    {
        GameObject popUpPanel = GameObject.FindGameObjectWithTag("popUp");  
        for (int i = 0; i < popUpPanel.transform.childCount; i++)
        {
            Panels[i] = popUpPanel.transform.GetChild(i).gameObject;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            AddPopUp();
        }
    }
    private void AddPopUp()
    {
        int randomIndex1 = Random.Range(0, Panels.Length);
        int randomIndex2 = Random.Range(0, Sprites.Length);
       
        if (Panels == null || Sprites == null) return;
        
        GameObject selectedObject = Panels[randomIndex1];
        if (selectedObject != null && !selectedObject.activeInHierarchy)
        {
            selectedObject.SetActive(true);
            SoundManager.Instance.PlaySound("PopUpAd", 1f);
            Image uiImage = selectedObject.GetComponent<Image>();
            if (uiImage != null && Sprites != null && randomIndex2 < Sprites.Length)
            {
                if (Sprites[randomIndex2] != null)
                {
                    uiImage.sprite = Sprites[randomIndex2];
                }
            }

        }

        else if (!AreAllObjectsActive())
        {
            AddPopUp();
        }
    }

    bool AreAllObjectsActive()
    {
        for (int i = 0; i < Panels.Length; i++)
        {
            // If any item is missing or inactive, immediately fail the check
            if (Panels[i] == null || !Panels[i].activeInHierarchy)
            {
                return false;
            }
        }
        return true;
    }
}
