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
    }

    [SerializeField] private int maxHealth;
    [SerializeField] private int initialMoney;

    private int health;
    private int previusHealth;
    private int circuits;
    public Text circuitosDisplay;
    public GameObject grid;
    public CustomCursor CC;
    public CustomCursor rangeDisplay;
    public Tile[] tiles;
    public GameObject eventPanel;
    public GameObject WinPanel;
    public GameObject LosePanel;
    public HealthbarBehavior healthBar;

    private Building buildingToPlace;
    private List<Tile> occupiedTiles = new List<Tile>(); 

    private float _gameSpeed;

    private void OnDisable()
    {
        EventManager.instance.OnEnemyKilled -= ChangeCircuits;
        EventManager.instance.OnAllEnemiesDead -= WinLevel;
        EventManager.instance.OnTimeChange -= SetGameTime;
        EventManager.instance.OnEngineerEvent -= EngineerEvent;
        EventManager.instance.OnTerroristEvent -= TerroristEvent;
        EventManager.instance.OnClimateEvent -= ClimateEvent;
        EventManager.instance.OnStrikeEvent -= StrikeEvent;
        EventManager.instance.OnStrikeFinish -= StrikeFinish;
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
        EventManager.instance.OnStrikeEvent += StrikeEvent;
        EventManager.instance.OnEnemyKilled += ChangeCircuits;
        EventManager.instance.OnAllEnemiesDead += WinLevel;
        EventManager.instance.OnStrikeFinish += StrikeFinish;
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
                AudioManager.GetInstance().PlayTowerPlaced();
                rangeDisplay.gameObject.SetActive(false);
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
                Scanner scanner = Edificio.GetComponentInChildren<Scanner>();
                if(scanner != null)
                {
                    Debug.Log("got the scanner");
                    GameObject rangeIndicator = scanner.rangeIndicator;
                    rangeDisplay.gameObject.SetActive(true);
                    rangeDisplay.setCursor(rangeIndicator.GetComponent<SpriteRenderer>());
                    rangeDisplay.transform.localScale = new Vector3(scanner.rangeScan *2, scanner.rangeScan*2, 1);
                }
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
                Scanner scanner = Edificio.GetComponentInChildren<Scanner>();
                if (scanner != null)
                {
                    Debug.Log("got the buff scanner");
                    GameObject rangeIndicator = scanner.rangeIndicator;
                    rangeDisplay.gameObject.SetActive(true);
                    rangeDisplay.setCursor(rangeIndicator.GetComponent<SpriteRenderer>());
                    rangeDisplay.transform.localScale = new Vector3(scanner.rangeScan * 2, scanner.rangeScan * 2, 1);
                }
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
                Building buildingToDestroy = DestroyTurret();
                ChangeCircuits((int)MathF.Round(buildingToDestroy.cost * cuantity));
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

    private void StrikeEvent(EventChoices choice, float rate, int counter, int reward)
    {
        switch (choice)
        {
            case EventChoices.accept:
                previusHealth = health;
                health = (int)rate;
                healthBar.SetHealth(health, maxHealth);
                break;
        }
    }

    private void StrikeFinish(int reward)
    {
        circuits += reward;
        health = previusHealth;
        healthBar.SetHealth(health - 1, maxHealth);
    }

    private Building DestroyTurret()
    {
        Building buildingToDestroy;
        int indexToDestroy = UnityEngine.Random.Range(0, occupiedTiles.Count);

        occupiedTiles[indexToDestroy].isOccupied = false;
        buildingToDestroy = occupiedTiles[indexToDestroy].buildingHere;
        occupiedTiles[indexToDestroy].buildingHere.DestroyTower();
        occupiedTiles[indexToDestroy].buildingHere = null;

        return buildingToDestroy;
    }

    public void RestartLevel()
    {
        AudioManager.GetInstance().PlayButtonPressed();
        FindObjectOfType<LevelManager>().LoadCurrentScene();
        CleanLevel();
        Destroy(gameObject);
    }


    public void CleanLevel()
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
        AudioManager.GetInstance().PlayButtonPressed();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Destroy(gameObject);
    }
    public void ExitToMainMenu()
    {
        CleanLevel();
        AudioManager.GetInstance().PlayButtonPressed();
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

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        //Debug.Log($"the base has taken {dmg} damage, and has {health} hit points left");
        healthBar.SetHealth(health, maxHealth);
        if (health <= 0)
        {
            health = 0;
            AudioManager.GetInstance().PlayPlayerDeath();
            LooseLevel();
            Debug.Log("level lost");
        }
    }

    public void SetHealth(int amount)
    {
        health = amount;
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

        if(circuits < 0)
        {
            circuits = 0;
        }
    }
}
