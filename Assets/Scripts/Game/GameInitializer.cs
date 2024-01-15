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
        private DataConfigurations dataConfigurations; 
        public GameInitializer(GameSettings gameSettings, RoundRunner roundRunner,
            RoundDataProvider roundDataProvider, MonsterWaveSpawner monsterWaveSpawner, 
            PoolProvider poolProvider, DataConfigurations dataConfigurations)
        {
            this.gameSettings = gameSettings;
            this.roundRunner = roundRunner;
            this.roundDataProvider = roundDataProvider;
            this.monsterWaveSpawner = monsterWaveSpawner;
            this.poolProvider = poolProvider;
            this.dataConfigurations = dataConfigurations; 
        }

        public void Initialize()
        {
            monsterWaveSpawner.Initialize(poolProvider, dataConfigurations.MonsterSpawnerDataConfig,
                dataConfigurations.RoundDataConfig);
        }
    }
}