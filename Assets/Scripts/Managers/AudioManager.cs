using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioManager instance;

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

    private AudioSource _audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        _audioSource.mute = !Preferences.GetToggleSfx();
    }

    public void PlayEnemySpawn() => _audioSource.PlayOneShot(enemySpawnSound);
    public void PlayEnemyDeath() => _audioSource.PlayOneShot(enemyDeathSound);
    public void PlayPlayerDeath() => _audioSource.PlayOneShot(playerDeathSound);
    public void PlayPlayerWin() => _audioSource.PlayOneShot(playerWinSound);
    public void PlayTowerPlaced() => _audioSource.PlayOneShot(towerPlacedSound);
    public void PlayButtonPressed() => _audioSource.PlayOneShot(buttonPressed);
    public void PlayCommonTowerShot() => _audioSource.PlayOneShot(commonTowerShot);
    public void PlayEnergyTowerShot() => _audioSource.PlayOneShot(energyTowerShot);
    public void PlayBurnTowerShot() => _audioSource.PlayOneShot(burnTowerShot);
    public void PlayCannonTowerShot() => _audioSource.PlayOneShot(CannonTowerShot);
    public void PlayStunTowerShot() => _audioSource.PlayOneShot(stunTowerShot);


}
