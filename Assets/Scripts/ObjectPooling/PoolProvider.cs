using System;
using System.Collections.Generic;
using MonsterRun.Monster;
using UnityEngine;
using UnityEngine.Pool;

namespace MonsterRun
{
    public class PoolProvider : IPoolProvider
    {
        private Dictionary<Type, IObjectPool<IPoolable>> poolCollection = new Dictionary<Type, IObjectPool<IPoolable>>();
        
        
        public IObjectPool<TT> GetPool<TT>(Func<TT> onCreatePoolObject, Action<TT> onGetFromPool = null, 
            Action<TT> onReturnToPool = null, Action<TT> onDestroyFromPool = null,
            bool collectionCheck = true, int maxPoolSize = 10000) where TT : class, IPoolable
        {
            // if (poolCollection.TryGetValue(typeof(TT),out IObjectPool<IPoolable> pool))
            // {
            //     // if (pool is IObjectPool<TT> typeObjectPool)
            //     // {
            //     IObjectPool<TT> objectPool = pool; 
            //         return pool as IObjectPool<TT>; 
            //     // }
            // }
            //
            var newTypeObjectPool = new ObjectPool<TT>(onCreatePoolObject, onGetFromPool, onReturnToPool, 
                onDestroyFromPool, collectionCheck, 10, maxPoolSize); 
            
            // poolCollection.Add(typeof(TT),newTypeObjectPool as IObjectPool<IPoolable>);
            return newTypeObjectPool; 
        }
    }

    public interface IPoolProvider
    {
        public IObjectPool<TT> GetPool<TT>(Func<TT> onCreatePoolObject,
            Action<TT> onGetFromPool = null, Action<TT> onReturnToPool = null, 
            Action<TT> onDestroyFromPool = null, bool collectionCheck = true, int maxPoolSize = 10000) where TT : class, IPoolable; 
    }
    
}