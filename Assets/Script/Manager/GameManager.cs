using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance => _instance;
    private static GameManager _instance;

    public GameObject canvas;
    public GameObject dragToMove;
    
    public float screenWidth;
    public float screenHeight;
    
    [SerializeField]
    private Image background2;
    [SerializeField]
    private Image background1;

    public int bullet;
    public bool isMaxBullet;
    public int bulletDamage = 5;

    public EnumManager.ETypeBullet eTypeBullet;
    public EnumManager.EAirPlaneType eAirPlaneType;
    
    public int waveEnemyCurrent = 0;
    public bool isPlayerFire = false;
    private void OnEnable()
    {
        ActionManager.OnUpdateBullet += OnUpdateBullet;
    }
    private void OnDisable()
    {
        ActionManager.OnUpdateBullet -= OnUpdateBullet;
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

        eAirPlaneType = EnumManager.EAirPlaneType.Zeus;
        bullet = 1;
        eTypeBullet = EnumManager.ETypeBullet.Bullet1;
        
        screenHeight = canvas.GetComponent<RectTransform>().rect.height;
        screenWidth = canvas.GetComponent<RectTransform>().rect.width;
        SetBottom(background1.GetComponent<RectTransform>(),-screenHeight);
        SetTop(background1.GetComponent<RectTransform>(),screenHeight);
    }

    private void Start()
    {
        ActionManager.OnActiveWaveIdx?.Invoke(1);
        waveEnemyCurrent = 1;
        SoundManager.Instance.PlayBgm(EnumManager.EBgmSoundName.MainBg);
    }

    #region Set Rect Transform

    public void SetTop(RectTransform rt, float top)
    {
        rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
    }
 
    public void SetBottom(RectTransform rt, float bottom)
    {
        rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
    }

    #endregion

    private void OnUpdateBullet(EnumManager.ETypeBullet eType)
    {
        if (bullet < 2)
        {
            bullet += 1;
            bulletDamage += bullet * 5;
        }
        // eTypeBullet = eType;
    }
}
