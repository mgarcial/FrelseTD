using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    //Wave atributes
    [SerializeField] private List<WaveSO> waves;
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private Transform endPoint;
    [SerializeField] private List<GameObject> nextWaveAnnouncers;

    [SerializeField] private int Event1Wave;
    [SerializeField] private int Event2Wave;
    [SerializeField] private int Event3Wave;

    private int waveCounter;
    private int strikeEventCounter = 0;
    private int strikeEventReward;
    private float circuitsMultiplier;
    private EventChoices strikeChoice;

    [SerializeField] private List<Enemy> enemies;

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

    private void Start()
    {
        enemies = new List<Enemy>();
        waveCounter = 0;
        circuitsMultiplier = 1;

        EventManager.instance.OnStrikeEvent += StrikeEvent;
    }

    private void OnDisable()
    {
        EventManager.instance.OnStrikeEvent -= StrikeEvent;
    }

    private void StrikeEvent(EventChoices choice, float rate, int counter, int reward)
    {
        switch (choice)
        {
            case EventChoices.accept:
                strikeChoice = choice;
                strikeEventCounter = counter;
                strikeEventReward = reward;
                break;
            case EventChoices.decline:
                strikeEventCounter = counter;
                circuitsMultiplier = rate;
                break;
        }
    }

    public void AddEnemy(Enemy enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(Enemy enemy)
    {
        enemies.Remove(enemy);
        EventManager.instance.EnemyKilled((int)Mathf.Round(enemy.GetCircuits() * circuitsMultiplier));
        if (waveCounter == waves.Count && enemies.Count == 0)
        {
            EventManager.instance.AllEnemiesDead();
        }
    }

    public List<Enemy> GetEnemiesList() => enemies;

    public int GetEnemyCount() => enemies.Count;
    public void ClearEnemiesList() => enemies.Clear();

    //Para probar
    public void StartWave()
    {
        if (waveCounter == (Event1Wave -1) || waveCounter == (Event2Wave -1) || waveCounter == (Event3Wave -1))
        {
            EventManager.instance.EventStart();
            EventManager.instance.TimeChange(TimeStates.pause);
        }

        WaveSO nextWave = waves[waveCounter];
        StartCoroutine(WaveAnnaouncerRoutine(nextWave.spawnPoint, nextWave));
    }
    IEnumerator WaveAnnaouncerRoutine(int spawnPoint, WaveSO nextWave)
    {
        nextWaveAnnouncers[spawnPoint].SetActive(true);

        yield return new WaitForSeconds(3f);

        nextWaveAnnouncers[spawnPoint].SetActive(false);
        StartCoroutine(StartWaveRoutine(1.0f, nextWave));
    }

    IEnumerator StartWaveRoutine(float seconds, WaveSO wave)
    {
        foreach (Enemy enemy in wave.enemiesOfWave)
        {
            enemy.endPoint = endPoint;
            Instantiate(enemy, spawnPoints[wave.spawnPoint].position, Quaternion.identity);
            //enemy.endPoint = endPoint;
            yield return new WaitForSeconds(seconds);
        }

        waveCounter++;
        strikeEventCounter--;

        if (strikeEventCounter == 0)
        {
            circuitsMultiplier = 1;

            if(strikeChoice == EventChoices.accept)
            {
                EventManager.instance.StrikeFinish(strikeEventReward);
            }
        }
    }
}
