using UnityEngine;
using UnityEngine.AI;

namespace Platformer {
    public class EnemyAttackState : EnemyBaseState {
        readonly NavMeshAgent agent;
        readonly Transform player;
        readonly Enemy enemy;
        
        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator) {
            this.agent = agent;
            this.player = player;
            this.enemy = enemy;
        }
        
        public override void OnEnter() {
            animator.CrossFade(AttackHash, crossFadeTime);
        }
        
        public override void Update() {
            agent.SetDestination(player.position);
            enemy.Attack();
        }
    }
}