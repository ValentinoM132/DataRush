using System;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{ 
    [SerializeField] private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Kill the duplicate
            return;
        }

        Instance = this;
    }
    public void PlaySound(string soundName, float volume)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioClip clip))
        {
            if (clip != null)
            {
                audioSource.volume = volume;
                audioSource.PlayOneShot(clip);
            }
            else
            Debug.Log("SoundName Found But Clip wasn't");

        }
        else
        {
            Debug.LogWarning($"Sound '{soundName}' not found in the dictionary!");
        }
    }
}
