using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public abstract class PrisonerBaseState : IState
    {
        protected readonly Prisoner prisoner;
        protected readonly Animator animator;
        
        protected static readonly int IdleHash = Animator.StringToHash("IdleNormal");
        protected static readonly int RunHash = Animator.StringToHash("MoveFWD");
        
        protected const float crossFadeTime = 0.1f;
            
        protected PrisonerBaseState(Prisoner prisoner, Animator animator)
        {
            this.prisoner = prisoner;
            this.animator = animator;
        }

        public virtual void OnEnter()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnExit()
        {
        }

        public virtual void Update()
        {
            
        }
    }
}