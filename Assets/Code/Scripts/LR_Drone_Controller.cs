using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

            // **FIX**: Combine rotation in the original way
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
        
        #endregion
    }
}