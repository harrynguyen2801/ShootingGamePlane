namespace Script
{
    public static class EnumManager
    {
        public enum EAirPlaneType
        {
            Zeus = 0,
            Tarragon = 2,
        }
        
        public enum ETypeBullet
        {
            Bullet1 = 1,
            Bullet2 = 2,
            Bullet3 = 3,
        }
        
        public enum EEnemyType
        {
            None = 0,
            BoOngVang = 1,
            BoOngXanhLa = 2,
            BoOngXanhDuong = 3,
            Boss = 11,
        }

        public enum ESfxSoundName
        {
            None,
            HitEnemy,
            HitBoss,
            EnemyDie,
            BossDie,
            Bullet,
            Collect,
        }

        public enum EBgmSoundName
        {
            None,
            MainBg,
        }
    }
}