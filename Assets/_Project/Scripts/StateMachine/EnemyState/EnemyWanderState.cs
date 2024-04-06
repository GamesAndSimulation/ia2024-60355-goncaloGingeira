using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer
{
    public class EnemyWanderState : EnemyBaseState
    {   
        
        readonly NavMeshAgent agent;
        readonly Vector3 startPosition;
        readonly float wonderRadius;
        
        public EnemyWanderState(Enemy enemy, Animator animator, NavMeshAgent agent, float wonderRadius) : base(enemy, animator)
        {
            this.agent = agent;
            startPosition = enemy.transform.position;
            this.wonderRadius = wonderRadius;
        }
        
        public override void OnEnter()
        {
            animator.CrossFade(WalkHash, crossFadeTime);
        }

        public override void Update()
        {
            if(ReachedDestination())
            {
                var randomDirection = Random.insideUnitSphere * wonderRadius;
                randomDirection += startPosition;
                NavMeshHit hit;
                NavMesh.SamplePosition(randomDirection, out hit, wonderRadius, 1);
                var finalPosition = hit.position;
                agent.SetDestination(finalPosition);
            }
        }
        
        bool ReachedDestination()
        {
            return !agent.pathPending
            && agent.remainingDistance <= agent.stoppingDistance
            && (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
        }

    }
    
}
