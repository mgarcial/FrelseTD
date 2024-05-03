using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    [SerializeField] private float rangeScan = 10f;
    private EnemyManager _enemyManager;
    private Transform _target;

    private void Awake()
    {
        _enemyManager = EnemyManager.instance;
    }
    // Start is called before the first frame update
    void Start()
    {
        float radiusScale = rangeScan * 2;
        transform.localScale = new Vector2(radiusScale, radiusScale);
    }

    public void Scan()
    {
        Transform enemyTargeted = null;

        List<Enemy> enemies = _enemyManager.GetEnemiesList();
    
        foreach(Enemy enemy in enemies)
        {
            float currentDistance = Vector2.Distance(transform.position, enemy.transform.position);

            if (enemyTargeted == null && currentDistance <= rangeScan)
            {
                enemyTargeted = enemy.transform;
            }
        }

        _target = enemyTargeted;
        
    }
    public bool TargetFound()
    {
        return _target != null;
    }

    public Transform GetTarget()
    {
        return _target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeScan);
    }

}
