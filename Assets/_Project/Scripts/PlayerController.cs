using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;
using Utilities;

namespace Platformer {
    public class PlayerController : ValidatedMonoBehaviour
    {
        [Header("References")]
        [SerializeField, Self] Rigidbody rb;
        [SerializeField, Self] GroundChecker groundCheck;
        [SerializeField, Self] WaterChecker waterChecker;
        [SerializeField, Self] Animator animator;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;
        [SerializeField, Anywhere] InputReader input;
        
        [Header("Movement Settings")]
        [SerializeField] float moveSpeed = 6f;
        [SerializeField] float rotationSpeed = 15f;
        [SerializeField] float smoothTime = 0.2f;
        
        [Header("Jump Settings")]
        [SerializeField] float jumpForce = 10f;
        [SerializeField] float jumpCooldown = 0f;
        [SerializeField] float jumpDuration = 0.1f;
        [SerializeField] float gravityMult = 3f;
        
        [Header("Attack Settings")]
        [SerializeField] float attackCooldown = 0.1f;
        
        [Header("Weapons Settings")]
        [SerializeField, Anywhere] GameObject sword;
        
        private bool isSwordVisible;
        [SerializeField, Anywhere] GameObject shield;
        private bool isShieldVisible;
        
        [Header("Health Settings")]
        [SerializeField] HealthBar healthBar;
        [SerializeField] int maxHealth = 100;
        [SerializeField] int currentHealth;
        
        [SerializeField] HeartRotator heartRotator;
        
        [Header("Sounds Settings")]
        [SerializeField] private AudioSource attackSFX;
        [SerializeField] private AudioSource deathSFX;
        
        [Header("Castle Settings")]
        [SerializeField] CastleManager castleManager;
        
        Transform mainCam;
        
        float currentSpeed;
        float velocity;
        float jumpVelocity;

        Vector3 movement;

        List<Timer> timers;
        CountdownTimer jumpTimer;
        CountdownTimer jumpCooldownTimer;
        
        CountdownTimer attackTimer;
        CountdownTimer defenseTimer;

        private bool IsRunning;
        
        private Vector3 originalPosition;
        

        
        static readonly int Speed = Animator.StringToHash("Speed");

        void Awake() {
            mainCam = Camera.main.transform;
            freeLookVCam.Follow = transform;
            freeLookVCam.LookAt = transform;
            
            freeLookVCam.OnTargetObjectWarped(transform, transform.position - freeLookVCam.transform.position - Vector3.forward);
            
            rb.freezeRotation = true;
            
            jumpTimer = new CountdownTimer(jumpDuration);
            jumpCooldownTimer = new CountdownTimer(jumpCooldown);
            
            jumpTimer.OnTimerStart += () => jumpVelocity = jumpForce;
            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
            
            attackTimer = new CountdownTimer(attackCooldown);
            defenseTimer = new CountdownTimer(attackCooldown);
            
            timers = new List<Timer>(4) {jumpTimer, jumpCooldownTimer, attackTimer, defenseTimer};

            jumpTimer.OnTimerStop += () => jumpCooldownTimer.Start();
            
            originalPosition = transform.position;
            
        }

        void Start() {
            input.EnablePlayerActions();
            currentHealth = maxHealth;
            healthBar.SetMaxHealth(maxHealth);
        }

        void Update() {
            movement = new Vector3(input.Direction.x, 0f, input.Direction.y);
            
            HandleTimers();
            UpdateAnimator();
            ResetPlayer();
            HandleShortcuts();

        }

        private void OnEnable() {
            input.Jump += OnJump;
            input.Attack += OnAttack;
            input.Defense += OnDefense;

        }
        
        private void OnDisable() {
            input.Jump -= OnJump;
            input.Attack -= OnAttack;
            input.Defense -= OnDefense;

        }

        void HandleShortcuts()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                rb.velocity = Vector3.zero;
                transform.position = originalPosition;
                
            }
            else if (Input.GetKey(KeyCode.Alpha2))
            {
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(4.91f, 10.33f, 27.34f);

            }
            else if (Input.GetKey(KeyCode.Alpha3))
            {
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(51.27f, 11.48f, 83.90f);
                
            }
            else if (Input.GetKey(KeyCode.Alpha4))
            {
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(97.21f, 11.55f, 97.61f);


            }
            else if (Input.GetKey(KeyCode.Alpha5))
            {
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(54.94f, 15.15f, 150.46f);


            }
            else if (Input.GetKey(KeyCode.Alpha6))
            {
                rb.velocity = Vector3.zero;
                transform.position = new Vector3(54.98f, 16.46f, 218.98f);


            }
        }
        
        
        void OnAttack() {
            if (!attackTimer.IsRunning) {
                attackTimer.Start();
            }
            else {
                attackTimer.Stop();
            }
        }
        
        void OnDefense() {
            if (!defenseTimer.IsRunning) {
                defenseTimer.Start();
            }
            else {
                defenseTimer.Stop();
            }
        }

        public void TakeDamage(String type) {
            if(type == "Enemy")
            {
                currentHealth -= 10;
            }
            else if(type == "BossSword")
            {
                if(isShieldVisible)
                {
                    currentHealth -= 20;
                }
                else
                {
                    currentHealth -= 25;
                }
                
            }
            healthBar.SetHealth(currentHealth);
        }

        public void AddHealth()
        {
            originalPosition = new Vector3(54.98f, 16.46f, 218.98f) ;
            if (currentHealth < maxHealth)
            {
                currentHealth = maxHealth;
                healthBar.SetHealth(currentHealth);
            }
        }
        
        public void ToggleSwordVisibility()
        {
            isSwordVisible = !isSwordVisible;
            sword.SetActive(isSwordVisible);
        }
        
        public void ToggleShieldVisibility()
        {
            isShieldVisible = !isShieldVisible;
            shield.SetActive(isShieldVisible);
        }
        
        void OnJump(bool isJumping) {
            if (isJumping && !jumpTimer.IsRunning && !jumpCooldownTimer.IsRunning && groundCheck.IsGrounded) {
                jumpTimer.Start();
            }
            else if (!isJumping && jumpTimer.IsRunning) {
                jumpTimer.Stop();
                
            }
        }
        
        void ResetPlayer() {
            if (waterChecker.IsInWater || currentHealth <= 0) {
                deathSFX.Play();
                transform.position = originalPosition;
                currentHealth = maxHealth;
                healthBar.SetHealth(currentHealth);
                heartRotator.SetHeartActive();
                castleManager.OpenDoors();


            }
        }
        

        private void FixedUpdate() {
            HandleJump();
            HandleMovement();
            HandleAttack();
            HandleDefense();
            HandleRunning();
        }

        void UpdateAnimator() {
            animator.SetFloat(Speed, currentSpeed);
        }
        
        public void HandleAttack() {
            
            if(attackTimer.IsRunning) {
                if (isSwordVisible)
                {
                    attackSFX.Play();

                }
                animator.SetBool("Attack", true);
            }
            else {
                animator.SetBool("Attack", false);
            }
        }
        
        public void HandleDefense() {
            
            if(defenseTimer.IsRunning && isShieldVisible) {
                animator.SetBool("Defense", true);
            }
            else {
                animator.SetBool("Defense", false);
            }
        }
        
        public void HandleRunning() {
            
            if(Input.GetKey(KeyCode.LeftShift) && movement.magnitude > 0F) {
                animator.SetBool("IsMoving", true);
                IsRunning = true;
            }
            else {
                animator.SetBool("IsMoving", false);
                IsRunning = false;
            }

        }
        
        
        void HandleTimers() {
            foreach (var t in timers) {
                t.Tick(Time.deltaTime);
            }
        }
        
         void HandleJump() {
            if (!jumpTimer.IsRunning && groundCheck.IsGrounded) {
                jumpVelocity = 0f;
                return;
            }
            
            if (!jumpTimer.IsRunning) {
                jumpVelocity += Physics.gravity.y * gravityMult * Time.fixedDeltaTime;
            }
            
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
        
        void HandleMovement() {
            
            var adjustedDirection = Quaternion.AngleAxis(mainCam.eulerAngles.y, Vector3.up) * movement;
            
            if (adjustedDirection.magnitude > 0F) {
                HandleRotation(adjustedDirection);
                HandleHorizontalMovement(adjustedDirection);
                SmoothSpeed(adjustedDirection.magnitude);
            } else {
                SmoothSpeed(0F);
                
                rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            }
        }

        void HandleHorizontalMovement(Vector3 adjustedDirection)
        {   
            float runVar = IsRunning ? 1.3f : 1f;
            Vector3 velocity = adjustedDirection * (runVar * moveSpeed  * Time.fixedDeltaTime);
            rb.velocity = new Vector3(velocity.x, rb.velocity.y, velocity.z);
        }

        void HandleRotation(Vector3 adjustedDirection) {
            var targetRotation = Quaternion.LookRotation(adjustedDirection);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            
            transform.LookAt(transform.position + adjustedDirection);
        }

        void SmoothSpeed(float value) {
            currentSpeed = Mathf.SmoothDamp(currentSpeed, value, ref velocity, smoothTime);
        }
    }
}