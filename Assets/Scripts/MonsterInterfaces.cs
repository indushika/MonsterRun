using System;
using UnityEngine;
using UnityEngine.Pool;

namespace MonsterRun
{
    public interface IDamageable
    {
        public void TakeDamage(int damage); 
    }

    public interface IInteractable
    {
        public void Interact(); 
    }

    public interface IMovement
    {
        public void StartMove(Vector3 direction, float speed); 
        public void Move(Vector3 direction, float speed, float? timeStep);
        public void Stop(); 
    }

    public interface ICharacterAnimator
    {
        
    }

    public interface IPoolable
    {
        public Type GetPooledType(); 
    }
}

