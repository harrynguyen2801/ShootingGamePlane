using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float health;
    private float _healthMax;
    [SerializeField]
    private Animator animatorExplo1;
    [SerializeField]
    private Animator animatorExplo2;

    [SerializeField] private GameObject[] dropObjects;
    public Text healthText;
    public Slider healthSlider;
    public enum EDropItem
    {
        None = 0,
        Speed = 1,
        Guard = 2,
        Magnet = 3,
        Rocket = 4,
    }
    public EnumManager.EEnemyType eEnemyType;

    public EDropItem itemToDrop;
    private EnemyAudioController _enemyAudioController;

    private void Awake()
    {
        _enemyAudioController = GetComponent<EnemyAudioController>();
    }

    private void Start()
    {
        _healthMax = health;
        if (healthText != null)
        {
            healthText.text = health.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            animatorExplo1.gameObject.SetActive(true);
            // TakeDamage(other.GetComponent<Bullet>().damage);
            TakeDamage(GameManager.Instance.bulletDamage);
            animatorExplo1.SetTrigger("Explosion");
        }
    }
    public void TakeDamage(int _damage)
    {
        health -= _damage;
        if (eEnemyType == EnumManager.EEnemyType.Boss)
        {
            healthText.text = health.ToString();
            healthSlider.value = health/_healthMax;
            _enemyAudioController.PlayHitEnemySound(EnumManager.EEnemyType.Boss);
        }
        if (health < 0)
        {
            if (eEnemyType == EnumManager.EEnemyType.Boss)
            {
                _enemyAudioController.PlayDieEnemySound(EnumManager.EEnemyType.Boss);
            }
            else
            {
                _enemyAudioController.PlayDieEnemySound(EnumManager.EEnemyType.BoOngVang);
            }
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        animatorExplo2.gameObject.SetActive(true);
        animatorExplo2.SetTrigger("Explosion");
        if (eEnemyType == EnumManager.EEnemyType.Boss)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            EnemyDropManager manager = transform.parent.GetComponent<EnemyDropManager>();
            if (manager != null)
            {
                manager.EnemyDied(gameObject.transform);
            }
        }
        DropAwardItem();
        ActionManager.OnEnemyDestroy?.Invoke();
        gameObject.SetActive(false);
    }

    private void DropAwardItem()
    {
        if (itemToDrop != EDropItem.None)
        {
            Instantiate(dropObjects[(int)itemToDrop-1], transform.position, Quaternion.identity);
        }
    }

    public float GetHealth()
    {
        return health;
    }
}
