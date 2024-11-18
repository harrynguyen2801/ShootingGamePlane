using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttractor : MonoBehaviour
{
    public float attractionRange = 2f;
    public float attractionSpeed = 5f;
    public bool canAttract = false;

    private void OnEnable()
    {
        ActionManager.OnActiveMagnet += ActiveMagnet;
    }

    private void OnDisable()
    {
        ActionManager.OnActiveMagnet -= ActiveMagnet;
    }

    private void ActiveMagnet()
    {
        canAttract = true;
    }

    private void Update()
    {
        if (canAttract)
        {
            Collider2D[] items = Physics2D.OverlapCircleAll(transform.position, attractionRange);
        
            foreach (var item in items)
            {
                if (item.CompareTag("Item"))
                {
                    item.transform.position = Vector3.MoveTowards(
                        item.transform.position,
                        transform.position,
                        attractionSpeed * Time.deltaTime
                    );
                }
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attractionRange);
    }
}