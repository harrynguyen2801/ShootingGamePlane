using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingEnemy : MonoBehaviour
{
    public static ObjectPoolingEnemy Instance => _instance;
    private static ObjectPoolingEnemy _instance;

    private List<GameObject> _listPoolingObjects = new List<GameObject>();
    private int _amountToPool = 30;

    [SerializeField] private GameObject[] listEnemy;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        
        InitializePool();
    }
    private void InitializePool()
    {
        _listPoolingObjects = new List<GameObject>();

        foreach (GameObject enemy in _listPoolingObjects)
        {
            enemy.SetActive(false);
            _listPoolingObjects.Add(enemy);
        }
    }

    public GameObject GetEnemy(Vector3 position, Quaternion rotation)
    {
        foreach (GameObject bullet in _listPoolingObjects)
        {
            if (!bullet.activeInHierarchy)
            {
                bullet.transform.position = position;
                bullet.transform.rotation = rotation;
                bullet.SetActive(true);
                return bullet;
            }
        }

        return null;
        // If all bullets are in use, expand the pool
        // GameObject newBullet = Instantiate(pbPooling);
        // newBullet.transform.position = position;
        // newBullet.transform.rotation = rotation;
        // _listPoolingObjects.Add(newBullet);
        // return newBullet;
    }
    
    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemy.transform.position = Vector3.zero;
        enemy.transform.rotation = Quaternion.identity;
    }
}
