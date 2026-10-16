using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Scene scene;
    public GameObject mainMenu;
    public GameObject levelSelectMenu;
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLevelSelect()
    {
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }

    public void BackButton()
    {        
        mainMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
    }
}
