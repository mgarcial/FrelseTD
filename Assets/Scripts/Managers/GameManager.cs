using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Events
{
    EngineerEvent,
    StrikeEvent,
    ClimateEvent,
    TerroristEvent,
    SurvivorsEvent
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
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

        _enemyManager = EnemyManager.instance;
    }

    [SerializeField] private int maxHealth;
    [SerializeField] private int initialMoney;
    private int health;
    public int circuitos;

    public TMP_Text circuitosDisplay;
    public GameObject grid;
    public CustomCursor CC;
    public Tile[] tiles;
    public GameObject eventPanel;

    private Building bAColocar;
    private EnemyManager _enemyManager;
    private int goldRate = 0;

    private float _gameSpeed;

    public int Circuitos
    {
        get { return goldRate; }
        set { goldRate += value; }
    }

    private void OnEnable()
    {
        _enemyManager.OnEnemyKilledEvent += AddToMoney;
    }

    private void OnDisable()
    {
        _enemyManager.OnEnemyKilledEvent -= AddToMoney;
    }

    private void Start()
    {
        _gameSpeed = 1f;
        health = maxHealth;
        Debug.Log($"base has {health} health points left");
        circuitos = initialMoney;

        EventManager.instance.OnTimeChange += SetGameTime;
    }

    void Update()
    {
        circuitosDisplay.text = "Recursos: " + circuitos.ToString();

        if (Input.GetMouseButtonDown(0) && bAColocar != null)
        {
            Tile nearestTile = null;
            float shortestDistance = 100;
            foreach (Tile tile in tiles)
            {
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    nearestTile = tile;
                }
            }

            if (!nearestTile.isOccupied)
            {
                Instantiate(bAColocar, nearestTile.transform.position, Quaternion.identity);
                nearestTile.isOccupied = true;
                grid.SetActive(false);
                CC.gameObject.SetActive(false);
                Cursor.visible = true;
                bAColocar = null;
            }
        }
    }

    public void BuyBuilding(MonoBehaviour building)
    {
        // Check if the object passed is a Building or BuffTower
        if (building is Building)
        {
            Building b = (Building)building; // Cast the MonoBehaviour to Building
            if (circuitos >= b.cost)
            {
                Debug.Log("Button pressed");
                GameObject Edificio = b.gameObject;
                CC.gameObject.SetActive(true);
                CC.setCursor(Edificio.GetComponent<SpriteRenderer>());
                Cursor.visible = false;
                circuitos -= b.cost;
                grid.SetActive(true);
                bAColocar = b;
            }
        }
        else if (building is BuffTower)
        {
            BuffTower buffTower = (BuffTower)building; // Cast the MonoBehaviour to BuffTower
            if (circuitos >= buffTower.cost)
            {
                Debug.Log("Button pressed for BuffTower");
                GameObject Edificio = buffTower.gameObject;
                CC.gameObject.SetActive(true);
                CC.setCursor(Edificio.GetComponent<SpriteRenderer>());
                Cursor.visible = false;
                circuitos -= buffTower.cost;
                grid.SetActive(true);
                bAColocar = buffTower;
            }
        }
        else
        {
            Debug.LogError("Invalid building type: " + building.GetType().ToString());
        }
    }

    public void SetGameTime(TimeStates timeState)
    {
        switch (timeState)
        {
            case TimeStates.pause:
                Time.timeScale = 0;
                break;
            case TimeStates.unpause:
                Time.timeScale = 1;
                break;
        }
    }

    public void RestartLevel()
    {
        FindObjectOfType<LevelManager>().LoadCurrentScene();
        CleanLevel();
        Destroy(gameObject);
    }


    private void CleanLevel()
    {
        health = maxHealth;
        circuitos = initialMoney;
        EnemyManager.instance.ClearEnemiesList();
        _gameSpeed = 1f;
        SetGameSpeed(_gameSpeed);
        Debug.Log("LevelCleaned");
    }
    public void NextLevel()
    {
        CleanLevel();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }

    public void WinLevel()
    {
        _gameSpeed = 0;
        SetGameSpeed(_gameSpeed);
        UnlockNextLevel();
    }

    private void UnlockNextLevel()
    {
        int currentLvl = Preferences.GetCurrentLvl();
        Preferences.SetMaxLvl(currentLvl + 1);
    }

    public void ExitToMainMenu()
    {
        CleanLevel();
        FindObjectOfType<LevelManager>().LoadMainMenuScene();
        Destroy(gameObject);
    }
    public void SetGameSpeed(float speed)
    {
        _gameSpeed = speed;
        Time.timeScale = speed;
    }

    public void AddToMoney(int amount)
    {
        circuitos += amount;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Debug.Log($"the base has taken {dmg} damage, and has {health} hit points left");
        if (health <= 0)
        {
            health = 0;
            LooseLevel();
            Debug.Log("level lost");
        }
    }

    private void LooseLevel()
    {
        SceneManager.LoadScene("Main screen");
    }

    public int GetCurrentMoney() => circuitos;
}
