using System;
using System.Collections;
using System.Collections.Generic;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;
using Utilities;

namespace Platformer
{   
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(PlayerDetector))]
    public class Enemy : MonoBehaviour
    {   
        
        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Child] Animator animator;
        [SerializeField, Self] PlayerDetector playerDetector;
        [SerializeField] float wanderRadius = 10f;
        [SerializeField] float timerAttacks = 1f;
        [SerializeField] public float health = 50f;

        private StateMachine stateMachine;
        CountdownTimer attackTimer;

        void OnValidate() => this.ValidateRefs();

        void Start()
        {   
            attackTimer = new CountdownTimer(timerAttacks);
            stateMachine = new StateMachine();
            
            var wanderState = new EnemyWanderState(this, animator, agent, wanderRadius);
            var chaseState = new EnemyChaseState(this, animator, agent, playerDetector.Player);
            var attackState = new EnemyAttackState(this, animator, agent, playerDetector.Player);
            var dieState = new EnemyDieState(this, animator, agent);

            
            At(wanderState, chaseState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
            At(chaseState, wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
            At(chaseState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
            At(attackState, chaseState, new FuncPredicate(() => !playerDetector.CanAttackPlayer()));
            At(chaseState, dieState, new FuncPredicate(() => health <= 0));
            
            stateMachine.SetState(wanderState);
        }
        
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        
        
        void Update()
        {
            stateMachine.Update();
            attackTimer.Tick(Time.deltaTime);
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        
        public void Attack()
        {   
            if (attackTimer.IsRunning) return;
            attackTimer.Start();
        }
        
        public void TakeDamage(float damage)
        {
            health -= damage;
            
        }
        
        public void Die()
        {
            Destroy(gameObject);
        }

        
    }
    
}
