using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public bool isMusicMuted = false;
    public bool isSoundMuted = false;
    public GameObject camera;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeisSoundMuted(bool value)
    {
        isSoundMuted = !value;
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
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
    }
}
