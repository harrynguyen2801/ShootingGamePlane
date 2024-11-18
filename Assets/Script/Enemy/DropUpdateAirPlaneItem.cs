using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class DropUpdateAirPlaneItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.Collect);
            ActionManager.OnUpdateAirPlane?.Invoke((int)EnumManager.EAirPlaneType.Tarragon);
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (GameManager.Instance.isPlayerFire)
        {
            gameObject.transform.Translate(0, -1f * Time.deltaTime, 0);
        }
    }
}
