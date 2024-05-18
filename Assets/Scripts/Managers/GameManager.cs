using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
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
    private int circuits;
    public Text circuitosDisplay;
    public GameObject grid;
    public CustomCursor CC;
    public Tile[] tiles;
    public GameObject eventPanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public HealthbarBehavior healthBar;

    private Building buildingToPlace;
    private List<Tile> occupiedTiles = new List<Tile>(); 
    private EnemyManager _enemyManager;

    private int goldRate = 0;

    private float _gameSpeed;

    private void OnEnable()
    {
        _enemyManager.OnEnemyKilledEvent += AddToMoney;
        _enemyManager.OnAllEnemiesDeadEvent += WinLevel;
    }

    private void OnDisable()
    {
        _enemyManager.OnEnemyKilledEvent -= AddToMoney;
        _enemyManager.OnAllEnemiesDeadEvent -= WinLevel;
        EventManager.instance.OnTimeChange -= SetGameTime;
        EventManager.instance.OnEngineerEvent -= EngineerEvent;
        EventManager.instance.OnTerroristEvent -= TerroristEvent;
        EventManager.instance.OnClimateEvent -= ClimateEvent;
    }

    private void Start()
    {
        _gameSpeed = 1f;
        health = maxHealth;
        healthBar.SetHealth(health, maxHealth);
        Debug.Log($"base has {health} health points left");
        circuits = initialMoney;

        EventManager.instance.OnTimeChange += SetGameTime;
        EventManager.instance.OnEngineerEvent += EngineerEvent;
        EventManager.instance.OnTerroristEvent += TerroristEvent;
        EventManager.instance.OnClimateEvent += ClimateEvent;
    }

    void Update()
    {
        circuitosDisplay.text =  circuits.ToString();

        if (Input.GetMouseButtonDown(0) && buildingToPlace != null)
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
                if(shortestDistance <= 3.0f)
                {
                    Building towerBuilt = Instantiate(buildingToPlace, nearestTile.transform.position, Quaternion.identity);
                    nearestTile.buildingHere = towerBuilt;
                    occupiedTiles.Add(nearestTile);
                    nearestTile.isOccupied = true;
                }
                else
                {
                    circuits += buildingToPlace.cost;
                }
                
                grid.SetActive(false);
                CC.gameObject.SetActive(false);
                Cursor.visible = true;
                buildingToPlace = null;
            }
        }
    }

    public void BuyBuilding(MonoBehaviour building)
    {
        // Check if the object passed is a Building or BuffTower
        if (building is Building)
        {
            Building b = (Building)building; // Cast the MonoBehaviour to Building
            if (circuits >= b.cost)
            {
                GameObject Edificio = b.gameObject;
                CC.gameObject.SetActive(true);
                CC.setCursor(Edificio.GetComponent<SpriteRenderer>());
                Cursor.visible = false;
                circuits -= b.cost;
                grid.SetActive(true);
                buildingToPlace = b;
            }
        }
        else if (building is BuffTower)
        {
            BuffTower buffTower = (BuffTower)building; // Cast the MonoBehaviour to BuffTower
            if (circuits >= buffTower.cost)
            {
                Debug.Log("Button pressed for BuffTower");
                GameObject Edificio = buffTower.gameObject;
                CC.gameObject.SetActive(true);
                CC.setCursor(Edificio.GetComponent<SpriteRenderer>());
                Cursor.visible = false;
                circuits -= buffTower.cost;
                grid.SetActive(true);
                buildingToPlace = buffTower;
            }
        }
        else
        {
            Debug.LogError("Invalid building type: " + building.GetType().ToString());
        }
    }

    private void SetGameTime(TimeStates timeState)
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

    private void EngineerEvent(EventChoices choice, float cuantity)
    {
        switch (choice)
        {
            case EventChoices.decline:
                DestroyTurret();
                break;
        }
    }

    private void TerroristEvent(EventChoices choice, int cuantity)
    {
        switch (choice)
        {
            case EventChoices.accept:
                for(int i = 0; i < cuantity; i++)
                {
                    DestroyTurret();
                }
                break;
            case EventChoices.decline:
                health -= cuantity;
                break;
        }
    }

    private void ClimateEvent(EventChoices choice, float cuantity)
    {
        switch (choice)
        {
            case EventChoices.accept:
                circuits -= (int)cuantity;
                break;
        }
    }

    //Al usarse sale el error de que no se puede destruir el edificio para no teenr perdida de datos, el internet dice que guarde una copia de la instancia de la torre, lo hago despues, ojala preguntandole a los profes
    private void DestroyTurret()
    {
        int indexToDestroy = UnityEngine.Random.Range(0, occupiedTiles.Count);

        occupiedTiles[indexToDestroy].isOccupied = false;
        occupiedTiles[indexToDestroy].buildingHere.DestroyTower();
        occupiedTiles[indexToDestroy].buildingHere = null;
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
        circuits = initialMoney;
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
    public void ExitToMainMenu()
    {
        CleanLevel();
        FindObjectOfType<LevelManager>().LoadMainMenuScene();
        Destroy(gameObject);
    }

    public void WinLevel()
    {
        _gameSpeed = 0;
        SetGameSpeed(_gameSpeed);
        UnlockNextLevel();
        WinPanel.SetActive(true);
    }

    private void UnlockNextLevel()
    {
        int currentLvl = Preferences.GetCurrentLvl();
        Preferences.SetMaxLvl(currentLvl + 1);
    }
    public void SetGameSpeed(float speed)
    {
        _gameSpeed = speed;
        Time.timeScale = speed;
    }

    public void AddToMoney(int amount)
    {
        circuits += amount;
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        //Debug.Log($"the base has taken {dmg} damage, and has {health} hit points left");
        healthBar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            health = 0;
            LooseLevel();
            Debug.Log("level lost");
        }
    }

    private void LooseLevel()
    {
        _gameSpeed = 0;
        SetGameSpeed(_gameSpeed);
        LosePanel.SetActive(true);
    }

    public int GetCurrentCircuits() => circuits;

    public void ChangeCircuits(int changeAmount)
    {
        circuits += changeAmount;
    }
}
