using UnityEngine;

namespace MonsterRun.Monster
{
    public class BehaviourData
    {
        private Vector2 direction;
        private float speed;
        private int waitTimeForSelfDestruct; 
        
        public Vector2 Direction => direction;
        public float Speed => speed;
        public int WaitTimeForSeldDestruct => waitTimeForSelfDestruct; 
        
        public BehaviourData(Vector2 direction, float speed, int waitTimeForSelfDestruct)
        {
            this.direction = direction;
            this.speed = speed;
            this.waitTimeForSelfDestruct = waitTimeForSelfDestruct; 
        }
    }
}