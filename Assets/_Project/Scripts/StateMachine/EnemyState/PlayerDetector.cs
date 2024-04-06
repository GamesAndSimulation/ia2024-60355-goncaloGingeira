using UnityEngine;
using Utilities;

namespace Platformer {
    public class PlayerDetector : MonoBehaviour {
        [SerializeField] float detectionAngle = 60f; 
        [SerializeField] float detectionRadius = 7; 
        [SerializeField] float innerDetectionRadius = 2f; 
        [SerializeField] float detectionCooldown = 1f; 
        [SerializeField] float attackRange = 2f; 
        
        public Transform Player { get; private set; }
        
        CountdownTimer detectionTimer;
        
        IDetectionStrategy detectionStrategy;

        void Awake() {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Start() {
            detectionTimer = new CountdownTimer(detectionCooldown);
            detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
        }
        
        void Update() => detectionTimer.Tick(Time.deltaTime);

        public bool CanDetectPlayer() {
            return detectionTimer.IsRunning || detectionStrategy.Execute(Player, transform, detectionTimer);
        }

        public bool CanAttackPlayer() {
            var directionToPlayer = Player.position - transform.position;
            return directionToPlayer.magnitude <= attackRange;
        }
        
    }
}