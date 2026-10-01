using UnityEngine;

public class QuitButtonUI: MonoBehaviour 
{
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
        //Just to make sure its working
    }
}
