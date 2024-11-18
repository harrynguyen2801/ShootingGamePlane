using System;
using Script;
using UnityEngine;
using UnityEngine.Events;

public static class ActionManager
{
    public static Action<float> OnUpdateSpeedBullet;
    public static Action<float> OnUpdateTypeBullet;
    public static Action<int> OnUpdateAirPlane;
    public static Action<EnumManager.ETypeBullet> OnUpdateBullet;
    public static Action OnEnemyDestroy;
    public static Action<int> OnActiveWaveIdx;
    public static Action OnPlayerDamage;
    public static Action OnUpdateUIFollowBoss;
    public static Action OnUpdateUIDefaultBoss;
    public static Action OnActiveMagnet;
}
