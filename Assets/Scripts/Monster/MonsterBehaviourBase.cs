using System;
using System.Diagnostics;
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

        private Camera mainCamera;
        private Vector2 screenPostion;
        private float xScreenBoundary; 
        private float yScreenBoundary; 

        public MonsterData MonsterData => monsterData; 
        public void Initialize(MonsterData monsterData, BehaviourData behaviourData, 
            SimpleScriptableEvent onStartMonsterMoveEvent, OnMonsterStopMoveScriptableEvent onMonsterStopEvent,
            Camera mainCamera, Vector2 screenPositionWeight)
        {
            this.monsterData = monsterData;
            this.behaviourData = behaviourData;
            this.onStartMonsterMoveEvent = onStartMonsterMoveEvent;
            this.onMonsterStopEvent = onMonsterStopEvent;
            this.mainCamera = mainCamera; 
            try
            {
               this.onStartMonsterMoveEvent.AddListener((s) => OnStartRunEventHandler());
               SetPositionByScreenPoint(screenPositionWeight);
               SetBoundary(screenPositionWeight);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void SetBoundary(Vector2 screenPositionWeight)
        {
            var pixelWidth = mainCamera.pixelWidth;
            xScreenBoundary = pixelWidth + pixelWidth * screenPositionWeight.x;
            var pixelHeight = mainCamera.pixelHeight;
            yScreenBoundary = pixelHeight + pixelHeight * screenPositionWeight.y; 
        }

        private void Update()
        {
            if (characterMoveFlag)
            {
                Move(direction, speed, null);
                if (mainCamera)
                {
                    var screenPosition = mainCamera.WorldToScreenPoint(characterTransform.position); 
                    if (screenPosition.x < 0 || screenPosition.x > xScreenBoundary 
                        || screenPosition.y < 0 || screenPosition.y > yScreenBoundary)
                    {
                        EndRunSequence();
                    }
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

        private void SetPositionByScreenPoint(Vector2 screenPositionWeight)
        {
            var screenPosition = new Vector2(mainCamera.pixelWidth*screenPositionWeight.x,mainCamera.pixelHeight*screenPositionWeight.y);
            var position = mainCamera.ScreenToWorldPoint(screenPosition); 
            SetPosition(position);
        }


    }
    
    
}

