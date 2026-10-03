using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonTinoTest : MonoBehaviour
{
    public Scene scene;
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
