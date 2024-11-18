using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed = 5f;
    public float lifeTime = 1.75f;
    private float _damage = 10f;

    private void Start()
    {
        _damage = GameManager.Instance.bulletDamage;
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            _damage = GameManager.Instance.bulletDamage;
        }
        Invoke(nameof(Deactivate), lifeTime);
    }
    private void OnDisable()
    {
        CancelInvoke();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            _damage -= other.GetComponent<Enemy>().GetHealth();
            if (_damage <= 0 )
            {
                Deactivate();
            }
        }
    }
    private void Deactivate()
    {
        ObjectPoolingBullet.Instance.ReturnBullet(gameObject);
    }
    
    public void Init(float prDamage, float prSpeed, Vector3 pos,  int arrGun, float scale = 1f)
    {
        gameObject.SetActive(true);
        transform.position = Player.Instance.arrGun[arrGun].position + pos;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        _speed = prSpeed;
        StartCoroutine(LerpPosition(Player.Instance.arrGun[arrGun].position.x,transform.position.x));
    }

    IEnumerator LerpPosition(float posX, float targetX)
    {
        float elapsed = 0f;
        float duration = 0.15f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float currentPosition = Mathf.Lerp(posX, targetX, t);
            gameObject.transform.position = new Vector3(currentPosition, transform.position.y, transform.position.z);
            yield return null;
        }

        gameObject.transform.position = new Vector3(targetX, transform.position.y, transform.position.z);
    }
    void Update()
    {
        transform.position += new Vector3(0f, _speed * Time.deltaTime, 0f);
    }
}
