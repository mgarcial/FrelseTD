using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    

    [Header("Enemies & Player Sounds")]
    [SerializeField] private AudioClip enemySpawnSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip playerDeathSound;
    [SerializeField] private AudioClip playerWinSound;

    [Header("Shop Sounds")]
    [SerializeField] private AudioClip towerPlacedSound;

    [Header("Button")]
    [SerializeField] private AudioClip buttonPressed;

    [Header("Towers shots")]
    [SerializeField] private AudioClip commonTowerShot;
    [SerializeField] private AudioClip energyTowerShot;
    [SerializeField] private AudioClip burnTowerShot;
    [SerializeField] private AudioClip CannonTowerShot;
    [SerializeField] private AudioClip stunTowerShot;

    private AudioSource audioSource;

    private static AudioManager instance;
    public static AudioManager GetInstance() => instance;

    private void Awake()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        audioSource = GetComponent<AudioSource>();

    }
    void Start()
    {
        audioSource.mute = !Preferences.GetToggleSfx();
    }

    public void PlayEnemySpawn() => audioSource.PlayOneShot(enemySpawnSound);
    public void PlayEnemyDeath() => audioSource.PlayOneShot(enemyDeathSound);
    public void PlayPlayerDeath() => audioSource.PlayOneShot(playerDeathSound);
    public void PlayPlayerWin() => audioSource.PlayOneShot(playerWinSound);
    public void PlayTowerPlaced() => audioSource.PlayOneShot(towerPlacedSound);
    public void PlayButtonPressed() => audioSource.PlayOneShot(buttonPressed);
    public void PlayCommonTowerShot() => audioSource.PlayOneShot(commonTowerShot);
    public void PlayEnergyTowerShot() => audioSource.PlayOneShot(energyTowerShot);
    public void PlayBurnTowerShot() => audioSource.PlayOneShot(burnTowerShot);
    public void PlayCannonTowerShot() => audioSource.PlayOneShot(CannonTowerShot);
    public void PlayStunTowerShot() => audioSource.PlayOneShot(stunTowerShot);


}
