using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public int cost;

    private Scanner _scanner;
    private Weapon _weapon;

    public GameObject explosionPrefab;
    private SpriteRenderer _spriteRenderer;
    GameManager gm = GameManager.instance;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _weapon = GetComponent<Weapon>();
        _scanner = GetComponentInChildren<Scanner>();
    }

    private void Start()
    {
        InvokeRepeating(nameof(ScanEnemies), 0f, 0.1f);
        //Debug.Log("paso scan");

    }
 
    private void Update()
    {
        if(_scanner.TargetFound())
        {
            _weapon.Shoot(_scanner.GetTarget());
        }
    }

    private void ScanEnemies()
    {
        _scanner.Scan();
    }

    public int GetCost()
    {
        return cost;
    }
    public void DestroyTower()
    {
        Debug.Log("DestroyTower called");
        if (explosionPrefab != null)
        {
            GameObject explosionInstance = Instantiate(explosionPrefab, transform.position, transform.rotation);
            ParticleSystem explosionParticles = explosionInstance.GetComponent<ParticleSystem>();
            if (explosionParticles != null)
            {
                Debug.Log("Playing explosion");
                explosionParticles.Play();
            }
            else
            {
                Debug.LogError("No ParticleSystem found");
            }
        }
        else
        {
            Debug.LogError("explosionPrefab not assigned");
        }
        if (_spriteRenderer != null)
        {
            Color color = _spriteRenderer.color;
            _spriteRenderer.color = new Color(color.r, color.g, color.b, 0f);
        }
        StartCoroutine(DestroyAfterDelay(0.1f));
    }
    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
