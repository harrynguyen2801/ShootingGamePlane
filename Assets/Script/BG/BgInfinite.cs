using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgInfinite : MonoBehaviour
{
    private Vector3 _startPos;
    private float _repeatHeight;
    [SerializeField]
    private float speed;
    private void Awake()
    {
        _startPos = transform.position;
        _repeatHeight = GetComponent<BoxCollider2D>().size.y/2;
    }

    void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * speed));
        Debug.Log(transform.localPosition.y + " | " + (_startPos.y - _repeatHeight));
        if (transform.localPosition.y < _startPos.y - _repeatHeight)
        {
            transform.localPosition = _startPos;
        }
    }
}
