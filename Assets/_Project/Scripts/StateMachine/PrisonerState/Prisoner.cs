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
    public class Prisoner : MonoBehaviour
    {   
        
        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Child] Animator animator;
        [SerializeField] CastleManager castleManager;
        [SerializeField] GameObject character;
        [SerializeField] GameObject place;

        private StateMachine stateMachine;

        void OnValidate() => this.ValidateRefs();

        void Start()
        {   
            stateMachine = new StateMachine();
            var idleState = new PrisonerIdleState(this, animator);
            var moveState = new PrisonerMoveState(this, animator, agent, character.transform);
            
            At(idleState, moveState, new FuncPredicate(() => castleManager.canReleasePrisoner));
            At(moveState, idleState, new FuncPredicate(() => agent.remainingDistance <= agent.stoppingDistance));

            
            stateMachine.SetState(idleState);
        }
        
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);
        
        
        void Update()
        {
            stateMachine.Update();
        }

        void FixedUpdate()
        {
            stateMachine.FixedUpdate();
        }
        
        
    }
    
}
