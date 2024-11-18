using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class EnemyAudioController : MonoBehaviour
{
    [SerializeField] private AudioClip dieEnemyClip;
    [SerializeField] private AudioClip hitEnemyClip;
    
    [SerializeField] private AudioClip dieBossClip;
    [SerializeField] private AudioClip hitBossClip;
    
    [SerializeField] private AudioSource audioSource;

    public void PlayDieEnemySound(EnumManager.EEnemyType type)
    {
        if (audioSource != null && dieEnemyClip != null)
        {
            if (type == EnumManager.EEnemyType.Boss)
            {
                audioSource.PlayOneShot(dieEnemyClip);
            }
            else
            {
                audioSource.PlayOneShot(dieBossClip);
            }
        }
    }

    public void PlayHitEnemySound(EnumManager.EEnemyType type)
    {
        if (audioSource != null && dieEnemyClip != null)
        {
            if (type == EnumManager.EEnemyType.Boss)
            {
                audioSource.PlayOneShot(hitBossClip);
            }
            else
            {
                audioSource.PlayOneShot(hitEnemyClip);
            }
        }
    }
}
