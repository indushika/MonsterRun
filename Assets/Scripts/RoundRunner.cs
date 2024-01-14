using System;
using CHARK.ScriptableEvents;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Main;
using MonsterRun.Monster;

namespace MonsterRun.Round
{
    public class RoundRunner
    {
        private RoundDataConfig _currentRoundDataConfig;
        
        private RoundDataProvider roundDataProvider;
        private MonsterWaveSpawner monsterWaveSpawner;
        private GameSettings gameSettings;
        private SimpleScriptableEvent onRoundStartEvent;
        private SimpleScriptableEvent onRoundEndEvent; 
        private SimpleScriptableEvent onEndRoundEvent; 
        

        public RoundRunner(GameSettings gameSettings, RoundDataProvider roundDataProvider,
            MonsterWaveSpawner monsterWaveSpawner, SimpleScriptableEvent onRoundStartEvent, 
            SimpleScriptableEvent onRoundEndEvent, SimpleScriptableEvent onEndRoundEvent)
        {
            this.roundDataProvider = roundDataProvider;
            this.monsterWaveSpawner = monsterWaveSpawner;
            this.gameSettings = gameSettings;

            this.onRoundStartEvent = onRoundStartEvent;
            this.onRoundEndEvent = onRoundEndEvent;
            this.onEndRoundEvent = onEndRoundEvent;

            try
            {
                onEndRoundEvent.AddListener((s) => EndRound());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        
        public void StartNewRound()
        {
            SetRoundData(gameSettings.RoundCount);
            StartRound();
        }

        public void EndRound()
        {
            onRoundEndEvent.Raise();
            roundDataProvider.UpdateRoundCount(gameSettings.RoundCount);
        }


        private void StartRound()
        {
            onRoundStartEvent.Raise();
            monsterWaveSpawner.SpawnMonstersForRound(_currentRoundDataConfig);
            monsterWaveSpawner.StartMonsterRun();
            
        }
        private void SetRoundData(uint roundCount)
        {
            _currentRoundDataConfig = roundDataProvider.GetNewRoundData(roundCount); 
        }
    }
}