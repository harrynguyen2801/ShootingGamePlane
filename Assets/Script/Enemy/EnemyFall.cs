using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; // Thư viện của DOTween

public class EnemyFall : MonoBehaviour
{
    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y + 10f, transform.position.z);

        transform.DOMoveY(_startPos.y, .5f)
            .SetEase(Ease.InQuad);
    }
}