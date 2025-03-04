using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.InputSystem;

namespace DroneGame
{
    [RequireComponent(typeof(LR_Drone_Inputs))]
    public class LR_Drone_Controller : LR_Base_Rigidbody
    {
        #region Variables

        [Header("Control Properties")]
        [SerializeField] private float minMaxPitch = 30.0f;
        [SerializeField] private float minMaxRoll = 30.0f;
        [SerializeField] private float yawPower = 4.0f;
        [SerializeField] private float lerpSpeed = 2f;
        
        [SerializeField] private Transform cameraTransform;
        
        private LR_Drone_Inputs _input;
        private List<IEngine> _engines = new List<IEngine>();

        private float _finalPitch;
        private float _finalRoll;
        private float _yaw;
        private float _finalYaw;
        
        #endregion
    
        #region Main Methods
    
        void Start()
        {
            _input = GetComponent<LR_Drone_Inputs>();
            _engines = GetComponentsInChildren<IEngine>().ToList<IEngine>();
    
            PlayerInput playerInput = GetComponent<PlayerInput>();

            if (Gamepad.current != null && gameObject.name == "Player_02") 
            {
                // If a gamepad is connected, assign gamepad controls
                playerInput.SwitchCurrentControlScheme("Gamepad", Gamepad.current);
                Debug.Log(gameObject.name + " switched to Gamepad controls");
            }
            else if (gameObject.name == "Player_01")
            {
                // Default to keyboard for Player_01
                playerInput.SwitchCurrentControlScheme("KeyboardLeft", Keyboard.current);
                Debug.Log(gameObject.name + " switched to KeyboardLeft");
            }
            else if (gameObject.name == "Player_02")
            {
                // Default to keyboard for Player_02
                playerInput.SwitchCurrentControlScheme("KeyboardRight", Keyboard.current);
                Debug.Log(gameObject.name + " switched to KeyboardRight");
            }
        }

        
        #endregion
    
        #region Custom Methods

        protected override void HandlePhysics()
        {
            if (!LR_Game_Manager.Instance.IsGamePlaying()) return;
            HandleEngines();
            HandleControls();
        }
    
        protected virtual void HandleEngines()
        {
            foreach (IEngine engine in _engines)
            {
                engine.UpdateEngine(Rb, _input);
            }
        }

        protected virtual void HandleControls()
        {
            // Read player input
            float pitchInput = _input.CyclicValue.y;
            float rollInput = -_input.CyclicValue.x;
            float yawInput = _input.PedalValue;

            // Calculate desired pitch and roll angles
            float targetPitch = pitchInput * minMaxPitch;
            float targetRoll = rollInput * minMaxRoll;

            // Smoothly interpolate pitch and roll
            _finalPitch = Mathf.Lerp(_finalPitch, targetPitch, lerpSpeed * Time.deltaTime);
            _finalRoll = Mathf.Lerp(_finalRoll, targetRoll, lerpSpeed * Time.deltaTime);

            // Accumulate yaw over time
            _yaw += yawInput * yawPower * Time.deltaTime;

            // Combine rotation
            Quaternion rot = Quaternion.Euler(_finalPitch, _yaw, _finalRoll);
    
            // Apply the final rotation
            Rb.MoveRotation(rot);

            // Smooth camera adjustment (same as before)
            if (cameraTransform != null)
            {
                Vector3 targetForward = transform.forward;
                targetForward.y = 0; // Keep camera level
                targetForward.Normalize();

                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, Quaternion.LookRotation(targetForward, Vector3.up), lerpSpeed * Time.deltaTime);
            }
        }
        
        public void ShutdownEngines()
        {
            foreach (IEngine engine in _engines)
            {
                engine.ShutdownEngine();
            }
        }
        
        #endregion
    }
}