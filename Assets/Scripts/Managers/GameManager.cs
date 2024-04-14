using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] private int maxHealth;
    [SerializeField] private int money;
    private int health;
    public int circuitos;
    public TMP_Text circuitosDisplay;
    public GameObject grid;
    public CustomCursor CC;
    public Tile[] tiles;

    private Building bAColocar;

    private int goldRate = 0;

    private EnemyManager enemyManager;

    private float _gameSpeed;

    /*private void OnEnable()
    {
        enemyManager.OnEnemyKilledEvent += AddToMoney;
        enemyManager.OnAllEnemiesDeadEvent += WaveFinished;
    }*/

    /*private void OnDisable()
    {
        enemyManager.OnEnemyKilledEvent -= AddToMoney;
        enemyManager.OnAllEnemiesDeadEvent -= WaveFinished;

    }*/
    public int Circuitos
    {
        get { return goldRate; }
        set { goldRate += value; }
    }

    private void Start()
    {
        _gameSpeed = 1f;
        health = maxHealth;
        circuitos = money;
    }
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update(){
        circuitosDisplay.text = "Recursos: " + circuitos.ToString();

        if(Input.GetMouseButtonDown(0) && bAColocar != null){
            Tile nearestTile = null;
            float shortestDistance = 100;
            foreach(Tile tile in tiles){
                float dist = Vector2.Distance(tile.transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));

                if(dist < shortestDistance){
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

    public void BuyBuilding(Building b){
        if (circuitos >= b.cost){
            Debug.Log("boton oprimido");
            GameObject Edificio = b.gameObject;
            CC.gameObject.SetActive(true);
            CC.setCursor(Edificio.GetComponent<SpriteRenderer>());
            Cursor.visible = false;
            circuitos -= b.cost;
            grid.SetActive(true);
            bAColocar = b;
        }
    }

    public void PruebaBoton()
    {
        Debug.Log(70);
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
        circuitos = money;
        enemyManager.ClearEnemiesList();
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
        if(health <= 0)
        {
            health = 0;
            LooseLevel();
        }
    }

    private void LooseLevel()
    {
        RestartLevel();
    }

    public int GetCurrentMoney() => circuitos;

    private void WaveFinished()
    {
        
    }
}
