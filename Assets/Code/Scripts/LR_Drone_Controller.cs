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

        private float finalPitch;
        private float finalRoll;
        private float yaw;
        private float finalYaw;
        
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
            HandleEngines();
            HandleControls();
        }
    
        protected virtual void HandleEngines()
        {
            //Rb.AddForce(Vector3.up * (Rb.mass * Physics.gravity.magnitude));
            foreach (IEngine engine in _engines)
            {
                engine.UpdateEngine(Rb, _input);
            }
        }

        protected virtual void HandleControls()
        {
            float pitch = _input.CyclicValue.y * minMaxPitch;
            float roll = -_input.CyclicValue.x * minMaxRoll;
            yaw += _input.PedalValue * yawPower;

            finalPitch = Mathf.Lerp(finalPitch, pitch, lerpSpeed * Time.deltaTime);
            finalRoll = Mathf.Lerp(finalRoll, roll, lerpSpeed * Time.deltaTime);
            finalYaw = Mathf.Lerp(finalYaw, yaw, lerpSpeed * Time.deltaTime);

            // Smooth camera rotation
            if (cameraTransform != null)
            {
                // Target forward direction for the camera
                Vector3 targetForward = transform.forward;
                targetForward.y = 0; // Neutralize pitch (vertical tilt)
                targetForward.Normalize();

                // Calculate target rotation
                Quaternion targetRotation = Quaternion.LookRotation(targetForward, Vector3.up);

                // Smoothly interpolate to target rotation
                cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
            }

            Quaternion rot = Quaternion.Euler(finalPitch, finalYaw, finalRoll);
            Rb.MoveRotation(rot);
        }


    
        #endregion
    }
   
}