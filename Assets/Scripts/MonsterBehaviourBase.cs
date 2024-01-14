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
        private DynamicData dynamicData;
        private BoundaryData boundaryData;

        private Vector2 boundaryMarker;
        private float minimumDistanceToBoundary; 

        public MonsterData MonsterData => monsterData; 
        public void Initialize(MonsterData monsterData, DynamicData dynamicData, BoundaryData boundaryData,
            SimpleScriptableEvent onStartMonsterMoveEvent, OnMonsterStopMoveScriptableEvent onMonsterStopEvent)
        {
            this.monsterData = monsterData;
            this.dynamicData = dynamicData;
            this.boundaryData = boundaryData; 
            this.onStartMonsterMoveEvent = onStartMonsterMoveEvent;
            this.onMonsterStopEvent = onMonsterStopEvent; 
            try
            {
               this.onStartMonsterMoveEvent.AddListener((s) => OnStartEventRunHandler());
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
        // private void OnTriggerExit2D(Collider2D other)
        // {
        //     if (other.TryGetComponent<IBounds>(out var bounds))
        //     {
        //         EndRunSequence();
        //     }
        // }
        
        public void TakeDamage(int damage)
        {
            throw new System.NotImplementedException();
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }
        

        private void OnStartEventRunHandler()
        {
            try
            {
                StartMove(dynamicData.Direction,dynamicData.Speed);
            }
            catch (Exception e)
            {
                Console.WriteLine("On Start Run Handler: " + e);
                throw;
            }
        }
        
        private async void EndRunSequence()
        {
            Stop();
            int secondsToWait = GameManager.Instance.GameSettings.DestroyMonsterInSeconds;
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

