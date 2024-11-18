using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance => _instance;
    private static WaveManager _instance;

    public GameObject[] waves;
    public int[] waveEnemyNumbers = new int[]{0,1,2};
    private void OnEnable()
    {
        ActionManager.OnActiveWaveIdx += OnActiveWaveWithIdx;
    }

    private void OnDisable()
    {
        ActionManager.OnActiveWaveIdx -= OnActiveWaveWithIdx;
    }

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
    }

    public void OnActiveWaveWithIdx(int waveIndex)
    {
        foreach (var wave in waves)
        {
            wave.SetActive(false);
        }
        waves[waveIndex-1].SetActive(true);
        GameManager.Instance.waveEnemyCurrent = waveIndex;
    }
}    

    

