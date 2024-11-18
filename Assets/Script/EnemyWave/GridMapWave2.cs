using System;
using Script;
using Unity.Mathematics;
using UnityEngine;

public class GridMapWave2 : MonoBehaviour
{
    public int rows = 4;
    public int columns = 8;
    public GameObject[] listEnemyPrefab;

    private int[,] grid;

    private int[,] wave1 = new int[,]
    {
        {1, 2, 1, 3, 2, 3, 3, 2},
        {1, 1, 2, 1, 1, 3, 1, 1},
        {2, 2, 1, 1, 1, 1, 2, 1},
        {2, 1, 2, 2, 1, 2, 3, 2},
        {2, 1, 2, 2, 1, 2, 3, 2},
    };
    private float _spacingX = .5f;
    private float _spacingY = .5f;
    public float spacing = .3f;
    public Vector2 startPosition = new Vector2(-1.1f,0f);
    private float _speed = -1.5f;
    
    [SerializeField]
    private int _enemiesInWave;
    private bool _allChildrenInactive = false;
    
    private void OnEnable()
    {
        ActionManager.OnEnemyDestroy += OnEnemyDestroy;
    }

    private void OnDisable()
    {
        ActionManager.OnEnemyDestroy -= OnEnemyDestroy;
    }
    
    void Start()
    {
        PopulateGrid();
        _enemiesInWave = gameObject.transform.childCount;
    }
    void PopulateGrid()
    {
        for (int k = 0; k < listEnemyPrefab.Length-1; k++)
        {
            grid = new int[rows, columns];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Vector2 position = startPosition + new Vector2(j * spacing, -i * spacing) 
                                                     + new Vector2(0,(k+1) * columns * spacing/1.5f);
                    grid[i, j] = k;
                    SpawnEnemy(grid[i, j], position,quaternion.identity); 
                }
            }
        }

        SpawnRow(3, new  Vector2(0,listEnemyPrefab.Length * columns * spacing/1.6f));
        SpawnRow(2, new  Vector2(.5f,listEnemyPrefab.Length * columns * spacing/1.6f + .5f));
    }

    void SpawnEnemy(int enemyIdx, Vector3 position, Quaternion rotation)
    {
        GameObject enemyPrefab = listEnemyPrefab[enemyIdx];
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, position, rotation,transform);
        }
    }
    
    void SpawnRow(int enemyCount, Vector2 spawnPosition)
    {
        GameObject enemyPrefab = listEnemyPrefab[9];
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 position = startPosition + new Vector2(i * spacing * 4 + spawnPosition.x, spawnPosition.y);
            Instantiate(enemyPrefab, position, Quaternion.identity,transform);
        }
    }
    
    private void OnEnemyDestroy()
    {
        _enemiesInWave--;
        if (_enemiesInWave <= 0)
        {
            _allChildrenInactive = true;
        }
    }

    private void Update()
    {
        transform.position += new Vector3(0f, _speed * Time.deltaTime, 0f);
        if (transform.position.y < -25f || _allChildrenInactive)
        {
            ActionManager.OnActiveWaveIdx?.Invoke(GameManager.Instance.waveEnemyCurrent + 1);
        }
    }
}