using System;
using UnityEngine;

public class EnemyDropManager : MonoBehaviour
{
    public GameObject[] itemPrefab;
    private int[] _dropItemIndexes = { 1, 4, 7, 9 , 12};
    private int _enemyCounter = 0; 
    private int _currentItemDrop = 0;
    private void Start()
    {
        UpdateEnemyIndexes();
    }

    public void EnemyDied(Transform enemyTransform)
    {
        _enemyCounter++;
        
        if (Array.Exists(_dropItemIndexes, index => index == _enemyCounter))
        {
            Debug.Log(_enemyCounter + " | count");
            DropItem(enemyTransform.position);
        }
    }

    private void DropItem(Vector3 position)
    {
        if (itemPrefab != null)
        {
            Instantiate(itemPrefab[_currentItemDrop], position, Quaternion.identity);
            _currentItemDrop++;
        }
    }

    private void UpdateEnemyIndexes()
    {
        Transform parent = transform;
        for (int i = 0; i < parent.childCount; i++)
        {
            parent.GetChild(i).name = "Enemy " + (i + 1);
        }
    }
}