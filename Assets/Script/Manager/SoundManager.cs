using System;
using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance => _instance;
    private static SoundManager _instance;
    
    public Sound[] sfxSounds,bgmSounds;
    public AudioSource bgmSource, sfxSource;

    #region DataSoundsName
    public Dictionary<EnumManager.ESfxSoundName,string> sfxSoundNames = new Dictionary<EnumManager.ESfxSoundName, string>()
    {
        { EnumManager.ESfxSoundName.HitEnemy ,"HitEnemy"},
        { EnumManager.ESfxSoundName.HitBoss ,"HitBoss"},
        { EnumManager.ESfxSoundName.EnemyDie ,"EnemyDie"},
        { EnumManager.ESfxSoundName.BossDie ,"BossDie"},
        { EnumManager.ESfxSoundName.Bullet ,"Bullet"},
        { EnumManager.ESfxSoundName.Collect ,"Collect"},
    };
    
    public Dictionary<EnumManager.EBgmSoundName,string> bgmSoundNames = new Dictionary<EnumManager.EBgmSoundName, string>()
    {
        { EnumManager.EBgmSoundName.MainBg ,"MainBgm"},
    };

    #endregion
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
    }
    
    public void PlaySfx(EnumManager.ESfxSoundName name)
    {
        Sound s = Array.Find(sfxSounds, x => x.soundName == sfxSoundNames[name]);
        if (s == null)
        {
            Debug.Log("Sfx Not Found");
            return;
        }

        if (sfxSource.isPlaying)
        {
            return;
        }
        sfxSource.clip = s.clipSound;
        sfxSource.Play();
    }
    public void PlayBgm(EnumManager.EBgmSoundName name)
    {
        Sound s = Array.Find(bgmSounds, x => x.soundName == bgmSoundNames[name]);
        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            bgmSource.clip = s.clipSound;
            bgmSource.Play();
        }
    }
}
