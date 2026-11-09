using System;
using System.Collections.Generic;
using UnityEngine;



public class SoundManager : MonoBehaviour
{ 
    [SerializeField] private Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();
    public AudioSource audioSource;
    public AudioClip[] CoinSounds;
    public AudioClip[] PlayerDamageSounds;
    public AudioClip[] TrickFail;
    public AudioClip[] ShootSounds;
    public AudioClip[] TrickWin; // Array of arrays to hold different sound categories
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
        if(soundName == "CoinPickup")
        {
            AudioClip coinSound = CoinSounds[UnityEngine.Random.Range(0, CoinSounds.Length)];
            audioSource.volume = volume;
            audioSource.PlayOneShot(coinSound);
            return;
        }
        else if (soundName == "CharacterDamage")
        {
            AudioClip damageSound = PlayerDamageSounds[UnityEngine.Random.Range(0, PlayerDamageSounds.Length)];
            audioSource.volume = volume;
            audioSource.PlayOneShot(damageSound);
            return;
        }
        else if (soundName == "TrickFailure")
        {
            AudioClip failSound = TrickFail[UnityEngine.Random.Range(0, TrickFail.Length)];
            audioSource.volume = volume;
            audioSource.PlayOneShot(failSound);
            return;
        }
        else if (soundName == "PlayerShoot")
        {
            AudioClip shootSound = ShootSounds[UnityEngine.Random.Range(0, ShootSounds.Length)];
            audioSource.volume = volume;
            audioSource.PlayOneShot(shootSound);
            return;
        }
        else if (soundName == "TrickSuccess")
        {
            AudioClip winSound = TrickWin[UnityEngine.Random.Range(0, TrickWin.Length)];
            audioSource.volume = volume;
            audioSource.PlayOneShot(winSound);
            return;
        }
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
