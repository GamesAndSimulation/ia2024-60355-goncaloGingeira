using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer {
    public class EnemyDieState : EnemyBaseState {
        readonly NavMeshAgent agent;
        readonly Enemy enemy;
        readonly float dieDelay = 2.5f;
        
        public EnemyDieState(Enemy enemy, Animator animator, NavMeshAgent agent) : base(enemy, animator) {
            this.agent = agent;
            this.enemy = enemy;
        }
        
        public override void OnEnter() {
            animator.CrossFade(DeathHash, crossFadeTime);
            enemy.StartCoroutine(DieWithDelay());
        }
        
        IEnumerator DieWithDelay() {
            yield return new WaitForSeconds(dieDelay);
            enemy.Die();
        }
        
        public override void Update() {
        }
    }
}