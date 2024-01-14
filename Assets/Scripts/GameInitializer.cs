using MonsterRun.Monster;
using MonsterRun.Round;
using UnityEngine;


namespace MonsterRun.Main
{
    public class GameInitializer
    {
        private GameSettings gameSettings;
        private RoundRunner roundRunner;
        private RoundDataProvider roundDataProvider;
        private MonsterWaveSpawner monsterWaveSpawner;
        private PoolProvider poolProvider; 
        public GameInitializer(GameSettings gameSettings, RoundRunner roundRunner,
            RoundDataProvider roundDataProvider, MonsterWaveSpawner monsterWaveSpawner, PoolProvider poolProvider)
        {
            this.gameSettings = gameSettings;
            this.roundRunner = roundRunner;
            this.roundDataProvider = roundDataProvider;
            this.monsterWaveSpawner = monsterWaveSpawner;
            this.poolProvider = poolProvider;
        }

        public void Initialize()
        {
            monsterWaveSpawner.Initialize(poolProvider);
            
        }
    }
}