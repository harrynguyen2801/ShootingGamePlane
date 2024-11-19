using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class EnemyFallManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();

    void Start()
    {
        StartCoroutine(RandomlyDropMonsters());
    }

    private IEnumerator<WaitForSeconds> RandomlyDropMonsters()
    {
        List<int> randomIndices = new List<int>();
        for (int i = 0; i < enemyList.Count; i++)
            randomIndices.Add(i);

        for (int i = 0; i < randomIndices.Count; i++)
        {
            int randomIndex = Random.Range(0, randomIndices.Count);
            (randomIndices[i], randomIndices[randomIndex]) = (randomIndices[randomIndex], randomIndices[i]);
        }

        foreach (int index in randomIndices)
        {
            GameObject enemy = enemyList[index];
            if (enemy != null)
            {
                enemy.SetActive(true);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
}
