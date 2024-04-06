﻿using System.Collections;
using Cinemachine;
using KBCore.Refs;
using UnityEngine;

namespace Platformer {
    public class CameraManager : ValidatedMonoBehaviour {
        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField, Anywhere] CinemachineFreeLook freeLookVCam;

        [Header("Settings")] 
        [SerializeField, Range(0.5f, 3f)] float speedMultiplier = 1f;
        
        bool isRMBPressed;
        bool cameraMovementLock;

        void OnEnable() {
            input.Look += OnLook;
            input.EnableMouseControlCamera += OnEnableMouseControlCamera;
            input.DisableMouseControlCamera += OnDisableMouseControlCamera;
        }
        
        void OnDisable() {
            input.Look -= OnLook;
            input.EnableMouseControlCamera -= OnEnableMouseControlCamera;
            input.DisableMouseControlCamera -= OnDisableMouseControlCamera;
        }

        void OnLook(Vector2 cameraMovement, bool isDeviceMouse) {
            if (cameraMovementLock) return;
            
            if (isDeviceMouse && !isRMBPressed) return;

            float deviceMultiplier = isDeviceMouse ? Time.fixedDeltaTime : Time.deltaTime;
            
            freeLookVCam.m_XAxis.m_InputAxisValue = cameraMovement.x * speedMultiplier * deviceMultiplier;
            freeLookVCam.m_YAxis.m_InputAxisValue = cameraMovement.y * speedMultiplier * deviceMultiplier;
        }

        void OnEnableMouseControlCamera() {
            isRMBPressed = true;
            
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            
            StartCoroutine(DisableMouseForFrame());
        }

        void OnDisableMouseControlCamera() {
            isRMBPressed = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
            freeLookVCam.m_XAxis.m_InputAxisValue = 0f;
            freeLookVCam.m_YAxis.m_InputAxisValue = 0f;
        }

        IEnumerator DisableMouseForFrame() {
            cameraMovementLock = true;
            yield return new WaitForEndOfFrame();
            cameraMovementLock = false;
        }

    }
}