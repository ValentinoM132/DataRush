using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Scene scene;
    public GameObject mainMenu;
    public GameObject levelSelectMenu;
    public AudioClip menuClick;
    public GameObject menuClose;
    public AudioClip ThunderSound;
    public GameObject Camera;
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLevelSelect()
    {
        StartCoroutine(levelSelectCoroutine());
    }

    public IEnumerator levelSelectCoroutine()
    {
        Camera.GetComponent<AudioSource>().volume = 0.5f;
        Camera.GetComponent<AudioSource>().PlayOneShot(ThunderSound);
        yield return new WaitForSeconds(1f);
        mainMenu.SetActive(false);
        levelSelectMenu.SetActive(true);

    }
    public void MenuClick()
    {
        Camera.GetComponent<AudioSource>().volume = .5f;
        Camera.GetComponent<AudioSource>().PlayOneShot(menuClick);
    }
    public void BackButton()
    {
        Camera.GetComponent<AudioSource>().volume = .5f;
        Camera.GetComponent<AudioSource>().PlayOneShot(menuClick);
        mainMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
    }
}
