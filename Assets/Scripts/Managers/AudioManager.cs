using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Enemies Sounds")]
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip enemyDeathSound;
    [SerializeField] private AudioClip playerDeathSound;

    [Header("Shop Sounds")]
    [SerializeField] private AudioClip towerPlacedSound;
    [SerializeField] private AudioClip towerSelectedSound;

    private AudioSource _audioSource;

    void Start()
    {
        
    }

    public void PlayEnemyHit() => _audioSource.PlayOneShot(enemyHitSound);

}
