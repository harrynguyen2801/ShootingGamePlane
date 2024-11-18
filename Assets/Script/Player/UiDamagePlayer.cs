using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiDamagePlayer : MonoBehaviour
{
    private Animator _animator;
    private void OnEnable()
    {
        ActionManager.OnPlayerDamage += OnDamagePlayer;
    }

    private void OnDisable()
    {
        ActionManager.OnPlayerDamage -= OnDamagePlayer;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnDamagePlayer()
    {
        _animator.SetTrigger("Damage");
    }
}
