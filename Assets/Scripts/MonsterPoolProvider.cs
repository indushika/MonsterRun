using System;
using System.Collections.Generic;
using MonsterRun;
using MonsterRun.Monster;
using UnityEngine;
using UnityEngine.Pool;

namespace MonsterRun.Monster
{
    public class MonsterPoolProvider
    {
        private IPoolProvider poolProvider;
        private Dictionary<MonsterType, IObjectPool<MonsterBehaviourBase>> monsterPools; 
        
        public MonsterPoolProvider(IPoolProvider poolProvider)
        {
            this.poolProvider = poolProvider;
            monsterPools = new Dictionary<MonsterType, IObjectPool<MonsterBehaviourBase>>(); 
        }

        public IObjectPool<MonsterBehaviourBase> GetPoolByType(MonsterData monsterData)
        {
            var monsterType = monsterData.MonsterType; 
            if (monsterPools.TryGetValue(monsterType, out IObjectPool<MonsterBehaviourBase> monsterPool))
            {
                return monsterPool; 
            }

            try
            {
                var newMonsterPool = poolProvider.GetPool(() =>
                {
                    return GameObject.Instantiate(monsterData.MonsterBehaviour); 
                }, OnTakeFromPool, OnReturnedToPool, OnDestroyPoolObject); 
                monsterPools.Add(monsterType,newMonsterPool);
                return newMonsterPool;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        
        void OnReturnedToPool(MonsterBehaviourBase monster)
        {
            if (monster)
            {
                monster.gameObject.SetActive(false);
            }
        }

        void OnTakeFromPool(MonsterBehaviourBase monster)
        {
            if (monster)
            {
                monster.gameObject.SetActive(true);
            }
        }

        void OnDestroyPoolObject(MonsterBehaviourBase monster)
        {
            GameObject.Destroy(monster.gameObject);
        }
        
    }
}