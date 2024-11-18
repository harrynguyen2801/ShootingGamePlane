using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBossController : MonoBehaviour
{
    public RectTransform UIBoss;

    private void OnEnable()
    {
        ActionManager.OnUpdateUIFollowBoss += OnUpdateUIFollowBoss;
        ActionManager.OnUpdateUIDefaultBoss += OnUpdateUIDefaultBoss;
    }

    private void OnDisable()
    {
        ActionManager.OnUpdateUIFollowBoss -= OnUpdateUIFollowBoss;
        ActionManager.OnUpdateUIDefaultBoss -= OnUpdateUIDefaultBoss;
    }

    void Start()
    {
        float scale = GameManager.Instance.screenHeight / GameManager.Instance.screenWidth;
        Vector3 scaleFactor = new Vector3(scale/1.5f,scale/1.5f,scale/1.5f);
        transform.localScale = scaleFactor;
        OnUpdateUIDefaultBoss();
    }

    private void OnUpdateUIFollowBoss()
    {
        GameManager.Instance.SetBottom(UIBoss,GameManager.Instance.screenHeight * 2/2.75f);
        GameManager.Instance.SetTop(UIBoss,-GameManager.Instance.screenHeight * 2/2.75f);
    }

    private void OnUpdateUIDefaultBoss()
    {
        GameManager.Instance.SetBottom(UIBoss,GameManager.Instance.screenHeight);
        GameManager.Instance.SetTop(UIBoss,-GameManager.Instance.screenHeight);
    }
}
