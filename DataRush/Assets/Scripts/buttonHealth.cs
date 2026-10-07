using UnityEngine;
using UnityEngine.UI;
public class buttonHealth : MonoBehaviour
{
    [SerializeField] private Button mybutton;
    [SerializeField] private GameObject myPanel;
    public int currentclickcount = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mybutton.onClick.AddListener(onClicked);
    }

    // Update is called once per frame
    void onClicked()
    {
        currentclickcount--;

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
