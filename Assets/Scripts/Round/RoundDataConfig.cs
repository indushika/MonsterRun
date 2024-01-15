using CHARK.ScriptableEvents.Events;
using ScriptableEvents.Events;
using UnityEngine;

namespace MonsterRun.Round
{
    [CreateAssetMenu(fileName = "RoundDataConfig",
        menuName = "Scriptable Objects/Data Configurations/ Round Data Config")]
    public class RoundDataConfig : ScriptableObject
    {
        [SerializeField] private uint roundNumber;
        [SerializeField] private int timeBetweenRounds;

        [SerializeField] private OnRoundStartScriptableEvent onRoundStartEvent;
        [SerializeField] private SimpleScriptableEvent onRoundEndEvent;
        [SerializeField] private SimpleScriptableEvent onEndRoundEvent;

        private int monsterSpawnCount;

        public uint RoundNumber => roundNumber;
        public int TimeBetweenRounds => timeBetweenRounds;
        public int MonsterSpawnCount => monsterSpawnCount;
        public OnRoundStartScriptableEvent OnRoundStartEvent => onRoundStartEvent;
        public SimpleScriptableEvent OnRoundEndEvent => onRoundEndEvent;
        public SimpleScriptableEvent OnEndRoundEvent => onEndRoundEvent; 

        public void SetRoundNumber(uint roundNumber)
        {
            this.roundNumber = roundNumber;
        }

        public void SetMonsterSpawnCount(int monsterSpawnCount)
        {
            this.monsterSpawnCount = monsterSpawnCount;
        }
    }
}