using System;
using System.Threading.Tasks;
using CHARK.ScriptableEvents.Events;
using MonsterRun.Main;
using ScriptableEvents.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace MonsterRun.Monster
{
    public abstract class MonsterBehaviourBase  : GameCharacterMovement, IDamageable, ICharacterAnimator, 
        IInteractable, IPoolable
    {
        private SimpleScriptableEvent onStartMonsterMoveEvent;
        private OnMonsterStopMoveScriptableEvent onMonsterStopEvent; 
        private MonsterData monsterData;
        private BehaviourData behaviourData;
        private BoundaryData boundaryData;

        private Vector2 boundaryMarker;
        private float minimumDistanceToBoundary; 
        

        public MonsterData MonsterData => monsterData; 
        public void Initialize(MonsterData monsterData, BehaviourData behaviourData, BoundaryData boundaryData,
            SimpleScriptableEvent onStartMonsterMoveEvent, OnMonsterStopMoveScriptableEvent onMonsterStopEvent )
        {
            this.monsterData = monsterData;
            this.behaviourData = behaviourData;
            this.boundaryData = boundaryData; 
            this.onStartMonsterMoveEvent = onStartMonsterMoveEvent;
            this.onMonsterStopEvent = onMonsterStopEvent; 
            try
            {
               this.onStartMonsterMoveEvent.AddListener((s) => OnStartRunEventHandler());
               SetPosition(monsterData.InitialPosition);
               SetBoundary();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
        
        private void Update()
        {
            if (characterMoveFlag)
            {
                Move(direction, speed, null);
                if (MathF.Abs(boundaryMarker.x - characterTransform.position.x) < minimumDistanceToBoundary)
                {
                    EndRunSequence();
                }
            }
        }
        
        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }
        

        private void OnStartRunEventHandler()
        {
            try
            {
                StartMove(behaviourData.Direction,behaviourData.Speed);
            }
            catch (Exception e)
            {
                Console.WriteLine("On Start Run Handler: " + e);
                throw;
            }
        }

        /// <summary>
        /// on stopping monster behaviour externally 
        /// </summary>
        private void OnStopRunEventHandler()
        {
            EndRunSequence();
        }
        
        private async void EndRunSequence()
        {
            Stop();
            int secondsToWait = GameManager.Instance.DataConfigurations.MonsterSpawnerDataConfig.DestroyMonsterInSeconds;
            await Task.Delay(secondsToWait*1000);

            var eventArgs = new OnMonsterStopMoveEventArgs()
            {
                Monster = this
            };
            onMonsterStopEvent.Raise(eventArgs);
        }

        public Type GetPooledType()
        {
            return typeof(This); 
        }

        private void SetBoundary()
        {
            boundaryMarker = boundaryData.BoundaryMarkerPosition;
            minimumDistanceToBoundary = boundaryData.MinimumDistanceToBoundary; 
        }
    }
    
    
}

