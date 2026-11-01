using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public bool isMusicMuted = false;
    public bool isSoundMuted = false;
    public GameObject camera;
    public GameObject musicMaker;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        musicMaker = GameObject.FindGameObjectWithTag("Music");
    }

    public void ChangeisSoundMuted(bool value)
    {
        isSoundMuted = !value;
    }

    public void ChangeisMusicMuted(bool value)
    {
        isMusicMuted = !value;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        musicMaker = GameObject.FindGameObjectWithTag("Music");
        SoundManager.Instance.audioSource.mute = isSoundMuted;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void Update()
    {
        if (isSoundMuted == true)
        {
            if (camera != null)
            {
                camera.GetComponent<AudioSource>().mute = isSoundMuted;
            }
        }
        else
        {
            if (camera != null)
            {
                camera.GetComponent<AudioSource>().mute = isSoundMuted;
            }
        }
        if (musicMaker != null && isMusicMuted)
        {
            musicMaker.GetComponent<AudioSource>().mute = isMusicMuted;
        }

        else if (musicMaker != null && !isMusicMuted)
        {
            musicMaker.GetComponent<AudioSource>().mute = isMusicMuted;
        }
    }
}
