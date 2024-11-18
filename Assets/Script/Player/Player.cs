using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Script;
using Unity.Mathematics;
using UnityEngine;
public class Player : MonoBehaviour
{
    private Transform _drag = null;
    private Vector3 _offset;
    
    public float fireRate = 0.065f; // Time between shots in seconds
    private float _fireCooldown; 
    private float _nextFire; 

    public AirPlanceSo[] airPlanceSo;
    
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    public Transform[] arrGun;
    
    [SerializeField] private AudioClip bulletSound;
    [SerializeField] private AudioSource audioSource;
    
    public float blinkDuration = 0.8f;
    public float blinkInterval = 0.1f;

    private bool _isBlinking = false;
    
    private void OnEnable()
    {
        ActionManager.OnUpdateAirPlane += OnUpdateAirPlane;
    }
    
    private void OnDisable()
    {
        ActionManager.OnUpdateAirPlane -= OnUpdateAirPlane;
    }

    public static Player Instance => _instance;
    private static Player _instance;

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
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        audioSource.clip = bulletSound;
        gameObject.transform.DOMoveY(-3.11f, 1f);        
    }

    private void OnUpdateAirPlane(int idAirPlane)
    {
        ChangeAirplane(idAirPlane);
    }
    private void ChangeAirplane(int idAirPlane)
    {
        AirPlanceSo airPlane = Array.Find(airPlanceSo, x => x.idAirPlane == idAirPlane);
        if (airPlane != null)
        {
            transform.DOScale(2.25f, 0.5f)
                .OnComplete(() => 
                    transform.DOScale(1f, 0.5f));
            _spriteRenderer.sprite = airPlane.sprite;
            _animator.runtimeAnimatorController = airPlane.animator;
            GameManager.Instance.eAirPlaneType = (EnumManager.EAirPlaneType)Enum.ToObject(typeof(EnumManager.EAirPlaneType), idAirPlane);;
            GameManager.Instance.isMaxBullet = true;
            fireRate = 0.05f;
            GameManager.Instance.bulletDamage = 20;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Player Damage");
            ActionManager.OnPlayerDamage?.Invoke();
            
            if (!_isBlinking)
            {
                StartCoroutine(Blink());
            }
        }
    }
    
    IEnumerator Blink()
    {
        _isBlinking = true;
        float elapsedTime = 0f;

        while (elapsedTime < blinkDuration)
        {
            _spriteRenderer.enabled = !_spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            elapsedTime += blinkInterval;
        }
        _spriteRenderer.enabled = true;
        _isBlinking = false;
    }

    void Update()
    {
        //Fire Bullet
        Fire2();
        
        if (Input.GetButton("Fire1") && Time.time > _nextFire)
        {
            audioSource.pitch = 1.0f + (1.0f - fireRate);
            audioSource.PlayOneShot(bulletSound);
            _nextFire = Time.time + fireRate;
        }
        //Drag Player Airplance
        if (Input.GetMouseButtonDown(0))
        {
            // PlayBulletSound();
            GameManager.Instance.isPlayerFire = true;
            //Disable drag to move image tutorial
            GameManager.Instance.dragToMove.SetActive(false);
            //drag logic
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero,float.PositiveInfinity,LayerMask.GetMask("Player"));
            if (hit)
            {
                _drag = hit.transform;
                _offset = _drag.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }
            else
            {
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPosition.z = 0;
                transform.position = targetPosition; 
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameManager.Instance.isPlayerFire = false;
            _drag = null;
        }

        if (_drag != null)
        {
            _drag.position = _offset + Camera.main.ScreenToWorldPoint(Input.mousePosition);
        } 
    }
    private void Fire2()
    {
        if (Input.GetButton("Fire1") && _fireCooldown <= 0f)
        {
            ObjectPoolingBullet.Instance.GetBullet(transform.position,quaternion.identity);
            _fireCooldown = fireRate; 
        }
        _fireCooldown -= Time.deltaTime;
    }
    
    public void PlayBulletSound()
    {
        if (audioSource != null && audioSource != null)
        {
            audioSource.pitch = 1.0f + (1.0f - fireRate);
            audioSource.Play();
        }
    }
}
