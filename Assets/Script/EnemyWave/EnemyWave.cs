using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWave : MonoBehaviour
{
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
        _enemiesInWave = gameObject.transform.childCount;
        // _enemiesInWave = WaveManager.Instance.waveEnemyNumbers[GameManager.Instance.waveEnemyCurrent];
        Debug.Log("wave enemy " + GameManager.Instance.waveEnemyCurrent + " / " + _enemiesInWave);
    }

    private void OnEnemyDestroy()
    {
        _enemiesInWave--;
        if (_enemiesInWave <= 0)
        {
            _allChildrenInactive = true;
        }
    }
    void Update()
    {
        if (_allChildrenInactive)
        {
            ActionManager.OnActiveWaveIdx?.Invoke(GameManager.Instance.waveEnemyCurrent + 1);
            Debug.Log("All Children Inactive, Active Wave 2");
        }
    }
}

