using CHARK.ScriptableEvents.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace MonsterRun.Monster
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/Monster Data")]
    public class MonsterData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private MonsterBehaviourBase monsterBehaviour; 
        
        
        public int ID => id;
        public MonsterType MonsterType => monsterType;
        public MonsterBehaviourBase MonsterBehaviour => monsterBehaviour;
    }

    public enum MonsterType
    {
        TypeOne,
        TypeTwo
    }
}

