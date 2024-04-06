using UnityEngine.InputSystem;
using static PlayerInputActions;
using UnityEngine;
using UnityEngine.Events;


namespace Platformer {

    [CreateAssetMenu(fileName = "InputReader", menuName = "Platformer/InputReader")]
   
    public class InputReader : ScriptableObject, IPlayerActions {

        public event UnityAction<Vector2> Move = delegate { };
        public event UnityAction<Vector2, bool> Look = delegate { };
        public event UnityAction EnableMouseControlCamera = delegate { };
        public event UnityAction DisableMouseControlCamera = delegate { };
        public event UnityAction<bool> Jump = delegate { };
        public event UnityAction Attack = delegate { };
        public event UnityAction Defense = delegate { };


        PlayerInputActions inputActions;

        public Vector3 Direction => inputActions.Player.Move.ReadValue<Vector2>();

        void OnEnable() {
            if (inputActions == null) {
                inputActions = new PlayerInputActions();
                inputActions.Player.SetCallbacks(this);
            }
        }

        public void EnablePlayerActions() {
            inputActions.Enable();
        }

        public void OnMove(InputAction.CallbackContext context) {
            Move.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context) {
            Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
        }

        bool IsDeviceMouse(InputAction.CallbackContext context) => context.control.device.name == "Mouse";


        public void OnFire(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Started) {
                Attack.Invoke();
            }
        }
        
        public void OnDefense(InputAction.CallbackContext context) {
            if (context.phase == InputActionPhase.Started) {
                Defense.Invoke();
            }
        }

        public void OnMouseControlCamera(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    EnableMouseControlCamera.Invoke();
                    break;
                case InputActionPhase.Canceled:
                    DisableMouseControlCamera.Invoke();
                    break;
            }
        }

        public void OnRun(InputAction.CallbackContext context) {
        }

        public void OnJump(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Started:
                    Jump.Invoke(true);
                    break;
                case InputActionPhase.Canceled:
                    Jump.Invoke(false);
                    break;
            }
        }
    }

}
