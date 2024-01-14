using MonsterRun;
using UnityEngine;
using UnityEngine.Pool;

namespace MonsterRun
{
    public abstract class ObjectPoolProviderBase : ScriptableObject
    {
        public abstract T GetFromPool<T>(T objectType);
        public abstract IObjectPool<IPoolable> GetPool(); 
    }
}