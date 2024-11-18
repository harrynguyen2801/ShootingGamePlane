using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class BulletCircleRed : MonoBehaviour
{
    private float _speed = 5f;
    public float lifeTime = 2f;
    public float _damage = 10;
    
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
        ObjectPoolingBullet.Instance.ReturnBulletCircleRed(gameObject);
    }
    
    public void Init(float prDamage, float prSpeed, Vector3 pos,  int arrGun, float scale = 1f)
    {
        gameObject.SetActive(true);
        transform.position = Player.Instance.arrGun[arrGun].position + pos;
        transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
        // damage = prDamage * GameManager.Instance.config.dataGame.valuePower[PlayerPrefs.GetInt("power")];
        _speed = prSpeed;
        StartCoroutine(LerpScale(transform.localScale, scale));
    }

    IEnumerator LerpScale(Vector3 scaleCur, float scaleTarget)
    {
        var _scaleTarget = new Vector3(scaleTarget, scaleTarget, scaleTarget);
        float elapsed = 0f;
        float duration = .55f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            Vector3 currentScale = Vector3.Lerp(scaleCur, _scaleTarget, t);
            gameObject.transform.localScale = currentScale;
            yield return null;
        }
        gameObject.transform.localScale = _scaleTarget;
    }
    void Update()
    {
        transform.position += new Vector3(0f, _speed * Time.deltaTime, 0f);
    }
}
