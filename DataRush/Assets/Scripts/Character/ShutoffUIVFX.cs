using System.Collections;
using UnityEngine;

public class ShutoffUIVFX : MonoBehaviour



{
    private bool isCountingDown = false;
    [SerializeField] public GameObject UIElementsToDisable;
    [SerializeField] public GameObject[] vfxToDisable;
    
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.CompareTag("Player") && !isCountingDown)
            {
                UIElementsToDisable.SetActive(false);
                foreach (GameObject vfx in vfxToDisable)
                {
                    vfx.SetActive(false);
                }
                StartCoroutine(StartCountdown(5f));
            }
        }
    }
    private IEnumerator StartCountdown(float duration)
    {
        isCountingDown = true;
        float remainingTime = duration;

        while (remainingTime > 0f)
        {
            Debug.Log($"Countdown: {remainingTime:F1}s"); // Replace with UI text update if needed
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        isCountingDown = false;
        vfxUiInactive();
    }
    private void vfxUiInactive()
    {
        UIElementsToDisable.SetActive(true);
        foreach (GameObject vfx in vfxToDisable)
        {
            vfx.SetActive(true);
        }
    }

}
