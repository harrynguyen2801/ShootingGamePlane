using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class DropMargnetItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ActionManager.OnActiveMagnet?.Invoke();
            SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.Collect);
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
