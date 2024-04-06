using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class PrisonerIdleState : PrisonerBaseState
    {
        readonly Animator animator;
        public PrisonerIdleState(Prisoner prisoner, Animator animator) : base(prisoner, animator)
        {
            this.animator = animator;
        }
        
        public override void OnEnter()
        {
            animator.CrossFade(IdleHash, crossFadeTime);
        }
        
        public override void Update()
        {
        }
        
    }
}