
using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneGame
{
    [RequireComponent(typeof(PlayerInput))]
    public class LR_Drone_Inputs : MonoBehaviour
    {
        #region Variables
    
        public Vector2 CyclicValue { get; private set; }
        public float CollectiveValue { get; private set; }
        public float PedalValue { get; private set; }
    
        #endregion
    
        #region Main Methods
    
        void Update()
        {

        }
    
        #endregion
    
        #region Input Methods

        private void OnCyclic(InputValue value)
        {
            CyclicValue = value.Get<Vector2>();
        }

        private void OnPedals(InputValue value)
        {
            PedalValue = value.Get<float>();
        }

        private void OnCollective(InputValue value)
        {
            CollectiveValue = value.Get<float>();
        }
    
        #endregion
    }
}