using System;
using System.Collections.Generic;
using CHARK.ScriptableEvents.Events;
using DefaultNamespace;
using MonsterRun.Main;
using MonsterRun.Round;
using ScriptableEvents.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace MonsterRun.Monster
{
    public class MonsterWaveSpawner : MonoBehaviour
    {
        private MonsterSpawnerDataConfig config;

        private List<MonsterData> monsters; 
        private SimpleScriptableEvent onStartMonsterMoveEvent;
        private OnMonsterStopMoveScriptableEvent onMonsterStopEvent;
        private SimpleScriptableEvent onEndRoundEvent;
        private OnRoundStartScriptableEvent onRoundStartEvent; 

        private IPoolProvider poolProvider;
        private MonsterPoolProvider monsterPoolProvider;
        public MonsterPoolProvider MonsterPoolProvider => monsterPoolProvider; 

        #region API

        public void Initialize(IPoolProvider poolProvider, MonsterSpawnerDataConfig monsterSpawnerDataConfig, 
            RoundDataConfig roundDataConfig)
        {
            this.poolProvider = poolProvider;
            config = monsterSpawnerDataConfig; 
            monsterPoolProvider = new MonsterPoolProvider(this.poolProvider);

            onStartMonsterMoveEvent = config.OnStartMonsterMoveEvent;
            onMonsterStopEvent = config.OnMonsterStopMoveEvent;
            onEndRoundEvent = roundDataConfig.OnEndRoundEvent;
            onRoundStartEvent = roundDataConfig.OnRoundStartEvent; 
            
            try
            {
                monsters = config.Monsters; 
                onRoundStartEvent.AddListener((eventArgs) => OnRoundStartEventHandler(eventArgs));
                onMonsterStopEvent.AddListener((eventArgs) => OnRunEndEventHandler(eventArgs));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        #endregion
        
        #region Event Handlers

        private void OnRoundStartEventHandler(OnRoundStartEventArgs args)
        {
            RoundDataConfig roundDataConfig = args.RoundDataConfig; 
            var boundaryData = GetNewBoundaryData(); 
            if (poolProvider != null)
            {
                int spawnCount = roundDataConfig.MonsterSpawnCount;

                for (int i = 0; i < spawnCount; i++)
                {
                    var monsterData = GetRandomMonster();
                    var monster = monsterPoolProvider.GetPoolByType(monsterData).Get(); 
                    if (monster)
                    {
                        monster.Initialize(monsterData,GetNewBehaviourData(), boundaryData,onStartMonsterMoveEvent,onMonsterStopEvent);
                    }
                }

                config.SpawnedMonsterCount = spawnCount;
                config.ActiveMonsterCount = spawnCount;
            }
            else
            {
                Debug.LogError("Pool Provider is missing.");
            }
            
            StartMonsterRun();
        }
        private void OnRunEndEventHandler(OnMonsterStopMoveEventArgs args)
        {
            var pool = monsterPoolProvider.GetPoolByType(args.Monster.MonsterData); 
            pool.Release(args.Monster);
            config.ActiveMonsterCount -= 1;
            
            if (config.ActiveMonsterCount == 0 && onEndRoundEvent)
            {
                onEndRoundEvent.Raise();
            }
        }
        
        //Handle Event when game stop before monster complete run 
        #endregion

        #region Implementation

        private void StartMonsterRun()
        {
            try
            {
                onStartMonsterMoveEvent.Raise();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private BehaviourData GetNewBehaviourData()
        {
            try
            {
                float speed = Random.Range(config.MinSpeed, config.MaxSpeed);
                return new BehaviourData(config.Direction, speed, config.DestroyMonsterInSeconds);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private BoundaryData GetNewBoundaryData()
        {
            try
            {
                return new BoundaryData(config.ScreenBoundaryTransform.position, config.MinimumDistanceToBoundary);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        private MonsterData GetRandomMonster()
        {
            int rand = Random.Range(0, monsters.Count);
            return monsters[rand]; 
        }
     
        
        #endregion
        
    }
}