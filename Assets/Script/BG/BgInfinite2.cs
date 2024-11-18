using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgInfinite2 : MonoBehaviour
{
    private Vector3 _startPos;
    private float _repeatHeight;
    [SerializeField]
    private float speed;
    private void Start()
    {
        //set default position for canvas bg
        GameManager.Instance.SetBottom(GetComponent<RectTransform>(),-GameManager.Instance.screenHeight);
        GameManager.Instance.SetTop(GetComponent<RectTransform>(),GameManager.Instance.screenHeight);
        
        _startPos = -transform.localPosition;
        _repeatHeight = GameManager.Instance.screenHeight;
    }

    void Update()
    {
        transform.Translate(Vector3.down * (Time.deltaTime * speed));
        // Debug.Log(transform.localPosition.y + " | " + (_startPos.y - _repeatHeight));
        if (transform.localPosition.y < _startPos.y - _repeatHeight)
        {
            transform.localPosition = _startPos;
        }
    }
}
