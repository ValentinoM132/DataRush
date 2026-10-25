using UnityEngine;
using UnityEngine.UI;
public class buttonHealth : MonoBehaviour
{
    [SerializeField] private Button mybutton;
    [SerializeField] private GameObject myPanel;
    public Image AdImage;
    public Material targetMaterial;
    public int currentclickcount = 3;
    private static readonly int DissolveAmountID = Shader.PropertyToID("_Dissolve_Amount");
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mybutton.onClick.AddListener(onClicked);
      
        
        if (targetMaterial != null )
            targetMaterial = AdImage.material;
      
    }

    // Update is called once per frame
    void onClicked()
    {
        currentclickcount--;
        if (targetMaterial != null)
        targetMaterial.SetFloat(DissolveAmountID, currentclickcount/3);

        if (currentclickcount <= 0)
        { 
            getriddof();
            currentclickcount = 3;
            Debug.Log("Button clicked 3 times. Panel deactivated.");
        }
    }
    void getriddof()
    {
        myPanel.SetActive(false);
    }
}
