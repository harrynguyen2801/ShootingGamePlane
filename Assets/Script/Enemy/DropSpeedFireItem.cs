using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class DropSpeedFireItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySfx(EnumManager.ESfxSoundName.Collect);
            ActionManager.OnUpdateSpeedBullet?.Invoke(Player.Instance.fireRate / 2);
            // Debug.Log("Drop Speed Fire");
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isPlayerFire)
        {
            gameObject.transform.Translate(0, -1f * Time.deltaTime, 0);
        }
    }
}
