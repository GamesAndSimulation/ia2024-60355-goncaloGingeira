using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class PrisonerMoveState : PrisonerBaseState
    {
        
        readonly NavMeshAgent agent;
        readonly Transform character;
        
        public PrisonerMoveState(Prisoner prisoner, Animator animator, NavMeshAgent agent, Transform character) : base(prisoner, animator)
        {
            this.agent = agent;
            this.character = character;
        }
        
        public override void OnEnter()
        {
            animator.CrossFade(RunHash, crossFadeTime);
        }
        
        public override void Update()
        {
            agent.SetDestination(character.position + character.forward * 2f);
            
        }
        
    }
}