using UnityEngine;

namespace MonsterRun.Monster
{
    public class DynamicData
    {
        private Vector2 direction;
        private float speed;

        public Vector2 Direction => direction;
        public float Speed => speed; 
        
        public DynamicData(Vector2 direction, float speed)
        {
            this.direction = direction;
            this.speed = speed; 
        }
    }
}