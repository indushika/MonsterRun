using CHARK.ScriptableEvents.Events;
using UnityEngine;

namespace MonsterRun.Monster
{
    [CreateAssetMenu(fileName = "MonsterData", menuName = "Scriptable Objects/Monster Data")]
    public class MonsterData : ScriptableObject
    {
        [SerializeField] private int id;
        [SerializeField] private MonsterType monsterType;
        [SerializeField] private MonsterBehaviourBase monsterBehaviour;
        [SerializeField] private Vector2 initialPosition; 
        
        
        public int ID => id;
        public MonsterType MonsterType => monsterType;
        public MonsterBehaviourBase MonsterBehaviour => monsterBehaviour;
        public Vector2 InitialPosition => initialPosition; 
    }

    public enum MonsterType
    {
        TypeOne,
        TypeTwo
    }
}

