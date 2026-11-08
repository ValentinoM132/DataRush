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
    public AudioClip menuClose;
    public AudioClip ThunderSound;
    public GameObject Camera;

    [Header("HighScores")]
    public TextMeshProUGUI[] highScoreDisplays;
    [Header("LevelSelectButtons")]
    public Button[] levelButtons;

    private bool unlockAll = false;
    public void StartGame(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OpenLevelSelect()
    {
        bool completedPreviousLevel = true;
        //StartCoroutine(levelSelectCoroutine());
        for (int i = 0; i < highScoreDisplays.Length; i++) // If a high score has been set in a level, change the text underneath the button to show it. Otherwise leave it as "INCOMPLETE"
        {
            string keyName = "Level" + (i + 1).ToString(); // Keys for main levels should be Level1, Level2, etc.
            if (PlayerPrefs.HasKey(keyName))
            {
                highScoreDisplays[i].text = PlayerPrefs.GetInt(keyName).ToString(); //I got rid of the high score text cause it was too big
                highScoreDisplays[i].color = Color.gold;
                levelButtons[i].interactable = true;
            }
            else if (completedPreviousLevel || unlockAll)
            {
                levelButtons[i].interactable = true;
                completedPreviousLevel = false;
            }
            else
            {
                highScoreDisplays[i].enabled = false;
            }
        }

    }

    public IEnumerator ResetSaveCoroutine() // Plays the thunder noise, deletes all player prefs and reloads the title screen. CANNOT BE REVERSED USE WITH EXTREME CAUTION
    {
        Camera.GetComponent<AudioSource>().volume = 0.5f;
        Camera.GetComponent<AudioSource>().PlayOneShot(ThunderSound);
        yield return new WaitForSeconds(1f);
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
    public void MenuClick()
    {
        Camera.GetComponent<AudioSource>().volume = .5f;
        Camera.GetComponent<AudioSource>().PlayOneShot(menuClick);

    }

    public void MenuClose()
    {
        Camera.GetComponent<AudioSource>().volume = .75f;
        Camera.GetComponent<AudioSource>().PlayOneShot(menuClose);
    }
    /*
    public void BackButton()
    {
        Camera.GetComponent<AudioSource>().volume = .5f;
        Camera.GetComponent<AudioSource>().PlayOneShot(menuClose);
        mainMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
    }*/

    public void RESETALLSAVEDATA()
    {
        StartCoroutine(ResetSaveCoroutine());
    }

    public void UNLOCKALLLEVELS()
    {
        unlockAll = true;
    }
}
