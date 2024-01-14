using System;
using MonsterRun;
using UnityEngine;

namespace MonsterRun
{
    public abstract class GameCharacterMovement : MonoBehaviour, IMovement
    {
        [SerializeField] protected Transform characterTransform;
        protected Vector3 direction;
        protected float speed;
        protected bool characterMoveFlag; 


        public void StartMove(Vector3 direction, float speed)
        {
            this.direction = direction;
            this.speed = speed;
            characterMoveFlag = true; 
        }

        public void Move(Vector3 direction, float speed, float? timeStep)
        {
            var characterPostion = characterTransform.position;
            if (timeStep == null)
            {
                timeStep = Time.fixedDeltaTime;
            }
            var targetPosition = characterPostion + (direction * speed);
            characterTransform.position = Vector3.Lerp(characterTransform.position, targetPosition, timeStep.Value);
        }

        public void Stop()
        {
            characterMoveFlag = false; 
        }

        public void SetPosition(Vector2 position)
        {
            characterTransform.position = position; 
        }
    }
}