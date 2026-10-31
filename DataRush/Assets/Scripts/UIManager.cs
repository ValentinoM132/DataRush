using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Scene scene;
    public GameObject mainMenu;
    public GameObject levelSelectMenu;
    public AudioClip menuClick;
    public GameObject menuClose;
    public AudioClip ThunderSound;
    public GameObject Camera;

    [Header("HighScores")]
    public TextMeshProUGUI[] highScoreDisplays;
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLevelSelect()
    {
        StartCoroutine(levelSelectCoroutine());
        for (int i = 0; i < highScoreDisplays.Length; i++) // If a high score has been set in a level, change the text underneath the button to show it. Otherwise leave it as "INCOMPLETE"
        {
            string keyName = "Level" + (i + 1).ToString(); // Keys for main levels should be Level1, Level2, etc.
            if (PlayerPrefs.HasKey(keyName))
            {
                highScoreDisplays[i].text = "High Score: " + PlayerPrefs.GetInt(keyName).ToString();
                highScoreDisplays[i].color = Color.gold;
            }
        }

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
