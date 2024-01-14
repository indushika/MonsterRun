namespace MonsterRun.Round
{
    public class RoundDataConfig
    {
        private uint roundNumber;
        private int monsterSpawnCount;

        public uint RoundNumber => roundNumber;
        public int MonsterSpawnCount => monsterSpawnCount;

        public RoundDataConfig(uint roundNumber, int monsterSpawnCount)
        {
            this.roundNumber = roundNumber;
            this.monsterSpawnCount = monsterSpawnCount; 
        }
    }
}